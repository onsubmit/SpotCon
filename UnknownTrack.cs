//-----------------------------------------------------------------------
// <copyright file="UnknownTrack.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using SpotCon.DataStructures;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;
    using SpotifyWebSharp.SpotifyServices;

    /// <summary>
    /// Unknown track form
    /// </summary>
    public partial class UnknownTrack : Form
    {
        /// <summary>
        /// Artist name
        /// </summary>
        private string artistName;

        /// <summary>
        /// Artist href
        /// </summary>
        private string artistHref;

        /// <summary>
        /// Track name
        /// </summary>
        private string trackName;

        /// <summary>
        /// List of tracks from Spotify search
        /// </summary>
        private List<SearchTrack> tracks;

        /// <summary>
        /// List of suggestions to display to the user
        /// </summary>
        private List<SearchTrack> suggestions = new List<SearchTrack>();

        /// <summary>
        /// Spotify Search service
        /// </summary>
        private SearchService search = new SearchService();

        /// <summary>
        /// Spotify Lookup service
        /// </summary>
        private LookupService lookup = new LookupService();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownTrack" /> class
        /// </summary>
        /// <param name="artistName">Artist name</param>
        /// <param name="artistHref">Artist href</param>
        /// <param name="trackName">Track name</param>
        /// <param name="tracks">List of tracks from Spotify search</param>
        public UnknownTrack(string artistName, string artistHref, string trackName, List<SearchTrack> tracks)
        {
            this.InitializeComponent();
            this.artistName = artistName;
            this.artistHref = artistHref;
            this.trackName = trackName;
            this.tracks = tracks;
        }

        /// <summary>
        /// Gets the track the user selected
        /// </summary>
        public TrackEx SelectedTrack { get; private set; }

        /// <summary>
        /// ButtonLookup Click event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ButtonLookup_Click(object sender, EventArgs e)
        {
            string value = this.textBoxManual.Text;
            Regex regex = new Regex(@"((http://open.spotify.com/track/)|(spotify:track:))(?<HREF>(.+))");
            Match match = regex.Match(value);
            if (match.Success)
            {
                value = match.Groups["HREF"].Value;
                Track track = this.lookup.LookupTrack(value);
                track.Href = "spotify:track:" + value;
                this.SelectedTrack = new TrackEx(track);
                this.ButtonOk_Click(null, null);
            }
            else
            {
                Tracks searchTracks = this.search.SearchTracks(value);
                this.tracks = searchTracks.TrackList;
                var artistQuery = searchTracks.TrackList.Where(t => t.Artist.Href == this.artistHref);
                if (artistQuery.Any())
                {
                    this.tracks = artistQuery.ToList();
                }

                this.PopulateListBoxFromTracks();
            }
        }

        /// <summary>
        /// ButtonOk Click event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (this.SelectedTrack == null)
            {
                this.SelectedTrack = new TrackEx(this.suggestions[this.listBoxSuggestions.SelectedIndex]);
            }

            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// ListBoxSuggestions SelectedIndexChanged event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ListBoxSuggestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBoxSuggestions.SelectedIndex;
            if (index >= this.suggestions.Count)
            {
                buttonOk.Enabled = false;
            }
            else
            {
                buttonOk.Enabled = true;
            }
        }

        /// <summary>
        /// Populates the suggestion list box from the Tracks response
        /// </summary>
        private void PopulateListBoxFromTracks()
        {
            int take = (int)this.numericUpDownNumSuggestions.Value;
            this.listBoxSuggestions.Items.Clear();
            this.suggestions.Clear();

            this.tracks.Take(take).ToList().ForEach(t =>
            {
                this.listBoxSuggestions.Items.Add(string.Format("{0} - {1} [{2}]", t.Artist.Name, t.Name, t.Album.Name));
                this.suggestions.Add(t);
            });

            if (this.tracks.Count == 1)
            {
                this.listBoxSuggestions.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// UnknownTrack Load event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void UnknownTrack_Load(object sender, EventArgs e)
        {
            this.PopulateListBoxFromTracks();
            this.labelDescriptionTrack.Text = string.Format("No tracks named <{0}> ", this.trackName);
            this.labelDescriptionArtist.Text = string.Format("with artist named <{0}>", this.artistName);
        }

        /// <summary>
        /// textBoxManual TextChanged event
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBoxManual_TextChanged(object sender, EventArgs e)
        {
            this.buttonLookup.Enabled = !string.IsNullOrEmpty(this.textBoxManual.Text);
        }
    }
}
