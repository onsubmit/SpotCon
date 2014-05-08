//-----------------------------------------------------------------------
// <copyright file="Search.cs" company="Andy Young">
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
    using System.Windows.Forms;
    using SpotCon.DataStructures;
    using SpotCon.Enums;
    using SpotifyWebSharp.SpotifyResponses.Search;
    using SpotifyWebSharp.SpotifyServices;

    /// <summary>
    /// Search functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Spotify Search service
        /// </summary>
        private Lazy<SearchService> search = new Lazy<SearchService>(() => { return new SearchService(); });

        /// <summary>
        /// Queue of terms/page to search for
        /// </summary>
        private Queue<Tuple<string, int?>> searchQueue = new Queue<Tuple<string, int?>>();

        /// <summary>
        /// Tracks that couldn't be immediately identified by the Search service
        /// </summary>
        private Queue<FindNewTracksThreadStatus> unknownTracks = new Queue<FindNewTracksThreadStatus>();

        /// <summary>
        /// Tracks whose identification process was skipped
        /// </summary>
        private List<string> skippedTracks = new List<string>();

        /// <summary>
        /// textBoxSearch Enter event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void textBoxSearch_Enter(object sender, EventArgs e)
        {
            if (this.textBoxSearch.Text == "Search")
            {
                this.textBoxSearch.Text = string.Empty;
                this.textBoxSearch.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
            }
        }

        /// <summary>
        /// textBoxSearch Leave event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void textBoxSearch_Leave(object sender, EventArgs e)
        {
            this.panelSearch.BackgroundImage = Properties.Resources.Search;
            if (string.IsNullOrEmpty(this.textBoxSearch.Text))
            {
                this.textBoxSearch.Text = "Search";
                this.textBoxSearch.ForeColor = (Color)this.textBoxSearch.Tag;
            }
        }

        /// <summary>
        /// textBoxSearch KeyDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void textBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.queuedIndices.Clear();
                this.searchQueue.Enqueue(new Tuple<string, int?>(textBoxSearch.Text, null));
                this.Search();
                e.SuppressKeyPress = true;
                this.ActiveControl = this.panelPlayPause;
            }
        }

        /// <summary>
        /// Searches the Spotify database
        /// </summary>
        /// <param name="searchType">Type of search. Used in filtering the results</param>
        /// <param name="fromHistory">Indicates whether the method is being invoked from the history</param>
        private void Search(SearchType? searchType = null, bool fromHistory = false)
        {
            Tuple<string, int?> input = this.searchQueue.Dequeue();
            string query = input.Item1;
            int? page = input.Item2;

            if (!page.HasValue)
            {
                // Reset the page counter when a fresh search comes in
                this.trackScrollBar.Tag = null;
            }

            Action action = null;
            if (query.StartsWith("spotify:"))
            {
                action = new Action(() => this.SendToServer(query + "|Play"));
            }
            else if (query.StartsWith("http://open.spotify.com"))
            {
                action = new Action(() => this.SendToServer(query + "|Play"));
            }
            else
            {
                if (!fromHistory && !page.HasValue)
                {
                    this.AddHistoryAction(() =>
                    {
                        this.searchQueue.Enqueue(new Tuple<string, int?>(query, null));
                        this.Search(searchType, fromHistory: true);
                    });
                }

                this.SetStatusBusy(string.Format(page.HasValue ? Properties.Resources.SearchingPage : Properties.Resources.Searching, query, page));
            }

            BackgroundWorker bw = new BackgroundWorker() { WorkerReportsProgress = true };
            bw.DoWork += (bwSender, bwArgs) =>
            {
                if (action != null)
                {
                    action.Invoke();
                }
                else
                {
                    List<SearchTrack> tracks = search.Value.SearchTracks(query, page).TrackList;
                    if (searchType.HasValue)
                    {
                        switch (searchType.Value)
                        {
                            case SearchType.Album:
                                tracks = tracks.Where(t => t.Album.Name.Equals(query, StringComparison.OrdinalIgnoreCase)).ToList();
                                break;
                            case SearchType.Artist:
                                tracks = tracks.Where(t => t.Artist.Name.Equals(query, StringComparison.OrdinalIgnoreCase)).ToList();
                                break;
                            case SearchType.Track:
                                tracks = tracks.Where(t => t.Name.Equals(query, StringComparison.OrdinalIgnoreCase)).ToList();
                                break;
                        }
                    }

                    if (!page.HasValue)
                    {
                        this.trackDataSet.Clear();
                    }

                    this.trackDataSet.AddRange(tracks.Select(t => new TrackEx(t)).ToList());
                    this.trackDataSetFiltered = this.GroupAndSortTracks(this.trackDataSet);
                }
            };

            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                Tuple<int, int> status = bwArgs.UserState as Tuple<int, int>;
                this.SetProgressBar(status.Item1, status.Item2);
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.dataGridViewTracks.Tag = query;
                this.PopulateTracks(append: page.HasValue && page.Value > 1);
                this.PopulateArtistFilter();
                this.PopulateAlbumFilter();

                if (!this.searchQueue.Any())
                {
                    this.SetBusyState(false);
                    this.SetStatus(string.Format(Properties.Resources.DisplayResults, this.trackDataSet.Count, this.trackDataSet.Count == 1 ? string.Empty : "s", query));
                }
                else
                {
                    this.Search();
                }
            };

            bw.RunWorkerAsync();
        }
    }
}
