//-----------------------------------------------------------------------
// <copyright file="Server.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Windows.Forms;
    using SpotCon.DataStructures;
    using SpotCon.Enums;
    using SpotifyWebHelperSharp;

    /// <summary>
    /// Server/client functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// TCP port to use
        /// </summary>
        private const int Port = 1729;

        /// <summary>
        /// Lock for sending packets
        /// </summary>
        private readonly object sendLock = new object();

        /// <summary>
        /// Lock for receiving packets
        /// </summary>
        private readonly object receiveLock = new object();

        /// <summary>
        /// The name of the current host
        /// </summary>
        private string currentHost;

        /// <summary>
        /// The name of the currently selected host
        /// </summary>
        private string selectedHost;

        /// <summary>
        /// Buffer for server input
        /// </summary>
        private string inputFromServer;

        /// <summary>
        /// Buffer for client input
        /// </summary>
        private string inputFromClient;

        /// <summary>
        /// The number of milliseconds to wait between each attempt to update status from Spotify
        /// </summary>
        private int defaultWaitTime = 1000;

        /// <summary>
        /// Server listener socket
        /// </summary>
        private Socket listener;

        /// <summary>
        /// Asynchronous callback for server
        /// </summary>
        private AsyncCallback callbackServer;

        /// <summary>
        /// Asynchronous callback for client
        /// </summary>
        private AsyncCallback callbackClient;

        /// <summary>
        /// Temporary placeholder for newly created server worker sockets
        /// </summary>
        private Socket newWorker;

        /// <summary>
        /// List of commands that execute in response to client or server requests
        /// </summary>
        private Dictionary<string, Action<string, string, string>> commands;

        /// <summary>
        /// Host information
        /// </summary>
        private Dictionary<string, HostData> hostData = new Dictionary<string, HostData>();

        /// <summary>
        /// Server worker sockets
        /// </summary>
        private Dictionary<string, Socket> workers = new Dictionary<string, Socket>();

        /// <summary>
        /// Used to retrieve status from Spotify
        /// </summary>
        private Lazy<SpotifyWebHelper> webHelper = new Lazy<SpotifyWebHelper>(() => { return new SpotifyWebHelper(); });

        /// <summary>
        /// Initializes the list of commands
        /// </summary>
        private void InitializeCommands()
        {
            this.commands = new Dictionary<string, Action<string, string, string>>()
            {
                {
                    "|Connected",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        this.workers[orig] = this.newWorker;

                        if (orig != currentHost)
                        {
                            this.SetStatus(string.Format(Properties.Resources.ClientConnected, orig));
                        }
                    })
                },
                {
                    "|Previous",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        SendMessage(this.spotifyHwnd, AppCommands.WM_APPCOMMAND, 0, AppCommands.APPCOMMAND_MEDIA_PREVIOUSTRACK);
                    })
                },
                {
                    "|Play",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        new BackgroundWorker().QuickStart(() => this.webHelper.Value.Play(s));
                    })
                },
                {
                    "|Next",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        SendMessage(this.spotifyHwnd, AppCommands.WM_APPCOMMAND, 0, AppCommands.APPCOMMAND_MEDIA_NEXTTRACK);
                    })
                },
                {
                    "|Pause",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        SendMessage(this.spotifyHwnd, AppCommands.WM_APPCOMMAND, 0, AppCommands.APPCOMMAND_MEDIA_PLAY_PAUSE);
                    })
                },
                {
                    "|SetVolume",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        string[] split = s.Split(':');
                        double desired = 0;
                        double.TryParse(split[0], out desired);

                        double current = 0;
                        double.TryParse(split[1], out current);

                        string action = desired < current ? "{DOWN}" : "{UP}";

                        bool success = SetForegroundWindow(this.spotifyHwnd);
                        for (int i = 0; i <= 100 * Math.Abs((desired - current) / 6.25); i++)
                        {
                            System.Threading.Thread.Sleep(10);
                            SendKeys.Send(string.Format("^({0})", action));
                            System.Threading.Thread.Sleep(10);
                        }

                        if (dest == this.currentHost)
                        {
                            success = SetForegroundWindow(this.Handle);
                        }
                    })
                },
                {
                    "|GetStatusFromServer",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        new BackgroundWorker().QuickStart(() =>
                        {
                            string response = string.Empty;
                            webHelper.Value.GetStatus(out response);
                            this.SendToClient(response + "|ServerStatusReturned", orig);
                        });
                    })
                },
                {
                    "|ServerStatusReturned",
                    new Action<string, string, string>((dest, orig, s) =>
                    {
                        Responses.Status status = webHelper.Value.Deserialize<Responses.Status>(s);
                        bool success = this.UpdateStatus(status);
                        string currentAlbumUri = this.currentStatus == null || this.currentStatus.Track == null ? null : this.currentStatus.Track.AlbumResource.Uri;
                        this.currentStatus = status;

                        if (success && status.Track.AlbumResource.Uri != currentAlbumUri)
                        {
                            BackgroundWorker bw = new BackgroundWorker();
                            bw.DoWork += (bwSender, bwArgs) =>
                            {
                                Tuple<string, string> art = webHelper.Value.GetArt(status.Track);
                                bwArgs.Result = art;
                            };

                            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
                            {
                                Tuple<string, string> art = bwArgs.Result as Tuple<string, string>;
                                pictureBox.LoadAsync(art.Item2);
                            };

                            bw.RunWorkerAsync();
                        }
                    })
                }
            };
        }

        /// <summary>
        /// Connects to the server
        /// </summary>
        /// <param name="colIndex">Column index of host data grid</param>
        /// <param name="rowIndex">Row index of host data grid</param>
        /// <param name="server">Server to which to connect</param>
        private void ConnectToServer(int colIndex, int rowIndex, string server)
        {
            dataGridViewComputers.Enabled = false;
            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };
            bw.DoWork += (bwSender, bwArgs) =>
            {
                if (!this.hostData.ContainsKey(server) || this.hostData[server] == null)
                {
                    bwArgs.Result = false;
                }
                else
                {
                    try
                    {
                        Socket socket = this.hostData[server].Socket != null &&
                                        this.hostData[server].Socket.Connected ? this.hostData[server].Socket : new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        IPEndPoint remoteEndPoint = new IPEndPoint(this.hostData[server].IPAddress, Port);
                        IAsyncResult result = socket.BeginConnect(remoteEP: remoteEndPoint, callback: null, state: null);

                        if (server != this.currentHost)
                        {
                            bw.ReportProgress(0, string.Format(Properties.Resources.ConnectingToClient, server));
                        }

                        bool success = result.AsyncWaitHandle.WaitOne(millisecondsTimeout: 3000, exitContext: true);
                        if (success)
                        {
                            this.hostData[server].Socket = socket;
                            this.hostData[server].ConnectionStatus = socket.Connected ? ClientConnectStatus.Connected : ClientConnectStatus.ConnectionFailure;

                            if (socket.Connected)
                            {
                                this.SendToServer("|Connected");
                            }
                        }
                        else
                        {
                            socket.Close();
                            this.hostData[server].ConnectionStatus = ClientConnectStatus.ConnectionFailure;
                        }

                        bwArgs.Result = success;
                    }
                    catch (Exception ex)
                    {
                        bwArgs.Result = ex;
                    }
                }
            };

            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                this.SetStatusBusy(bwArgs.UserState as string);
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                dataGridViewComputers.Enabled = true;

                if (bwArgs.Result is Exception)
                {
                    Exception ex = bwArgs.Result as Exception;
                    this.SetStatusError(ex.Message);
                }
                else if (bwArgs.Result is bool)
                {
                    bool success = (bool)bwArgs.Result;
                    ClientConnectStatus status = this.hostData[server] == null ? ClientConnectStatus.ConnectionFailure : this.hostData[server].ConnectionStatus;
                    dataGridViewComputers[(int)HostColumns.ConnectionStatus, rowIndex].Value = hostImages.Images[(int)status];
                    switch (status)
                    {
                        case ClientConnectStatus.Connected:
                            if (server != this.currentHost)
                            {
                                this.SetStatusSuccess(string.Format(Properties.Resources.ConnectingToClientSuccess, server));
                            }

                            dataGridViewComputers.Rows[rowIndex].Selected = true;

                            Timer timer = null;
                            if (this.hostData[server].Timer == null)
                            {
                                timer = new Timer() { Interval = this.defaultWaitTime, Enabled = true };
                                timer.Tick += (sender, e) =>
                                {
                                    BackgroundWorker bw2 = new BackgroundWorker();
                                    bw2.DoWork += (bw2Sender, bw2Args) =>
                                    {
                                        this.SendToServer("|GetStatusFromServer", server);
                                    };

                                    bw2.RunWorkerAsync();
                                };

                                this.hostData[server].Timer = timer;
                            }
                            else
                            {
                                timer = this.hostData[server].Timer;
                            }

                            foreach (var ip in this.hostData.Values.Where(i => i != null && i.Timer != null && i.Timer.Enabled))
                            {
                                ip.Timer.Stop();
                            }

                            timer.Start();
                            break;

                        case ClientConnectStatus.ConnectionFailure:
                            this.SetStatusError(string.Format(success ? Properties.Resources.ConnectingToClientUnknown : Properties.Resources.ConnectingToClientError, server));
                            dataGridViewComputers[colIndex, rowIndex].Value = Properties.Resources.Checkbox;
                            dataGridViewComputers[colIndex, rowIndex].Tag = false;
                            break;

                        case ClientConnectStatus.Disconnected:
                            this.ClearStatus();
                            break;
                    }

                    if (success && this.hostData[server].Socket.Connected)
                    {
                        this.WaitForDataClient(this.hostData[server].Socket);
                    }
                }
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Invoked when a new client connects
        /// </summary>
        /// <param name="result">Connection result</param>
        private void OnClientConnect(IAsyncResult result)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                try
                {
                    // Complete the BeginAccept() asynchronous call by calling EndAccept() which returns a reference to the new socket
                    Socket socket = this.listener.EndAccept(result);
                    this.newWorker = socket;

                    // Let the worker socket do further processing for the just connected client
                    WaitForDataServer(socket);

                    // Since the main socket is now free, it can go back and wait for other clients who are attempting to connect
                    listener.BeginAccept(new AsyncCallback(OnClientConnect), null);
                }
                catch (ObjectDisposedException)
                {
                    System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
                }
                catch (Exception ex)
                {
                    bwArgs.Result = ex;
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                if (bwArgs.Result is Exception && this.InvokeRequired)
                {
                    this.BeginInvoke((Delegate)(new Action(() => this.SetStatusError((bwArgs.Result as Exception).Message))));
                }
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Invoked when data is received by the server
        /// </summary>
        /// <param name="result">Data result</param>
        private void OnDataReceivedByServer(IAsyncResult result)
        {
            SocketPacket packet = (SocketPacket)result.AsyncState;

            // Complete the BeginReceive() asynchronous call by the EndReceive() method which will return the number of characters written to the stream by the client
            if (packet.Socket.Connected)
            {
                lock (this.receiveLock)
                {
                    int byteCount = 0;
                    try
                    {
                        byteCount = packet.Socket.EndReceive(result);
                    }
                    catch (SocketException)
                    {
                        return;
                    }

                    char[] chars = new char[byteCount];
                    int charLen = Encoding.UTF8.GetChars(bytes: packet.Buffer, byteIndex: 0, byteCount: byteCount, chars: chars, charIndex: 0);
                    string data = new string(chars);
                    this.inputFromClient += data;

                    foreach (var command in this.commands)
                    {
                        if (this.inputFromClient.EndsWith(command.Key))
                        {
                            string input = this.inputFromClient.Replace(command.Key, string.Empty);
                            this.inputFromClient = string.Empty;

                            string[] split = input.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            string originatingClient = split[0];
                            input = split.Length > 1 ? split[1] : string.Empty;

                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke((Delegate)command.Value, this.currentHost, originatingClient, input);
                            }

                            break;
                        }
                    }

                    // Continue the waiting for data on the socket
                    this.WaitForDataServer(packet.Socket);
                }
            }
        }

        /// <summary>
        /// Invoked when data is received by the client
        /// </summary>
        /// <param name="result">Data result</param>
        private void OnDataReceivedByClient(IAsyncResult result)
        {
            SocketPacket packet = (SocketPacket)result.AsyncState;

            // Complete the BeginReceive() asynchronous call by the EndReceive() method which will return the number of characters written to the stream by the client
            if (packet.Socket.Connected)
            {
                lock (this.receiveLock)
                {
                    int byteCount = 0;
                    try
                    {
                        byteCount = packet.Socket.EndReceive(result);
                    }
                    catch (SocketException)
                    {
                        return;
                    }

                    char[] chars = new char[byteCount];
                    int charLen = Encoding.UTF8.GetChars(bytes: packet.Buffer, byteIndex: 0, byteCount: byteCount, chars: chars, charIndex: 0);
                    string data = new string(chars);
                    this.inputFromServer += data;

                    foreach (var command in this.commands)
                    {
                        if (this.inputFromServer.EndsWith(command.Key))
                        {
                            string input = this.inputFromServer.Replace(command.Key, string.Empty);
                            this.inputFromServer = string.Empty;

                            string[] split = input.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            string originatingServer = split[0];
                            input = split.Length > 1 ? split[1] : string.Empty;

                            if (this.InvokeRequired)
                            {
                                this.BeginInvoke((Delegate)command.Value, this.currentHost, originatingServer, input);
                            }

                            break;
                        }
                    }

                    // Continue the waiting for data on the socket
                    this.WaitForDataClient(packet.Socket);
                }
            }
        }

        /// <summary>
        /// Sends the given command to the server
        /// </summary>
        /// <param name="command">Command to send</param>
        private void SendToServer(string command)
        {
            this.SendToServer(command, this.selectedHost, this.currentHost);
        }

        /// <summary>
        /// Sends the given command to the server
        /// </summary>
        /// <param name="command">Command to send</param>
        /// <param name="server">Destination server</param>
        private void SendToServer(string command, string server)
        {
            this.SendToServer(command, server, this.currentHost);
        }

        /// <summary>
        /// Sends the given command to the server
        /// </summary>
        /// <param name="command">Command to send</param>
        /// <param name="server">Destination server</param>
        /// <param name="client">Client name</param>
        private void SendToServer(string command, string server, string client)
        {
            try
            {
                if (this.hostData.ContainsKey(server) && this.hostData[server].Socket != null && this.hostData[server].Socket.Connected)
                {
                    lock (this.sendLock)
                    {
                        byte[] bytes = Encoding.ASCII.GetBytes(client + "|" + command);
                        this.hostData[server].Socket.Send(bytes);
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// Sends the given command to the client
        /// </summary>
        /// <param name="command">Command to send</param>
        private void SendToClient(string command)
        {
            this.SendToClient(command, this.selectedHost, this.currentHost);
        }

        /// <summary>
        /// Sends the given command to the client
        /// </summary>
        /// <param name="command">Command to send</param>
        /// <param name="client">Destination client</param>
        private void SendToClient(string command, string client)
        {
            this.SendToClient(command, client, this.currentHost);
        }

        /// <summary>
        /// Sends the given command to the client
        /// </summary>
        /// <param name="command">Command to send</param>
        /// <param name="client">Destination client</param>
        /// <param name="server">Server name</param>
        private void SendToClient(string command, string client, string server)
        {
            try
            {
                if (this.workers.ContainsKey(client) && this.workers[client].Connected)
                {
                    lock (this.sendLock)
                    {
                        byte[] bytes = Encoding.ASCII.GetBytes(server + "|" + command);
                        this.workers[client].Send(bytes);
                    }
                }
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// Invoked when the server begins waiting for data
        /// </summary>
        /// <param name="socket">Server socket</param>
        private void WaitForDataServer(Socket socket)
        {
            try
            {
                if (this.callbackServer == null)
                {
                    // Specify the callback function which is to be invoked when there is any write activity by the connected client
                    this.callbackServer = new AsyncCallback(this.OnDataReceivedByServer);
                }

                SocketPacket socketPacket = new SocketPacket() { Socket = socket };

                // Start receiving any data written by the connected client asynchronously
                socket.BeginReceive(buffer: socketPacket.Buffer, offset: 0, size: socketPacket.Buffer.Length, socketFlags: SocketFlags.None, callback: this.callbackServer, state: socketPacket);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// Invoked when the client begins waiting for data
        /// </summary>
        /// <param name="socket">Client socket</param>
        private void WaitForDataClient(Socket socket)
        {
            try
            {
                if (this.callbackClient == null)
                {
                    // Specify the callback function which is to be invoked when there is any write activity by the connected client
                    this.callbackClient = new AsyncCallback(this.OnDataReceivedByClient);
                }

                SocketPacket socketPacket = new SocketPacket() { Socket = socket };

                // Start receiving any data written by the connected client asynchronously
                socket.BeginReceive(buffer: socketPacket.Buffer, offset: 0, size: socketPacket.Buffer.Length, socketFlags: SocketFlags.None, callback: this.callbackClient, state: socketPacket);
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}
