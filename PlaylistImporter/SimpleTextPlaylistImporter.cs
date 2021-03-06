﻿//-----------------------------------------------------------------------
// <copyright file="SimpleTextPlaylistImporter.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.PlaylistImporter
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows.Forms;

    /// <summary>
    /// Example playlist import plugin
    /// </summary>
    public partial class SimpleTextPlaylistImporter : Form, IPlaylistImporter
    {
        /// <summary>
        /// Initializes a new instance of the SimpleTextPlaylistImporter class
        /// </summary>
        public SimpleTextPlaylistImporter()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the regular expression to use to parse the artist and track information
        /// </summary>
        public string ArtistTrackRegex { get; set; }

        /// <summary>
        /// Gets the form to show
        /// </summary>
        public Form MainForm
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets the plugin menu item text
        /// </summary>
        public string MenuItem
        {
            get
            {
                return "Plain text";
            }
        }

        /// <summary>
        /// Gets or sets the list of tracks
        /// </summary>
        public List<string> Tracks { get; set; }

        /// <summary>
        /// buttonImport Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void buttonImport_Click(object sender, EventArgs e)
        {
            this.Tracks = new List<string>(this.richTextBoxInput.Text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
            this.ArtistTrackRegex = this.textBoxFormat.Text;
            this.Close();
        }
    }
}
