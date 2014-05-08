//-----------------------------------------------------------------------
// <copyright file="Playlists.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using SpotCon.DataStructures;

    /// <summary>
    /// Methods for the playlists data grid
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// dataGridViewPlaylists SelectionChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewPlaylists_SelectionChanged(object sender, EventArgs e)
        {
            HashSet<string> tracks = new HashSet<string>();
            foreach (DataGridViewRow row in this.dataGridViewPlaylists.SelectedRows)
            {
                PlaylistEx playlist = row.Tag as PlaylistEx;
                tracks.UnionWith(playlist.Tracks);
            }

            this.ImportTrackList(tracks);
        }
    }
}
