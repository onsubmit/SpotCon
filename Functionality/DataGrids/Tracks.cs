//-----------------------------------------------------------------------
// <copyright file="Tracks.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using SpotCon.DataStructures;
    using SpotCon.Enums;

    /// <summary>
    /// Methods for the tracks data grid
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Reference to the track data grid scroll bar -- used for lazy initlization
        /// </summary>
        private ScrollBar trackScrollBarLazy;

        /// <summary>
        /// List of track data grid scroll bar indices that have triggered a search
        /// </summary>
        private List<int> queuedIndices = new List<int>();

        /// <summary>
        /// Tracks the list of selected track row indices
        /// </summary>
        private List<int> selectedTrackRowIndices = new List<int>();

        /// <summary>
        /// Popularity images
        /// </summary>
        private ImageList popImages = new ImageList();

        /// <summary>
        /// Gets a reference to the track data grid scroll bar
        /// </summary>
        private ScrollBar trackScrollBar
        {
            get
            {
                if (this.trackScrollBarLazy == null)
                {
                    Type type = this.dataGridViewTracks.GetType();
                    PropertyInfo pi = type.GetProperty("VerticalScrollBar", BindingFlags.Instance | BindingFlags.NonPublic);
                    this.trackScrollBarLazy = pi.GetValue(this.dataGridViewTracks, null) as ScrollBar;
                }

                return this.trackScrollBarLazy;
            }
        }

        /// <summary>
        /// dataGridViewTracks SelectionChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewTracks.SelectedRows.Count == 0)
            {
                return;
            }

            List<int> newSelectedTrackRowIndices = new List<int>();
            for (int i = 0; i < dataGridViewTracks.SelectedRows.Count; i++)
            {
                DataGridViewRow row = dataGridViewTracks.SelectedRows[i];
                DataGridViewImageCell pop = (DataGridViewImageCell)row.Cells[(int)TrackColumns.Popularity];
                pop.Value = this.GetPopularityImage((double)pop.Tag);

                newSelectedTrackRowIndices.Add(row.Index);
            }

            this.selectedTrackRowIndices.RemoveAll(x => newSelectedTrackRowIndices.Contains(x));
            foreach (int i in this.selectedTrackRowIndices)
            {
                if (i < dataGridViewTracks.Rows.Count)
                {
                    DataGridViewRow row = dataGridViewTracks.Rows[i];
                    DataGridViewImageCell pop = (DataGridViewImageCell)row.Cells[(int)TrackColumns.Popularity];
                    pop.Value = this.GetPopularityImage((double)pop.Tag);
                }
            }

            this.selectedTrackRowIndices = newSelectedTrackRowIndices;
        }

        /// <summary>
        /// dataGridViewTracks CellDoubleClick event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            this.PlayTrackset(e.RowIndex);
        }

        /// <summary>
        /// dataGridViewTracks RowLeave event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewImageCell pop = (DataGridViewImageCell)dataGridViewTracks[(int)TrackColumns.Popularity, e.RowIndex];
                pop.Value = this.GetPopularityImage((double)pop.Tag);
            }
        }

        /// <summary>
        /// dataGridViewTracks CellContentClick event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == (int)TrackColumns.Artist)
                {
                    this.queuedIndices.Clear();
                    this.searchQueue.Enqueue(new Tuple<string, int?>(dataGridViewTracks[e.ColumnIndex, e.RowIndex].Value.ToString(), null));
                    this.Search(SearchType.Artist);
                }
                else if (e.ColumnIndex == (int)TrackColumns.Album)
                {
                    string uri = (this.dataGridViewTracks.Rows[e.RowIndex].Tag as TrackEx).Album.Href;
                    string name = this.dataGridViewTracks[(int)TrackColumns.Track, e.RowIndex].Value.ToString();
                    this.LookupAlbum(uri, (this.dataGridViewTracks.Rows[e.RowIndex].Tag as TrackEx).Href, name);
                }
            }
        }

        /// <summary>
        /// dataGridViewTracks CellMouseEnter event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewTracks.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewTracks.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.FromArgb(28, 28, 31);

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(28, 28, 31);
                }
            }
        }

        /// <summary>
        /// dataGridViewTracks CellMouseLeave event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < this.dataGridViewTracks.Rows.Count)
            {
                DataGridViewRow row = this.dataGridViewTracks.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = this.dataGridViewTracks.DefaultCellStyle.BackColor;

                if (row.Selected)
                {
                    row.DefaultCellStyle.SelectionBackColor = this.dataGridViewTracks.DefaultCellStyle.SelectionBackColor;
                }
            }
        }

        /// <summary>
        /// dataGridViewTracks ColumnHeaderMouseClick event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.SortTracklist(e.ColumnIndex);
        }

        /// <summary>
        /// dataGridViewTracks Scroll event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
            {
                if (!this.queuedIndices.Contains(this.trackScrollBar.Maximum) && this.trackScrollBar.Value > (double)this.trackScrollBar.Maximum * 0.9)
                {
                    string query = this.dataGridViewTracks.Tag as string;

                    if (!string.IsNullOrEmpty(query))
                    {
                        int page = 2;
                        if (this.trackScrollBar.Tag == null)
                        {
                            this.trackScrollBar.Tag = page;
                        }
                        else
                        {
                            page = (int)this.trackScrollBar.Tag;
                            this.trackScrollBar.Tag = ++page;
                        }

                        this.queuedIndices.Add(this.trackScrollBar.Maximum);
                        this.searchQueue.Enqueue(new Tuple<string, int?>(query, page));
                        this.Search();
                    }
                }
            }
        }

        /// <summary>
        /// dataGridViewTracks DragEnter event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        /// <summary>
        /// dataGridViewTracks DragDrop event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewTracks_DragDrop(object sender, DragEventArgs e)
        {
            string contents = e.Data.GetData(DataFormats.Text).ToString();
            IEnumerable<string> trackList = contents.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            trackList = trackList.Where(l => l.StartsWith("http://open.spotify.com/track/")).Select(l => l.Replace("http://open.spotify.com/track/", string.Empty));

            this.ImportTrackList(trackList);
        }

        /// <summary>
        /// toolStripMenuItemFindDuplicates Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void toolStripMenuItemFindDuplicates_Click(object sender, EventArgs e)
        {
            var tuples = new List<Tuple<double, DataGridViewRow, DataGridViewRow>>();
            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };
            bw.DoWork += (bwSender, bwArgs) =>
            {
                for (int i = 0; i < this.dataGridViewTracks.Rows.Count - 1; i++)
                {
                    bw.ReportProgress(0, i);
                    for (int j = i + 1; j < this.dataGridViewTracks.Rows.Count; j++)
                    {
                        object track1 = this.dataGridViewTracks.Rows[i].Tag;
                        object track2 = this.dataGridViewTracks.Rows[j].Tag;
                        double similarity = TrackComparer.Compare(track1, track2);
                        tuples.Add(new Tuple<double, DataGridViewRow, DataGridViewRow>(similarity, this.dataGridViewTracks.Rows[i], this.dataGridViewTracks.Rows[j]));
                    }
                }
            };

            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                int count = (int)bwArgs.UserState + 1;
                this.SetProgressBar(count, this.dataGridViewTracks.Rows.Count);
                this.SetStatus(string.Format(Properties.Resources.FindingDuplicates, count, this.dataGridViewTracks.Rows.Count));
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.ClearStatus();
                this.HideProgressBar();
                this.SetBusyState(false);

                tuples = tuples.Where(t => t.Item1 > 80).OrderBy(t => t.Item1).Reverse().ToList();

                if (tuples.Any())
                {
                    this.dataGridViewTracks.Rows.Clear();
                    foreach (var tuple in tuples)
                    {
                        if (!this.dataGridViewTracks.Rows.Contains(tuple.Item2))
                        {
                            this.dataGridViewTracks.Rows.Add(tuple.Item2);
                        }

                        if (!this.dataGridViewTracks.Rows.Contains(tuple.Item3))
                        {
                            this.dataGridViewTracks.Rows.Add(tuple.Item3);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(Properties.Resources.NoDuplicatesFound, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            this.SetBusyState(true);
            bw.RunWorkerAsync();
        }

        /// <summary>
        /// toolStripMenuItemPlay Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void toolStripMenuItemPlay_Click(object sender, EventArgs e)
        {
            List<int> indices = new List<int>();
            foreach (DataGridViewRow row in this.dataGridViewTracks.SelectedRows)
            {
                indices.Add(row.Index);
            }

            if (indices.Any())
            {
                this.PlayTrackset(indices.Min());
            }
        }

        /// <summary>
        /// toolStripMenuItemPlay Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void toolStripMenuItemPlaySelected_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewTracks.SelectedRows.Count == 0)
            {
                return;
            }

            List<int> indices = new List<int>();
            foreach (DataGridViewRow row in this.dataGridViewTracks.SelectedRows)
            {
                indices.Add(row.Index);
            }

            List<string> tracks = new List<string>();
            foreach (int i in indices.OrderBy(i => i))
            {
                tracks.Add((this.dataGridViewTracks.Rows[i].Tag as TrackEx).Href.Replace("spotify:track:", string.Empty));
            }

            if (tracks.Any())
            {
                string trackset = "spotify:trackset:SpotCon:" + string.Join(",", tracks);
                this.SendToServer(trackset + "|Play");
            }
        }

        /// <summary>
        /// Populates the track information
        /// </summary>
        /// <param name="selectedTrackHref">The hypertext reference of the selected track, null if no selection exists</param>
        /// <param name="append">True to append the new tracks -- used when scrolling to the bottom and initiating a page 'n' search</param>
        private void PopulateTracks(string selectedTrackHref = null, bool append = false)
        {
            if (!append)
            {
                this.dataGridViewTracks.Rows.Clear();
            }

            int rows = this.dataGridViewTracks.Rows.Count;

            int numDiscs = 0;
            this.trackDataSetFiltered.ForEach(t =>
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(this.dataGridViewTracks, t.DiscNumber == numDiscs ? string.Empty : t.DiscNumber.ToString(), t.TrackNumber, t.Name, t.Artist.Name, TimeSpan.FromSeconds(t.Length).ToString(@"m\:ss"), this.GetPopularityImage(t.Popularity), t.Album.Name);
                row.Cells[(int)TrackColumns.Popularity].Tag = t.Popularity;
                row.Cells[(int)TrackColumns.Time].Tag = t.Length;
                row.Tag = t;
                row.Height = 42;
                numDiscs = Math.Max(numDiscs, t.DiscNumber);

                int index = this.dataGridViewTracks.Rows.Add(row);
                row.Selected = t.Href == selectedTrackHref;
            });

            this.dataGridViewTracks.Columns[(int)TrackColumns.Disc].Visible = numDiscs > 1;
        }

        /// <summary>
        /// Sorts the track list
        /// </summary>
        /// <param name="column">Column to sort on</param>
        /// <param name="sortOrder">Order of the sort</param>
        private void SortTracklist(int column, SortOrder? sortOrder = null)
        {
            Type type = typeof(string);
            switch (column)
            {
                case (int)TrackColumns.Album:
                case (int)TrackColumns.Artist:
                case (int)TrackColumns.Track:
                    type = typeof(string);
                    break;
                case (int)TrackColumns.Popularity:
                case (int)TrackColumns.Time:
                    type = typeof(double);
                    break;
                case (int)TrackColumns.TrackNumber:
                    type = typeof(int);
                    break;
            }

            // Remove sort glyph from all other headers
            foreach (DataGridViewColumn col in dataGridViewTracks.Columns)
            {
                if (col.Index != column)
                {
                    col.HeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                }
            }

            if (!sortOrder.HasValue)
            {
                sortOrder = dataGridViewTracks.Columns[column].HeaderCell.SortGlyphDirection;
                if ((column == (int)TrackColumns.TrackNumber && sortOrder == SortOrder.None) ||
                    sortOrder == System.Windows.Forms.SortOrder.Descending)
                {
                    sortOrder = System.Windows.Forms.SortOrder.Ascending;
                }
                else
                {
                    sortOrder = System.Windows.Forms.SortOrder.Descending;
                }
            }

            dataGridViewTracks.Sort(new DataGridViewRowComparer(column, sortOrder.Value, type));
            dataGridViewTracks.Columns[column].HeaderCell.SortGlyphDirection = sortOrder.Value;

            foreach (DataGridViewRow row in this.dataGridViewTracks.Rows)
            {
                if (row.Visible)
                {
                    row.Cells[(int)TrackColumns.Popularity].Style = new DataGridViewCellStyle(row.DefaultCellStyle) { Alignment = DataGridViewContentAlignment.MiddleCenter };
                }
            }
        }

        /// <summary>
        /// Plays the set of tracks starting at the given index through the end of the search results
        /// </summary>
        /// <param name="startRowIndex">Starting row index</param>
        private void PlayTrackset(int startRowIndex)
        {
            List<string> tracks = new List<string>();
            for (int i = startRowIndex; i < this.dataGridViewTracks.Rows.Count; i++)
            {
                tracks.Add((this.dataGridViewTracks.Rows[i].Tag as TrackEx).Href.Replace("spotify:track:", string.Empty));
            }

            string trackset = "spotify:trackset:SpotCon:" + string.Join(",", tracks);
            this.SendToServer(trackset + "|Play");
        }

        /// <summary>
        /// Gets the popularity image
        /// </summary>
        /// <param name="popularity">Track popularity value</param>
        /// <returns>Desired popularity image</returns>
        private Image GetPopularityImage(double? popularity)
        {
            int numImages = this.popImages.Images.Count - 1;
            int index = popularity.HasValue ? (int)((double)numImages * popularity) : 0;
            return this.popImages.Images[index];
        }
    }
}
