//-----------------------------------------------------------------------
// <copyright file="Albums.cs" company="Andy Young">
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
    /// Methods for the albums data grid
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// The relative width of the album data grid
        /// </summary>
        private double albumPercentage;

        /// <summary>
        /// dataGridViewAlbums CellMouseEnter event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewAlbums_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewAlbums.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewAlbums.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 31);

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(28, 28, 31);
                }
            }
        }

        /// <summary>
        /// dataGridViewAlbums CellMouseLeave event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewAlbums_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewAlbums.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewAlbums.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = this.dataGridViewArtists.DefaultCellStyle.BackColor;

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = this.dataGridViewTracks.DefaultCellStyle.SelectionBackColor;
                }
            }
        }

        /// <summary>
        /// dataGridViewAlbums SelectionChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewAlbums_SelectionChanged(object sender, EventArgs e)
        {
            if (this.dataGridViewAlbums.Rows.Count < 3)
            {
                // Return if the data grids hasn't been fully populated, yet or if there's only one album,
                // switching between "All albums" and the album will give the same results
                return;
            }

            List<string> selectedArtistHrefs = new List<string>();
            bool allArtistsSelected = this.dataGridViewArtists.Rows[0].Selected;
            foreach (DataGridViewRow row in this.dataGridViewArtists.Rows)
            {
                if (row.Index != 0 && (allArtistsSelected || row.Selected))
                {
                    selectedArtistHrefs.Add((row.Tag as TrackEx).Artist.Href);
                }
            }

            List<string> selectedAlbumHrefs = new List<string>();
            if (dataGridViewAlbums.Rows[0].Selected)
            {
                foreach (DataGridViewRow row in this.dataGridViewAlbums.Rows)
                {
                    if (row.Index != 0)
                    {
                        row.Selected = false;

                        if (row.Visible)
                        {
                            selectedAlbumHrefs.Add((row.Tag as AlbumEx).Href);
                        }
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in this.dataGridViewAlbums.SelectedRows)
                {
                    selectedAlbumHrefs.Add((row.Tag as AlbumEx).Href);
                }
            }

            this.trackDataSetFiltered = this.GroupAndSortTracks(this.trackDataSet.Where(t => selectedAlbumHrefs.Contains(t.Album.Href) && (allArtistsSelected || selectedArtistHrefs.Contains(t.Artist.Href))).ToList());
            this.PopulateTracks();
        }

        /// <summary>
        /// dataGridViewAlbums SortCompare event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewAlbums_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.RowIndex1 == 0 || e.RowIndex2 == 0)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Populates the album filter
        /// </summary>
        /// <param name="track">Track to add</param>
        private void PopulateAlbumFilter(TrackEx track)
        {
            Dictionary<string, AlbumEx> distinctAlbums = this.dataGridViewAlbums.Tag as Dictionary<string, AlbumEx>;

            if (distinctAlbums == null)
            {
                distinctAlbums = new Dictionary<string, AlbumEx>();
            }

            if (distinctAlbums.ContainsKey(track.Album.Href))
            {
                foreach (DataGridViewRow row in this.dataGridViewAlbums.Rows)
                {
                    AlbumEx album = row.Tag as AlbumEx;
                    if (album != null && album.Href.Equals(track.Album.Href))
                    {
                        int numTracks = int.Parse(row.Cells[(int)AlbumColumns.NumberOfTracks].Value.ToString());
                        row.Cells[(int)AlbumColumns.NumberOfTracks].Value = numTracks + 1;
                        break;
                    }
                }
            }
            else
            {
                AlbumEx album = new AlbumEx(track);
                distinctAlbums.Add(track.Album.Href, album);

                if (this.dataGridViewAlbums.Rows.Count == 0)
                {
                    this.dataGridViewAlbums.Rows.Add(string.Format("All ({0} album{1})", distinctAlbums.Count(), distinctAlbums.Count() == 1 ? string.Empty : "s"));
                }
                else
                {
                    this.dataGridViewAlbums[(int)AlbumColumns.Name, 0].Value = string.Format("All ({0} album{1})", distinctAlbums.Count(), distinctAlbums.Count() == 1 ? string.Empty : "s");
                }

                DataGridViewRow newRow = new DataGridViewRow();
                object released = track.Album.Released;
                if (track.Album.Released == 0)
                {
                    released = null;
                }

                newRow.CreateCells(this.dataGridViewAlbums, track.Album.Name, album.Tracks.Count, released);
                newRow.Tag = album;

                int index = 1;
                foreach (DataGridViewRow row in this.dataGridViewAlbums.Rows)
                {
                    if (row.Index == 0)
                    {
                        continue;
                    }

                    index = row.Index;
                    string albumName = row.Cells[(int)AlbumColumns.Name].Value.ToString();
                    if (string.Compare(track.Album.Name, albumName) < 0)
                    {
                        break;
                    }
                }

                this.dataGridViewAlbums.Rows.Insert(index, newRow);
            }

            this.dataGridViewAlbums.Tag = distinctAlbums;
        }

        /// <summary>
        /// Populates the artist filters
        /// </summary>
        /// <param name="selectedArtistHrefs">The hypertext reference of the selected artist, null if no selection exists</param>
        private void PopulateAlbumFilter(List<string> selectedArtistHrefs = null)
        {
            if (selectedArtistHrefs == null || !selectedArtistHrefs.Any())
            {
                // Don't trigger any selection change events
                this.dataGridViewAlbums.SelectionChanged -= this.dataGridViewAlbums_SelectionChanged;

                this.dataGridViewAlbums.Rows.Clear();

                var distinctAlbums = from t in this.trackDataSet
                                     group t by t.Album.Href into g
                                     orderby g.First().Album.Released descending
                                     group g by g.Count() > 4 into g2
                                     let orderValue = g2.Key ? 1 : 0
                                     select g2;

                Dictionary<string, AlbumEx> distinctAlbumList = new Dictionary<string, AlbumEx>();
                if (distinctAlbums.Count() > 0)
                {
                    int totalAlbums = distinctAlbums.Sum(a => a.Count());
                    this.dataGridViewAlbums.Rows.Add(string.Format("All ({0} album{1})", totalAlbums, totalAlbums == 1 ? string.Empty : "s"));

                    foreach (var group in distinctAlbums)
                    {
                        foreach (var distinctAlbum in group)
                        {
                            AlbumEx album = new AlbumEx(distinctAlbum.ToList());
                            distinctAlbumList.Add(album.Href, album);

                            TrackEx track = distinctAlbum.First();
                            DataGridViewRow row = new DataGridViewRow();
                            row.CreateCells(this.dataGridViewAlbums, track.Album.Name, album.Tracks.Count, track.Album.Released);
                            row.Tag = album;
                            this.dataGridViewAlbums.Rows.Add(row);
                        }
                    }

                    this.dataGridViewAlbums.SelectionChanged += this.dataGridViewAlbums_SelectionChanged;
                }

                this.dataGridViewAlbums.Tag = distinctAlbumList;
            }
            else
            {
                int totalAlbums = 0;
                foreach (DataGridViewRow row in this.dataGridViewAlbums.Rows)
                {
                    if (row.Index == 0)
                    {
                        continue;
                    }

                    List<string> albumArtistHrefs = (row.Tag as AlbumEx).Tracks.Select(t => t.Artist.Href).Distinct().ToList();
                    int numTracks = (row.Tag as AlbumEx).Tracks.Count(t => selectedArtistHrefs.Contains(t.Artist.Href));
                    bool show = numTracks > 0;
                    row.Visible = show;
                    if (show)
                    {
                        totalAlbums++;
                        row.Cells[(int)AlbumColumns.NumberOfTracks].Value = numTracks;
                    }
                }

                this.dataGridViewAlbums[(int)AlbumColumns.Name, 0].Value = string.Format("All ({0} album{1})", totalAlbums, totalAlbums == 1 ? string.Empty : "s");
            }
        }
    }
}
