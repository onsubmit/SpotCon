//-----------------------------------------------------------------------
// <copyright file="UnknownArtist.cs" company="Andy Young">
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
    using SpotifyWebSharp.SpotifyResponses.Search;
    using SpotifyWebSharp.SpotifyServices;

    /// <summary>
    /// Unknown artist form
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed.")]
    public partial class UnknownArtist : Form
    {
        /// <summary>
        /// Artist name
        /// </summary>
        private string artistName;

        /// <summary>
        /// Artists response from Spotify search
        /// </summary>
        private Artists artists;

        /// <summary>
        /// List of suggestions to display to the user
        /// </summary>
        private List<SearchArtist> suggestions = new List<SearchArtist>();

        /// <summary>
        /// Spotify Search service
        /// </summary>
        private SearchService search = new SearchService();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownArtist" /> class
        /// </summary>
        /// <param name="artistName">Artist name</param>
        /// <param name="artists">Artists response from Spotify search</param>
        public UnknownArtist(string artistName, Artists artists)
        {
            this.InitializeComponent();
            this.artistName = artistName;
            this.artists = artists;
        }

        /// <summary>
        /// Gets the artist the user selected
        /// </summary>
        public SearchArtist SelectedArtist { get; private set; }

        /// <summary>
        /// ButtonLookup Click event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ButtonLookup_Click(object sender, EventArgs e)
        {
            string value = this.textBoxManual.Text;
            Regex regex = new Regex(@"((http://open.spotify.com/artist/)|(spotify:artist:))(?<HREF>(.+))");
            Match match = regex.Match(value);
            if (match.Success)
            {
                value = match.Groups["HREF"].Value;
                this.SelectedArtist = new SearchArtist() { Href = value };
                this.ButtonOk_Click(null, null);
            }
            else
            {
                this.artists = this.search.SearchArtists(value);
                this.PopulateListBoxFromArtists();
            }
        }

        /// <summary>
        /// ButtonOk Click event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (this.SelectedArtist == null)
            {
                this.SelectedArtist = this.suggestions[this.listBoxSuggestions.SelectedIndex];
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
        /// Populates the suggestion list box from the Artists response
        /// </summary>
        private void PopulateListBoxFromArtists()
        {
            int take = (int)this.numericUpDownNumSuggestions.Value;
            this.listBoxSuggestions.Items.Clear();
            this.suggestions.Clear();

            this.artists.ArtistList.Take(take).ToList().ForEach(a =>
            {
                this.listBoxSuggestions.Items.Add(a.Name);
                this.suggestions.Add(a);
            });

            if (this.artists.ArtistList.Count == 1)
            {
                this.listBoxSuggestions.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// UnknownArtist Load event handler
        /// </summary>
        /// <param name="sender">Object that raised the event</param>
        /// <param name="e">Event arguments</param>
        private void UnknownArtist_Load(object sender, EventArgs e)
        {
            this.PopulateListBoxFromArtists();
            this.labelDescription.Text = string.Format("No results for artists named <{0}>", this.artistName);
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
