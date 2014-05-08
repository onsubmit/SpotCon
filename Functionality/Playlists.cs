//-----------------------------------------------------------------------
// <copyright file="Playlists.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using SpotCon.DataStructures;

    /// <summary>
    /// Playlist functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// addPlaylistToolStripMenuItem Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void addPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenSpotConApp();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                bwArgs.Result = new PlaylistEx(webHelper.Value.GetLatestPlaylist());
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                PlaylistEx newPlaylist = bwArgs.Result as PlaylistEx;
                if (newPlaylist.IsEmpty)
                {
                    if (DialogResult.OK == MessageBox.Show(this, Properties.Resources.AddPlaylistMessage, Properties.Resources.AddPlaylistCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                    {
                        bw.RunWorkerAsync();
                    }
                }
                else
                {
                    DataGridViewRow row = null;
                    var playlists = GetPlaylistCache();
                    if (playlists.ContainsKey(newPlaylist.Uri))
                    {
                        foreach (DataGridViewRow r in this.dataGridViewPlaylists.Rows)
                        {
                            if ((r.Tag as PlaylistEx).Uri.Equals(newPlaylist.Uri, StringComparison.OrdinalIgnoreCase))
                            {
                                row = r;
                                break;
                            }
                        }

                        row.Cells[0].Value = newPlaylist.Name;
                        row.Tag = newPlaylist;
                        row.Selected = true;

                        this.UpdatePlaylistCache(newPlaylist);
                    }
                    else
                    {
                        row = new DataGridViewRow();
                        row.CreateCells(this.dataGridViewPlaylists, newPlaylist.Name);
                        row.Tag = newPlaylist;
                        this.dataGridViewPlaylists.Rows.Add(row);
                        row.Selected = true;

                        this.AddToPlaylistCache(newPlaylist);
                    }
                }
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// refreshPlaylistToolStripMenuItem Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void refreshPlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.dataGridViewPlaylists.SelectedRows)
            {
                rows.Add(row);
            }

            this.RefreshPlaylistRows(rows);
        }

        /// <summary>
        /// refreshAllPlaylistsToolStripMenuItem Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void refreshAllPlaylistsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            foreach (DataGridViewRow row in this.dataGridViewPlaylists.Rows)
            {
                rows.Add(row);
            }

            this.RefreshPlaylistRows(rows);
        }

        /// <summary>
        /// removePlaylistToolStripMenuItem Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void removePlaylistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> rowIndices = new List<int>();
            foreach (DataGridViewRow row in this.dataGridViewPlaylists.SelectedRows)
            {
                rowIndices.Add(row.Index);
            }

            if (!rowIndices.Any())
            {
                return;
            }

            Dictionary<string, PlaylistEx> playlists = this.GetPlaylistCache();
            foreach (int index in rowIndices.OrderByDescending(i => i).Select(i => i))
            {
                PlaylistEx playlist = this.dataGridViewPlaylists.Rows[index].Tag as PlaylistEx;
                if (playlists.ContainsKey(playlist.Uri))
                {
                    playlists.Remove(playlist.Uri);
                }

                this.dataGridViewPlaylists.Rows[index].Selected = false;
                this.dataGridViewPlaylists.Rows.RemoveAt(index);
            }

            this.ReplacePlaylistCache(playlists);
        }

        /// <summary>
        /// Loads the playlists from the cache
        /// </summary>
        private void LoadPlaylists()
        {
            if (Properties.Settings.Default.Playlists != null)
            {
                this.dataGridViewPlaylists.SelectionChanged -= this.dataGridViewPlaylists_SelectionChanged;

                Dictionary<string, PlaylistEx> playlists = this.GetPlaylistCache();
                foreach (var p in playlists)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridViewPlaylists, p.Value.Name);
                    row.Tag = p.Value;
                    this.dataGridViewPlaylists.Rows.Add(row);
                    row.Selected = false;
                }

                this.dataGridViewPlaylists.SelectionChanged += this.dataGridViewPlaylists_SelectionChanged;
            }
        }

        /// <summary>
        /// Gets the playlist cache
        /// </summary>
        /// <returns>Playlist cache</returns>
        private Dictionary<string, PlaylistEx> GetPlaylistCache()
        {
            StringCollection sc = Properties.Settings.Default.Playlists;
            Dictionary<string, PlaylistEx> playlists = new Dictionary<string, PlaylistEx>();

            if (sc != null)
            {
                string[] split = null;
                playlists = sc.Cast<string>().ToDictionary(s => (split = s.Split(new[] { "|||" }, StringSplitOptions.None))[0], s => new PlaylistEx(split[0], split[1], split[2].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList()));
            }

            return playlists;
        }

        /// <summary>
        /// Adds the given playlist the the playlist cache
        /// </summary>
        /// <param name="playlist">Playlist to add</param>
        private void AddToPlaylistCache(PlaylistEx playlist)
        {
            StringCollection sc = Properties.Settings.Default.Playlists ?? new StringCollection();
            sc.Add(string.Format("{0}|||{1}|||{2}", playlist.Uri, playlist.Name, string.Join(",", playlist.Tracks)));
            Properties.Settings.Default.Playlists = sc;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Updates an existing cached playlist entry
        /// </summary>
        /// <param name="playlist">Playlist to update</param>
        private void UpdatePlaylistCache(PlaylistEx playlist)
        {
            Dictionary<string, PlaylistEx> playlists = this.GetPlaylistCache();
            playlists[playlist.Uri] = playlist;
            this.ReplacePlaylistCache(playlists);
        }

        /// <summary>
        /// Replaces the playlist cache
        /// </summary>
        /// <param name="playlists">New playlists to write out to the cache</param>
        private void ReplacePlaylistCache(Dictionary<string, PlaylistEx> playlists)
        {
            StringCollection sc = new StringCollection();
            foreach (var playlist in playlists.Values)
            {
                sc.Add(string.Format("{0}|||{1}|||{2}", playlist.Uri, playlist.Name, string.Join(",", playlist.Tracks)));
            }

            Properties.Settings.Default.Playlists = sc;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Refreshes the given playlists
        /// </summary>
        /// <param name="rows">Rows of playlists to refresh</param>
        private void RefreshPlaylistRows(List<DataGridViewRow> rows)
        {
            if (!rows.Any())
            {
                return;
            }

            this.OpenSpotConApp();
            HashSet<string> tracks = new HashSet<string>();
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                if (rows.Any())
                {
                    PlaylistEx currentPlaylist = rows[0].Tag as PlaylistEx;
                    bwArgs.Result = new Tuple<PlaylistEx, PlaylistEx>(currentPlaylist, new PlaylistEx(webHelper.Value.GetPlaylist(currentPlaylist.Uri)));
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                Tuple<PlaylistEx, PlaylistEx> playlists = bwArgs.Result as Tuple<PlaylistEx, PlaylistEx>;

                if (playlists != null)
                {
                    PlaylistEx currentPlaylist = playlists.Item1;
                    PlaylistEx newPlaylist = playlists.Item2;

                    if (newPlaylist.IsEmpty)
                    {
                        string message = string.Format(Properties.Resources.RefreshPlaylistMessage, currentPlaylist.Name);
                        if (DialogResult.OK == MessageBox.Show(this, message, Properties.Resources.RefreshPlaylistCaption, MessageBoxButtons.OKCancel, MessageBoxIcon.Information))
                        {
                            bw.RunWorkerAsync();
                        }
                    }
                    else if (!newPlaylist.IsEmpty)
                    {
                        rows[0].Cells[0].Value = newPlaylist.Name;
                        rows[0].Tag = newPlaylist;

                        this.UpdatePlaylistCache(newPlaylist);
                        tracks.UnionWith(newPlaylist.Tracks);
                        rows.RemoveAt(0);
                        bw.RunWorkerAsync();
                    }
                }
                else if (tracks.Any())
                {
                    ImportTrackList(tracks);
                }
            };

            bw.RunWorkerAsync();
        }
    }
}
