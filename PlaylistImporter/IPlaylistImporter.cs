//-----------------------------------------------------------------------
// <copyright file="IPlaylistImporter.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.PlaylistImporter
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// PlaylistImporter interface
    /// </summary>
    public interface IPlaylistImporter
    {
        /// <summary>
        /// Gets or sets the regular expression to use to parse the artist and track information
        /// </summary>
        string ArtistTrackRegex { get; set; }

        /// <summary>
        /// Gets the form to show
        /// </summary>
        Form MainForm { get; }

        /// <summary>
        /// Gets the plugin menu item text
        /// </summary>
        string MenuItem { get; }

        /// <summary>
        /// Gets or sets the list of tracks
        /// </summary>
        List<string> Tracks { get; set; }
    }
}
