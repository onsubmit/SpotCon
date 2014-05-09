//-----------------------------------------------------------------------
// <copyright file="SpotConForm.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.DirectoryServices;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Windows.Forms;
    using System.Xml.Linq;
    using SpotCon.DataStructures;
    using SpotCon.Enums;
    using SpotCon.PlaylistImporter;
    using SpotifyWebHelperSharp;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;
    using SpotifyWebSharp.SpotifyServices;

    /// <summary>
    /// Main form
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Application directory
        /// </summary>
        private static readonly string AppDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpotCon");

        /// <summary>
        /// Directory for SpotCon Spotify app
        /// </summary>
        private static readonly string SpotifyAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "spotify", "spotcon");

        /// <summary>
        /// Handle to Spotify window
        /// </summary>
        private IntPtr spotifyHwnd = IntPtr.Zero;

        /// <summary>
        /// Indicates if the form was previously maximized
        /// </summary>
        private bool wasMaximized = false;

        /// <summary>
        /// IIS process
        /// </summary>
        private Process iisExpress;

        /// <summary>
        /// Initializes a new instance of the SpotConForm class
        /// </summary>
        public SpotConForm()
        {
            this.InitializeComponent();
            this.InitializeCommands();
            this.InstallSpotifyApp();
        }

        /// <summary>
        /// Installs the SpotCon Spotify app
        /// </summary>
        private void InstallSpotifyApp()
        {
            if (!Directory.Exists(SpotConForm.SpotifyAppFolder))
            {
                Directory.CreateDirectory(SpotConForm.SpotifyAppFolder);

                string source = Path.Combine(Directory.GetCurrentDirectory(), "spotcon-app");
                this.CopyFilesRecursively(new DirectoryInfo(source), new DirectoryInfo(SpotConForm.SpotifyAppFolder));
            }
        }

        /// <summary>
        /// Copies files recursively
        /// </summary>
        /// <param name="source">Source directory</param>
        /// <param name="target">Destination directory</param>
        private void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            }

            foreach (FileInfo file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        /// <summary>
        /// pictureBox Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                AlbumArtViewer arv = new AlbumArtViewer(this.currentStatus.Track.AlbumResource.ToString(), pictureBox.Image);
                arv.Show();
            }
        }

        /// <summary>
        /// pictureBox SizeChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void pictureBox_SizeChanged(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(500, Math.Max(410, 410 + panelPicture.Height - 150));
        }

        /// <summary>
        /// splitterMain SplitterMoved event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void splitterMain_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.linkLabelTrack.MaximumSize = new Size(splitterMain.Location.X, this.linkLabelTrack.Height);
            this.linkLabelArtist.MaximumSize = new Size(splitterMain.Location.X, this.linkLabelArtist.Height);
            panelPicture.Height = splitterMain.Location.X;
        }

        /// <summary>
        /// SpotConForm Load event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_Load(object sender, EventArgs e)
        {
            this.history.Add(() =>
            {
                this.trackDataSet.Clear();
                this.trackDataSetFiltered.Clear();
                this.PopulateTracks();
                this.PopulateArtistFilter();
                this.PopulateAlbumFilter();
                this.ClearStatus();
            });

            this.panelBack.Tag = false;
            this.panelForward.Tag = false;

            this.ActiveControl = this.panelPlayPause;
            this.textBoxSearch.Tag = this.textBoxSearch.ForeColor;

            this.ClearStatus();
            this.SetStatusBusy(Properties.Resources.SearchingNetwork);

            this.hostImages.Images.Add(Properties.Resources.Computer);
            this.hostImages.Images.Add(Properties.Resources.ComputerLink);
            this.hostImages.Images.Add(Properties.Resources.ComputerError);

            this.popImages.ImageSize = new Size(65, 8);
            this.popImages.Images.Add(Properties.Resources.pop00);
            this.popImages.Images.Add(Properties.Resources.pop01);
            this.popImages.Images.Add(Properties.Resources.pop02);
            this.popImages.Images.Add(Properties.Resources.pop03);
            this.popImages.Images.Add(Properties.Resources.pop04);
            this.popImages.Images.Add(Properties.Resources.pop05);
            this.popImages.Images.Add(Properties.Resources.pop06);
            this.popImages.Images.Add(Properties.Resources.pop07);
            this.popImages.Images.Add(Properties.Resources.pop08);
            this.popImages.Images.Add(Properties.Resources.pop09);
            this.popImages.Images.Add(Properties.Resources.pop10);
            this.popImages.Images.Add(Properties.Resources.pop11);
            this.popImages.Images.Add(Properties.Resources.pop12);
            this.popImages.Images.Add(Properties.Resources.pop13);
            this.popImages.Images.Add(Properties.Resources.pop14);
            this.popImages.Images.Add(Properties.Resources.pop15);
            this.popImages.Images.Add(Properties.Resources.pop16);
            this.popImages.Images.Add(Properties.Resources.pop17);
            this.popImages.Images.Add(Properties.Resources.pop18);
            this.popImages.Images.Add(Properties.Resources.pop19);
            this.popImages.Images.Add(Properties.Resources.pop20);
            this.popImages.Images.Add(Properties.Resources.pop21);
            this.popImages.Images.Add(Properties.Resources.pop22);

            Directory.CreateDirectory(Path.Combine(SpotConForm.AppDataFolder, "Lookup", "Tracks"));
            Directory.CreateDirectory(Path.Combine(SpotConForm.AppDataFolder, "Lookup", "Albums"));
            Directory.CreateDirectory(Path.Combine(SpotConForm.AppDataFolder, "Search"));
            Directory.CreateDirectory(Path.Combine(SpotConForm.AppDataFolder, "Plugins"));
            this.StartIIS();
            this.LoadImportPlugins();
            this.LoadPlaylists();
            this.ReadArtistCache();
            this.ReadMisspelledArtistCache();
            this.ReadMisspelledTrackCache();
            this.ReadCachedTracks();

            this.albumPercentage = (double)this.dataGridViewAlbums.Width / (double)this.dataGridViewTracks.Width;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                try
                {
                    listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // Bind to local IP address
                    IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, Port);
                    listener.Bind(ipLocal);

                    // Start listening
                    listener.Listen(backlog: 4);

                    // Create the call back for any client connections...
                    listener.BeginAccept(new AsyncCallback(OnClientConnect), state: null);

                    this.currentHost = Dns.GetHostName().ToUpper();

                    List<string> names;
                    if (Properties.Settings.Default.HostNames == null || Properties.Settings.Default.HostNames.Count == 0)
                    {
                        DirectoryEntry entry = new DirectoryEntry("WinNT:");
                        names = (from DirectoryEntry domains in entry.Children
                                 from DirectoryEntry pc in domains.Children
                                 where pc.SchemaClassName.ToLower().Contains("computer")
                                 select pc.Name.ToUpper()).ToList();

                        if (!names.Any())
                        {
                            names.Add(this.currentHost);
                        }

                        StringCollection sc = new StringCollection();
                        sc.AddRange(names.ToArray());

                        Properties.Settings.Default.HostNames = sc;
                        Properties.Settings.Default.Save();
                        bwArgs.Result = names;
                    }
                    else
                    {
                        bwArgs.Result = names = Properties.Settings.Default.HostNames.Cast<string>().ToList();
                    }

                    foreach (var name in names)
                    {
                        try
                        {
                            if (name.Equals(this.currentHost, StringComparison.OrdinalIgnoreCase))
                            {
                                this.hostData[name] = new HostData() { IPAddress = Dns.GetHostAddresses(name).FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork) };
                            }
                            else
                            {
                                this.hostData[name] = null;
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception ex)
                {
                    bwArgs.Result = ex;
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.ClearStatus();
                this.SetBusyState(false);

                if (bwArgs.Result is Exception)
                {
                    Exception ex = bwArgs.Result as Exception;
                    this.SetStatusError(ex.Message);
                    return;
                }
                else
                {
                    List<string> names = bwArgs.Result as List<string>;
                    foreach (var name in names)
                    {
                        try
                        {
                            if (this.hostData.ContainsKey(name))
                            {
                                DataGridViewRow row = new DataGridViewRow();
                                bool connected = name.Equals(this.currentHost, StringComparison.OrdinalIgnoreCase);
                                Bitmap connectedImage = connected ? Properties.Resources.CheckboxChecked : Properties.Resources.Checkbox;
                                row.CreateCells(this.dataGridViewComputers, connectedImage, hostImages.Images[(int)ClientConnectStatus.Disconnected], name);
                                row.Cells[(int)HostColumns.Checkbox].Tag = connected;
                                row.Height = 28;

                                int index = this.dataGridViewComputers.Rows.Add(row);
                                this.hostData[name].Row = dataGridViewComputers.Rows[index];
                            }
                            else
                            {
                                // TODO: Why is there an empty "else" here?
                            }
                        }
                        catch
                        {
                        }
                    }

                    bool exit = false;
                    Process[] processes = null;

                    while (!exit)
                    {
                        processes = Process.GetProcessesByName("spotify");
                        if (processes.Any())
                        {
                            this.spotifyHwnd = processes[0].MainWindowHandle;
                            break;
                        }

                        if (DialogResult.Cancel == MessageBox.Show(this, Properties.Resources.SpotifyNotFound, this.Text, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1))
                        {
                            exit = true;
                        }
                    }

                    if (exit)
                    {
                        Application.Exit();
                    }

                    GlobalMouseHandler globalClick = new GlobalMouseHandler()
                    {
                        BackAction = () => this.GoBack(),
                        ForwardAction = () => this.GoForward()
                    };

                    Application.AddMessageFilter(globalClick);

                    if (names.Contains(this.currentHost))
                    {
                        this.selectedHost = this.currentHost;
                        this.ConnectToServer((int)HostColumns.Checkbox, this.hostData[this.currentHost].Row.Index, this.currentHost);
                    }
                }
            };

            bw.RunWorkerAsync(bw);
        }

        /// <summary>
        /// SpotConForm FormClosed event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.listener != null)
            {
                this.listener.Close();
            }

            foreach (Socket worker in this.workers.Values)
            {
                if (worker != null)
                {
                    worker.Close();
                }
            }

            foreach (var item in this.hostData.Values)
            {
                if (item != null && item.Socket != null)
                {
                    item.Socket.Close();
                }
            }
        }

        /// <summary>
        /// SpotConForm FormClosing event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.iisExpress != null && !this.iisExpress.HasExited)
            {
                for (IntPtr pointer = GetTopWindow(IntPtr.Zero); pointer != IntPtr.Zero; pointer = SpotConForm.GetWindow(pointer, AppCommands.GW_HWNDNEXT))
                {
                    uint processId;
                    SpotConForm.GetWindowThreadProcessId(pointer, out processId);

                    if (this.iisExpress.Id == processId)
                    {
                        HandleRef handle = new HandleRef(null, pointer);
                        SpotConForm.PostMessage(handle, AppCommands.WM_QUIT, IntPtr.Zero, IntPtr.Zero);
                        this.iisExpress.Close();
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// SpotConForm Resize event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_Resize(object sender, EventArgs e)
        {
            this.panelRepeat.Location = new Point(this.Width - 40, this.panelRepeat.Location.Y);
            this.panelShuffle.Location = new Point(this.panelRepeat.Location.X - 1, this.panelShuffle.Location.Y);
        }

        /// <summary>
        /// SpotConForm ResizeBegin event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_ResizeBegin(object sender, EventArgs e)
        {
            this.panelFilter.SuspendLayout();
            this.albumPercentage = (double)this.dataGridViewAlbums.Width / (double)this.dataGridViewTracks.Width;
        }

        /// <summary>
        /// SpotConForm ResizeEnd event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_ResizeEnd(object sender, EventArgs e)
        {
            this.panelFilter.ResumeLayout();
            this.dataGridViewAlbums.Width = (int)Math.Round(this.dataGridViewTracks.Width * this.albumPercentage);
        }

        /// <summary>
        /// SpotConForm SizeChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void SpotConForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized || (this.wasMaximized && this.WindowState == FormWindowState.Normal))
            {
                this.wasMaximized = !this.wasMaximized;
                this.dataGridViewAlbums.Width = (int)Math.Round(this.dataGridViewTracks.Width * this.albumPercentage);
                this.dataGridViewArtists.Visible = this.dataGridViewAlbums.Visible = true;
            }
        }

        /// <summary>
        /// splitterFilterRight SplitterMoved event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void splitterFilterRight_SplitterMoved(object sender, SplitterEventArgs e)
        {
            this.albumPercentage = (double)this.dataGridViewAlbums.Width / (double)this.dataGridViewTracks.Width;
        }

        /// <summary>
        /// Starts the SpotCon web service in IIS Express
        /// </summary>
        private void StartIIS()
        {
            bool success = false;
            do
            {
                success = this.StartIISSafe();
            }
            while (!success && DialogResult.Retry == MessageBox.Show(this, Properties.Resources.IISError, this.Text, MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error));
        }

        /// <summary>
        /// Starts IIS safely
        /// </summary>
        /// <returns>True if successful</returns>
        private bool StartIISSafe()
        {
            string filename = Path.Combine(Environment.GetEnvironmentVariable("ProgramFiles"), "IIS Express", "iisexpress.exe");
            if (!File.Exists(filename))
            {
                filename = Path.Combine(Environment.GetEnvironmentVariable("ProgramW6432"), "IIS Express", "iisexpress.exe");
            }

            if (!File.Exists(filename))
            {
                return false;
            }

            ProcessStartInfo start = new ProcessStartInfo()
            {
                FileName = filename,
                Arguments = string.Format(@"/port:17290 /systray:false /path:""{0}""", Path.Combine(Directory.GetCurrentDirectory(), "WebService")),
                WorkingDirectory = Directory.GetCurrentDirectory(),
                UseShellExecute = false,
                CreateNoWindow = true
            };

            this.iisExpress = Process.Start(start);
            return true;
        }

        /// <summary>
        /// Opens the SpotCon app inside Spotify
        /// </summary>
        private void OpenSpotConApp()
        {
            SpotConForm.SetForegroundWindow(this.spotifyHwnd);
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("^l");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("spotify:app:spotcon");
            System.Threading.Thread.Sleep(100);
            SendKeys.Send("{ENTER}");
            SpotConForm.SetForegroundWindow(this.Handle);
        }
    }
}