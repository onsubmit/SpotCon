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
    using System.IO;
    using System.Linq;
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
    /// Track methods
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Spotify Lookup service
        /// </summary>
        private Lazy<LookupService> lookup = new Lazy<LookupService>(() => { return new LookupService(); });

        /// <summary>
        /// Tooltip for track
        /// </summary>
        private ToolTip tooltipTrack = new ToolTip();

        /// <summary>
        /// Tooltip for artist
        /// </summary>
        private ToolTip tooltipArtist = new ToolTip();

        /// <summary>
        /// Complete list of tracks returned by a search, lookup, or import
        /// </summary>
        private List<TrackEx> trackDataSet = new List<TrackEx>();

        /// <summary>
        /// Filtered list of tracks to be displayed in the tracks data grid
        /// </summary>
        private List<TrackEx> trackDataSetFiltered = new List<TrackEx>();

        /// <summary>
        /// linkLabelTrack MouseHover event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void linkLabelTrack_MouseHover(object sender, EventArgs e)
        {
            this.tooltipTrack.Show(linkLabelTrack.Text, this.linkLabelTrack);
        }

        /// <summary>
        /// linkLabelArtist MouseHover event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void linkLabelArtist_MouseHover(object sender, EventArgs e)
        {
            this.tooltipArtist.Show(linkLabelArtist.Text, this.linkLabelArtist);
        }

        /// <summary>
        /// Imports a list of tracks
        /// </summary>
        /// <param name="trackList">List of tracks to import</param>
        private void ImportTrackList(IEnumerable<string> trackList)
        {
            BackgroundWorker bw = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            this.trackDataSet.Clear();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                int i = 0;
                foreach (string href in trackList)
                {
                    if (bw.CancellationPending)
                    {
                        return;
                    }

                    bw.ReportProgress(0, new Tuple<int, int>(++i, trackList.Count()));

                    Track track = null;
                    if (cachedLookupTracks.ContainsKey(href))
                    {
                        track = cachedLookupTracks[href];
                    }
                    else
                    {
                        string path = Path.Combine(SpotConForm.AppDataFolder, "Lookup", "Tracks", href + ".xml");
                        if (File.Exists(path))
                        {
                            track = SpotifyService.Deserialize<Track>(File.ReadAllText(path));
                            track.Href = "spotify:track:" + href;
                        }
                        else
                        {
                            track = lookup.Value.LookupTrack(href);
                            SpotifyService.Serialize(track, path);
                            track.Href = "spotify:track:" + href;
                        }

                        this.cachedLookupTracks[href] = track;
                    }

                    TrackEx trackEx = new TrackEx(track);
                    this.trackDataSet.Add(trackEx);
                    bw.ReportProgress(0, trackEx);
                }
            };

            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                if (bwArgs.UserState is Tuple<int, int>)
                {
                    Tuple<int, int> status = bwArgs.UserState as Tuple<int, int>;
                    this.SetStatus(string.Format(Properties.Resources.GatheringTrackInfo, status.Item1, status.Item2));
                    this.SetProgressBar(status.Item1);
                }
                else if (bwArgs.UserState is TrackEx)
                {
                    TrackEx t = bwArgs.UserState as TrackEx;
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(this.dataGridViewTracks, t.DiscNumber, t.TrackNumber, t.Name, t.Artist.Name, TimeSpan.FromSeconds((double)t.Length).ToString(@"m\:ss"), this.GetPopularityImage(t.Popularity), t.Album.Name);
                    row.Cells[(int)TrackColumns.Popularity].Tag = t.Popularity;
                    row.Cells[(int)TrackColumns.Time].Tag = t.Length;
                    row.Tag = t;
                    row.Height = 42;

                    this.dataGridViewTracks.Rows.Add(row);
                    this.PopulateArtistFilter(t);
                    this.PopulateAlbumFilter(t);
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.HideProgressBar();
                this.ClearStatus();
            };

            this.dataGridViewAlbums.Rows.Clear();
            this.dataGridViewArtists.Rows.Clear();
            this.dataGridViewTracks.Rows.Clear();

            this.dataGridViewArtists.Tag = null;
            this.dataGridViewAlbums.Tag = null;

            if (trackList.Any())
            {
                this.SetProgressBar(0, trackList.Count());
                bw.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Groups and sorts the track information
        /// </summary>
        /// <param name="tracks">Tracks to group and sort</param>
        /// <returns>Grouped and filtered list of tracks</returns>
        private List<TrackEx> GroupAndSortTracks(List<TrackEx> tracks)
        {
            List<TrackEx> filtered = new List<TrackEx>();
            //// 1. Sort the tracks by album release date (descending)
            //// 2. Sort the tracks by track number (increasing)
            //// 3. Group the tracks by album
            //// 4. Put the albums with less than five tracks at the bottom of the list
            tracks = tracks.OrderByDescending(t => t.Album.Released).ThenBy(t => t.TrackNumber).ToList();

            var groupedTracks = from t in tracks
                                group t by t.Album.Href into albumGroup
                                group albumGroup by albumGroup.Count() > 4 into bigAlbumGroup
                                orderby bigAlbumGroup.Key ? 1 : 0
                                select bigAlbumGroup;

            foreach (var bigAlbumGroup in groupedTracks)
            {
                foreach (var album in bigAlbumGroup)
                {
                    var discQuery = from track in album
                                    group track by track.DiscNumber into discGroup
                                    orderby discGroup.Key ascending
                                    select discGroup;

                    foreach (var disc in discQuery)
                    {
                        filtered.AddRange(disc.ToList());
                    }
                }
            }

            return filtered;
        }

        /// <summary>
        /// Finds new tracks from the given playlist import plugin
        /// </summary>
        /// <param name="plugin">Playlist import plugin</param>
        private void FindNewTracks(IPlaylistImporter plugin)
        {
            BackgroundWorker bw = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            bw.DoWork += (bwSender, bwArgs) =>
            {
                Regex regex = null;
                try
                {
                    regex = new Regex(plugin.ArtistTrackRegex);
                }
                catch (ArgumentException aex)
                {
                    FindNewTracksThreadStatus status = new FindNewTracksThreadStatus()
                    {
                        Message = aex.Message,
                        Status = FindNewTracksStatus.InvalidRegex
                    };

                    bw.ReportProgress(0, status);
                    return;
                }

                // Distinct tracks only
                plugin.Tracks = plugin.Tracks.Distinct().ToList();
                for (int i = 0; !bw.CancellationPending && i < plugin.Tracks.Count; i++)
                {
                    bw.ReportProgress(0, new FindNewTracksThreadStatus() { Counter = i, Maximum = plugin.Tracks.Count });

                    Match match = regex.Match(plugin.Tracks[i]);
                    FindNewTracksThreadStatus status = new FindNewTracksThreadStatus()
                    {
                        Counter = i,
                        Maximum = plugin.Tracks.Count,
                        Message = plugin.Tracks[i],
                        Status = FindNewTracksStatus.AddNewTrack
                    };

                    if (!match.Success)
                    {
                        status.Status = FindNewTracksStatus.ParseError;
                        bw.ReportProgress(0, status);
                        continue;
                    }

                    if (!bw.CancellationPending)
                    {
                        status.ArtistName = match.Groups["ARTIST"].Value.Trim();
                        status.TrackName = match.Groups["TRACK"].Value.Trim();

                        this.GetArtist(status);
                        this.GetTrack(status);

                        bw.ReportProgress(0, status);
                    }
                }
            };

            bw.ProgressChanged += (bwSender, bwArgs) =>
            {
                FindNewTracksThreadStatus status = bwArgs.UserState as FindNewTracksThreadStatus;

                this.SetStatus(string.Format("Looking up track {0} of {1}...", status.Counter + 1, status.Maximum));
                this.SetProgressBar(status.Counter);

                switch (status.Status)
                {
                    case FindNewTracksStatus.InvalidRegex:
                        if (DialogResult.Cancel == MessageBox.Show(status.Message, "Regex error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                        {
                            bw.CancelAsync();
                        }

                        break;
                    case FindNewTracksStatus.ParseError:
                        if (DialogResult.Cancel == MessageBox.Show(string.Format("Could not parse song information from <{0}>", status.Message), "Parse error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                        {
                            bw.CancelAsync();
                        }

                        break;
                    case FindNewTracksStatus.UnknownArtist:
                    case FindNewTracksStatus.UnknownTrack:
                        unknownTracks.Enqueue(status);
                        break;
                    case FindNewTracksStatus.AddNewTrack:
                        this.AddNewTrack(status);
                        break;
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.ClearStatus();
                this.HideProgressBar();
                this.SetBusyState(false);

                if (this.unknownTracks.Any())
                {
                    this.GetUnknownInfo(this.unknownTracks.Dequeue());
                }
                else
                {
                    this.FindNewTracksEnd();
                }
            };

            this.SetBusyState(true);
            this.unknownTracks.Clear();
            this.dataGridViewTracks.Rows.Clear();
            this.SetProgressBar(1, plugin.Tracks.Count);
            bw.RunWorkerAsync(plugin.Tracks);
        }

        /// <summary>
        /// Runs after the user is done finding new tracks
        /// </summary>
        private void FindNewTracksEnd()
        {
            if (this.skippedTracks.Any() &&
                DialogResult.Yes == MessageBox.Show(
                    Properties.Resources.SkippedTracksClipboard,
                    Properties.Resources.SkippedTracksClipboardCaption,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning))
            {
                Clipboard.SetText(string.Join(Environment.NewLine, this.skippedTracks));
            }

            this.SaveArtistCache();
            this.SaveSearchTrackCache();
            this.SaveMisspelledArtistCache();
            this.SaveMisspelledTrackCache();
            this.SetBusyState(false);
        }

        /// <summary>
        /// Lookups the given album
        /// </summary>
        /// <param name="query">Album href</param>
        /// <param name="selectedTrackHref">Href of track to select</param>
        /// <param name="friendlyName">The query to display in the status bar</param>
        /// <param name="fromHistory">Indicates whether the method is being invoked from the history</param>
        private void LookupAlbum(string query, string selectedTrackHref, string friendlyName, bool fromHistory = false)
        {
            if (string.IsNullOrEmpty(query))
            {
                return;
            }

            if (!fromHistory)
            {
                string queryCopy = string.Copy(query);
                string selectedTrackHrefCopy = string.Copy(selectedTrackHref);
                this.AddHistoryAction(() => this.LookupAlbum(queryCopy, selectedTrackHrefCopy, friendlyName, fromHistory: true));
            }

            this.SetStatus(string.Format(Properties.Resources.Searching, friendlyName));
            this.dataGridViewTracks.IgnoreSelectionChangesAndClearRows();
            this.dataGridViewTracks.IgnoreSelectionChangesAndClearRows();
            this.dataGridViewTracks.IgnoreSelectionChangesAndClearRows();
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (bwSender, bwArgs) =>
            {
                query = query.Split(':')[2];

                Album album = null;
                string albumPath = Path.Combine(SpotConForm.AppDataFolder, "Lookup", "Albums", query + ".xml");
                if (this.cachedLookupAlbums.ContainsKey(query))
                {
                    album = this.cachedLookupAlbums[query];
                }
                else if (File.Exists(albumPath))
                {
                    album = SpotifyService.Deserialize<Album>(File.ReadAllText(albumPath));
                }

                if (album == null || album.Tracks.Count == 0)
                {
                    album = lookup.Value.LookupAlbum(query, LookupService.AlbumExtras.TrackDetail);
                    SpotifyService.Serialize(album, albumPath);
                }

                album.Href = "spotify:album:" + query;

                this.trackDataSet = (new AlbumEx(album)).Tracks;
                this.trackDataSetFiltered = this.GroupAndSortTracks(this.trackDataSet);
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                this.SetStatus(string.Format(Properties.Resources.DisplayResults, this.trackDataSetFiltered.Count, this.trackDataSetFiltered.Count == 1 ? string.Empty : "s", friendlyName));
                this.PopulateTracks(selectedTrackHref);
                this.PopulateArtistFilter();
                this.PopulateAlbumFilter();
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Gets missing info when a search query doesn't return an exact match
        /// </summary>
        /// <param name="unknownTrack">FindNewTracks thread status</param>
        private void GetUnknownInfo(FindNewTracksThreadStatus unknownTrack)
        {
            if (unknownTrack.Status == FindNewTracksStatus.UnknownArtist)
            {
                this.GetUnknownArtist(unknownTrack);
            }
            else if (unknownTrack.Status == FindNewTracksStatus.UnknownTrack)
            {
                this.GetUnknownTrackThreadSafe(unknownTrack);
            }
        }

        /// <summary>
        /// Gets track information when no exact match is found for the track
        /// </summary>
        /// <param name="unknownTrack">FindNewTracks thread status</param>
        private void GetUnknownTrack(FindNewTracksThreadStatus unknownTrack)
        {
            List<SearchTrack> trackList = unknownTrack.Tracks.TrackList;
            var artistQuery = unknownTrack.Tracks.TrackList.Where(t => t.Artist.Href == unknownTrack.ArtistHref);
            if (artistQuery.Any())
            {
                trackList = artistQuery.ToList();
            }

            string originalTrack = unknownTrack.TrackName;
            UnknownTrack ut = new UnknownTrack(unknownTrack.ArtistName, unknownTrack.ArtistHref, unknownTrack.TrackName, trackList);
            if (ut.ShowDialog(this) == DialogResult.Cancel)
            {
                unknownTrack.Status = FindNewTracksStatus.Cancel;
                this.FindNewTracksEnd();
                return;
            }

            if (ut.SelectedTrack != null)
            {
                unknownTrack.Track = ut.SelectedTrack;
                string misspelledKey = string.Format("{0}:::{1}", unknownTrack.ArtistHref, originalTrack);

                if (unknownTrack.Track.Original is Track)
                {
                    Track track = unknownTrack.Track.Original as Track;
                    this.misspelledTracks[misspelledKey] = track.Name;
                    this.cachedLookupTracks.Add(track.Id.Value, track);

                    string path = Path.Combine(SpotConForm.AppDataFolder, "Lookup", "Tracks", track.Href.Replace("spotify:track:", string.Empty) + ".xml");
                    SpotifyService.Serialize(track, path);
                }
                else if (unknownTrack.Track.Original is SearchTrack)
                {
                    XDocument doc = new XDocument();
                    SearchTrack track = unknownTrack.Track.Original as SearchTrack;
                    this.misspelledTracks[misspelledKey] = track.Name;

                    string path = Path.Combine(SpotConForm.AppDataFolder, "Search", unknownTrack.ArtistHref.Replace("spotify:artist:", string.Empty) + ".xml");

                    XElement trackNode = new XElement(
                        "Track",
                        new XAttribute("Query", unknownTrack.TrackName),
                        HttpUtility.HtmlEncode(SpotifyService.Serialize(track)));

                    if (File.Exists(path))
                    {
                        doc = XDocument.Load(path);
                        doc.Root.Add(trackNode);
                    }
                    else
                    {
                        doc = new XDocument(new XElement("Tracks", trackNode));
                    }

                    doc.Save(path);
                }
            }
            else
            {
                unknownTrack.Status = FindNewTracksStatus.TrackSkipped;
                this.skippedTracks.Add(unknownTrack.Message);
            }
        }

        /// <summary>
        /// Gets artist information when no exact match is found for the artist
        /// </summary>
        /// <param name="unknownTrack">FindNewTracks thread status</param>
        private void GetUnknownArtist(FindNewTracksThreadStatus unknownTrack)
        {
            if (unknownTrack.ArtistHref == null)
            {
                string originalArtist = unknownTrack.ArtistName;
                UnknownArtist ua = new UnknownArtist(unknownTrack.ArtistName, unknownTrack.Artists);
                if (ua.ShowDialog(this) == DialogResult.Cancel)
                {
                    unknownTrack.Status = FindNewTracksStatus.Cancel;
                    this.FindNewTracksEnd();
                    return;
                }

                if (ua.SelectedArtist != null)
                {
                    unknownTrack.Artist = ua.SelectedArtist;

                    unknownTrack.ArtistName = unknownTrack.Artist.Name;
                    unknownTrack.ArtistHref = unknownTrack.Artist.Href;
                    this.misspelledArtists[originalArtist] = unknownTrack.ArtistName;
                    this.cachedArtistHrefs[unknownTrack.ArtistName] = unknownTrack.Artist.Href;

                    this.GetUnknownTrackThreadSafe(unknownTrack);
                }
                else
                {
                    unknownTrack.Status = FindNewTracksStatus.TrackSkipped;
                    this.skippedTracks.Add(unknownTrack.Message);
                    if (this.unknownTracks.Any())
                    {
                        this.GetUnknownInfo(this.unknownTracks.Dequeue());
                    }
                    else
                    {
                        this.FindNewTracksEnd();
                    }
                }
            }
        }

        /// <summary>
        /// Gets track information
        /// </summary>
        /// <param name="unknownTrack">FindNewTracks thread status</param>
        private void GetUnknownTrackThreadSafe(FindNewTracksThreadStatus unknownTrack)
        {
            BackgroundWorker bw = new BackgroundWorker();

            bw.DoWork += (bwSender, bwArgs) =>
            {
                this.GetTrack(unknownTrack);

                if (unknownTrack.Tracks == null)
                {
                    unknownTrack.Tracks = this.search.Value.SearchTracks(unknownTrack.TrackName);
                }
            };

            bw.RunWorkerCompleted += (bwSender, bwArgs) =>
            {
                if (unknownTrack.Status != FindNewTracksStatus.TrackSkipped)
                {
                    this.GetUnknownTrack(unknownTrack);
                }

                if (unknownTrack.Status == FindNewTracksStatus.Cancel)
                {
                    return;
                }

                if (unknownTrack.Status != FindNewTracksStatus.TrackSkipped)
                {
                    this.AddNewTrack(unknownTrack);
                }

                if (this.unknownTracks.Any())
                {
                    this.GetUnknownInfo(this.unknownTracks.Dequeue());
                }
                else
                {
                    this.FindNewTracksEnd();
                }
            };

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Adds a new track to the track gird
        /// </summary>
        /// <param name="status">FindNewTracks thread status</param>
        private void AddNewTrack(FindNewTracksThreadStatus status)
        {
            DataGridViewRow row = new DataGridViewRow();
            row.CreateCells(this.dataGridViewTracks, status.Track.DiscNumber, status.Track.TrackNumber, status.Track.Name, status.Track.Artist.Name, TimeSpan.FromSeconds(status.Track.Length).ToString(@"m\:ss"), this.GetPopularityImage(status.Track.Popularity), status.Track.Album.Name);
            row.Cells[(int)TrackColumns.Popularity].Tag = status.Track.Popularity;
            row.Cells[(int)TrackColumns.Time].Tag = status.Track.Length;
            row.Tag = status.Track;
            row.Height = 42;

            this.dataGridViewTracks.Rows.Add(row);
        }

        /// <summary>
        /// Gets the Spotify artist
        /// </summary>
        /// <param name="status">Thread status object</param>
        private void GetArtist(FindNewTracksThreadStatus status)
        {
            string artistName = status.ArtistName;
            bool artistFound = this.cachedArtistHrefs.ContainsKey(artistName);

            if (!artistFound && this.misspelledArtists.ContainsKey(artistName))
            {
                artistName = this.misspelledArtists[artistName];
                artistFound = this.cachedArtistHrefs.ContainsKey(artistName);
            }

            if (artistFound)
            {
                status.ArtistHref = this.cachedArtistHrefs[artistName];
            }
            else
            {
                status.Artists = this.search.Value.SearchArtists(artistName);
                var artistQuery = status.Artists.ArtistList.Where(a => a.Name.Equals(artistName, System.StringComparison.OrdinalIgnoreCase));
                if (!artistQuery.Any())
                {
                    status.Status = FindNewTracksStatus.UnknownArtist;
                }
                else
                {
                    status.Artist = artistQuery.First();
                }

                if (status.Artist != null)
                {
                    this.cachedArtistHrefs[artistName] = status.Artist.Href;
                    status.ArtistName = status.Artist.Name;
                    status.ArtistHref = status.Artist.Href;
                }
            }
        }

        /// <summary>
        /// Gets the Spotify track
        /// </summary>
        /// <param name="status">Thread status object</param>
        private void GetTrack(FindNewTracksThreadStatus status)
        {
            if (status.ArtistHref == null)
            {
                return;
            }

            SearchTrack track = null;
            string trackName = status.TrackName;
            string key = string.Format("{0}:::{1}", status.ArtistHref, trackName);

            bool trackFound = this.cachedSearchTracks.ContainsKey(key);
            if (!trackFound && this.misspelledTracks.ContainsKey(key))
            {
                trackName = this.misspelledTracks[key];
                key = string.Format("{0}:::{1}", status.ArtistHref, trackName);
                trackFound = this.cachedSearchTracks.ContainsKey(key);
            }

            if (trackFound && (track = this.cachedSearchTracks[key]) != null)
            {
                status.Track = new TrackEx(track);
            }
            else
            {
                XDocument doc = null;
                string path = Path.Combine(SpotConForm.AppDataFolder, "Search", status.ArtistHref.Replace("spotify:artist:", string.Empty) + ".xml");
                if (File.Exists(path))
                {
                    doc = XDocument.Load(path);
                    var trackNode = from node in doc.Root.Elements("Track")
                                    where node.Attribute("Query").Value.Equals(trackName, StringComparison.OrdinalIgnoreCase)
                                    select node;

                    if (trackNode.Any())
                    {
                        track = SpotifyService.Deserialize<SearchTrack>(HttpUtility.HtmlDecode(trackNode.First().Value));
                        status.Track = new TrackEx(track);
                        this.cachedSearchTracks[key] = track;
                        return;
                    }
                }

                status.Tracks = this.search.Value.SearchTracks(trackName);
                var trackQuery = status.Tracks.TrackList.Where(t => t.Artist.Href == status.ArtistHref && t.Name.Equals(trackName, StringComparison.OrdinalIgnoreCase));

                if (!trackQuery.Any())
                {
                    status.Status = FindNewTracksStatus.UnknownTrack;
                }
                else
                {
                    track = trackQuery.First();
                }

                if (track != null)
                {
                    status.Track = new TrackEx(track);
                    this.cachedSearchTracks[key] = track;

                    XElement trackNode = new XElement(
                        "Track",
                        new XAttribute("Query", trackName),
                        HttpUtility.HtmlEncode(SpotifyService.Serialize(track)));

                    if (File.Exists(path))
                    {
                        doc.Root.Add(trackNode);
                    }
                    else
                    {
                        doc = new XDocument(new XElement("Tracks", trackNode));
                    }

                    doc.Save(path);
                }
            }
        }

        /// <summary>
        /// Positions the track nob based on the current play position
        /// </summary>
        /// <param name="status">Current play status</param>
        private void MoveTrackKnob(Responses.Status status)
        {
            int trackPosWidth = this.panelTrackRightEnd.Right - this.panelTrackLeftEnd.Left;
            this.panelTrackKnob.Left = this.panelTrackLeftEnd.Left + (int)(trackPosWidth * (status.PlayingPosition / status.Track.Length));
            this.panelTrackKnob.Tag = status.PlayingPosition;

            this.panelTrackLeft.Width = this.panelTrackKnob.Left - this.panelTrackLeft.Left + (this.panelTrackKnob.Width / 2);
            this.panelTrackRight.Left = this.panelTrackLeft.Right;
            this.panelTrackRight.Width = this.labelTrackLength.Left - this.panelTrackLeft.Right - 1;
        }
    }
}
