//-----------------------------------------------------------------------
// <copyright file="Computers.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Forms;
    using SpotCon.DataStructures;
    using SpotCon.Enums;

    /// <summary>
    /// Methods for the computers data grid
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Host images
        /// </summary>
        private ImageList hostImages = new ImageList();

        /// <summary>
        /// dataGridViewComputers CellClick event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewComputers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == (int)HostColumns.Checkbox)
            {
                bool isChecked = (bool)dataGridViewComputers[(int)HostColumns.Checkbox, e.RowIndex].Tag;
                this.dataGridViewComputers[e.ColumnIndex, e.RowIndex].Tag = !isChecked;

                if (isChecked)
                {
                    dataGridViewComputers[e.ColumnIndex, e.RowIndex].Value = Properties.Resources.Checkbox;
                }
                else
                {
                    dataGridViewComputers[e.ColumnIndex, e.RowIndex].Value = Properties.Resources.CheckboxChecked;
                }
            }
        }

        /// <summary>
        /// dataGridViewComputers CellValueChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewComputers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != (int)HostColumns.Checkbox || e.RowIndex < 0)
            {
                return;
            }

            bool isChecked = (bool)dataGridViewComputers[e.ColumnIndex, e.RowIndex].Tag;
            string host = dataGridViewComputers[(int)HostColumns.Name, e.RowIndex].Value.ToString();

            if (!isChecked)
            {
                if (this.hostData.ContainsKey(host) && this.hostData[host] != null && this.hostData[host].Socket != null && this.hostData[host].Socket.Connected)
                {
                    this.hostData[host].Timer.Stop();
                    this.hostData[host].Socket.Close();
                    this.hostData[host].ConnectionStatus = ClientConnectStatus.Disconnected;
                    dataGridViewComputers[(int)HostColumns.ConnectionStatus, e.RowIndex].Value = this.hostImages.Images[(int)ClientConnectStatus.Disconnected];
                    this.SetStatus(string.Format(Properties.Resources.DisconnectedFrom, host));
                }
            }
            else
            {
                this.ConnectToServer(e.ColumnIndex, e.RowIndex, host);
            }
        }

        /// <summary>
        /// dataGridViewComputers SelectionChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewComputers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewComputers.SelectedRows.Count == 0)
            {
                return;
            }

            this.selectedHost = dataGridViewComputers.SelectedRows[0].Cells[(int)HostColumns.Name].Value.ToString();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                if (this.hostData[this.selectedHost] == null)
                {
                    this.hostData[this.selectedHost] = new HostData() { IPAddress = Dns.GetHostAddresses(this.selectedHost).FirstOrDefault(i => i.AddressFamily == AddressFamily.InterNetwork) };
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                int selectedRowIndex = dataGridViewComputers.SelectedRows[0].Index;
                for (int i = 0; i < dataGridViewComputers.Rows.Count; i++)
                {
                    DataGridViewCellStyle style = dataGridViewComputers.Rows[i].InheritedStyle;
                    dataGridViewComputers.Rows[i].DefaultCellStyle = style;
                }

                foreach (var ip in this.hostData.Values.Where(i => i != null && i.Timer != null && i.Timer.Enabled))
                {
                    ip.Timer.Stop();
                }

                if (this.hostData[this.selectedHost] != null && this.hostData[this.selectedHost].Timer != null)
                {
                    this.hostData[this.selectedHost].Timer.Start();
                }
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// dataGridViewComputers CurrentCellDirtyStateChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewComputers_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewComputers.IsCurrentCellDirty)
            {
                dataGridViewComputers.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
