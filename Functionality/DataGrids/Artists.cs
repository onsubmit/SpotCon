//-----------------------------------------------------------------------
// <copyright file="Artists.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using SpotCon.DataStructures;

    /// <summary>
    /// Methods for the artists data grid
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// dataGridViewArtists CellMouseEnter event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewArtists_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewArtists.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewArtists.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 31);

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(28, 28, 31);
                }
            }
        }

        /// <summary>
        /// dataGridViewArtists CellMouseLeave event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewArtists_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewTracks.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewArtists.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = this.dataGridViewArtists.DefaultCellStyle.BackColor;

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = this.dataGridViewTracks.DefaultCellStyle.SelectionBackColor;
                }
            }
        }

        /// <summary>
        /// dataGridViewArtists SelectionChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewArtists_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridViewArtists.Rows.Count < 3)
            {
                // Return if the data grid hasn't been fully populated, yet or if there's only one artist,
                // switching between "All artists" and the artist will give the same results
                return;
            }

            if (this.dataGridViewArtists.Rows[0].Selected)
            {
                foreach (DataGridViewRow row in this.dataGridViewArtists.SelectedRows)
                {
                    if (row.Index != 0)
                    {
                        row.Selected = false;
                    }
                }

                this.trackDataSetFiltered = this.trackDataSet;
                this.PopulateTracks();
                this.PopulateAlbumFilter();
            }
            else if (this.dataGridViewArtists.SelectedRows.Count > 0)
            {
                List<string> selectedArtistHrefs = new List<string>();
                foreach (DataGridViewRow row in this.dataGridViewArtists.SelectedRows)
                {
                    selectedArtistHrefs.Add((row.Tag as TrackEx).Artist.Href);
                }

                this.trackDataSetFiltered = this.trackDataSet.Where(t => selectedArtistHrefs.Contains(t.Artist.Href)).ToList();
                this.PopulateTracks();
                this.PopulateAlbumFilter(selectedArtistHrefs);
            }

            if (this.dataGridViewAlbums.Rows.Count > 0)
            {
                this.dataGridViewAlbums.Rows[0].Selected = true;
            }
        }

        /// <summary>
        /// dataGridViewArtists SortCompare event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewArtists_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.RowIndex1 == 0 || e.RowIndex2 == 0)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Populates artist filter
        /// </summary>
        /// <param name="track">Track to add</param>
        private void PopulateArtistFilter(TrackEx track)
        {
            Dictionary<string, TrackEx> distinctArtists = this.dataGridViewArtists.Tag as Dictionary<string, TrackEx>;

            if (distinctArtists == null)
            {
                distinctArtists = new Dictionary<string, TrackEx>();
            }

            if (!distinctArtists.ContainsKey(track.Artist.Href))
            {
                distinctArtists.Add(track.Artist.Href, track);
                if (this.dataGridViewArtists.Rows.Count == 0)
                {
                    this.dataGridViewArtists.Rows.Add(string.Format("All ({0} artist{1})", distinctArtists.Count(), distinctArtists.Count() == 1 ? string.Empty : "s"));
                }
                else
                {
                    this.dataGridViewArtists[0, 0].Value = string.Format("All ({0} artist{1})", distinctArtists.Count(), distinctArtists.Count() == 1 ? string.Empty : "s");
                }

                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(this.dataGridViewArtists, track.Artist.Name);
                newRow.Tag = track;

                int index = this.dataGridViewArtists.Rows.Count;
                foreach (DataGridViewRow row in this.dataGridViewArtists.Rows)
                {
                    if (row.Index == 0)
                    {
                        continue;
                    }

                    string artistName = row.Cells[0].Value.ToString();
                    if (string.Compare(track.Artist.Name, artistName) < 0)
                    {
                        index = row.Index;
                        break;
                    }
                }

                this.dataGridViewArtists.Rows.Insert(index, newRow);
                this.dataGridViewArtists.Tag = distinctArtists;
            }
        }

        /// <summary>
        /// Populates the artist filters
        /// </summary>
        private void PopulateArtistFilter()
        {
            // Don't trigger any selection change events
            this.dataGridViewArtists.SelectionChanged -= this.dataGridViewArtists_SelectionChanged;

            this.dataGridViewArtists.Rows.Clear();

            var distinctArtists = from t in this.trackDataSetFiltered
                                  orderby t.Artist.Name
                                  group t by t.Artist.Href into T
                                  select T;

            Dictionary<string, TrackEx> distinctArtistTracks = new Dictionary<string, TrackEx>();
            if (distinctArtists.Any())
            {
                this.dataGridViewArtists.Rows.Add(string.Format("All ({0} artist{1})", distinctArtists.Count(), distinctArtists.Count() == 1 ? string.Empty : "s"));
                foreach (var distinctArtist in distinctArtists)
                {
                    TrackEx track = distinctArtist.First();

                    if (track.Artist.Href == null)
                    {
                        // Various artists.
                        continue;
                    }

                    distinctArtistTracks.Add(track.Artist.Href, track);

                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridViewArtists, track.Artist.Name);
                    row.Tag = track;
                    this.dataGridViewArtists.Rows.Add(row);
                }

                this.dataGridViewArtists.SelectionChanged += this.dataGridViewArtists_SelectionChanged;
            }

            this.dataGridViewArtists.Tag = distinctArtistTracks;
        }
    }
}
