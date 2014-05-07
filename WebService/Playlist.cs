//-----------------------------------------------------------------------
// <copyright file="Playlist.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WebService
{
    using System;
using System.Collections.Generic;

    /// <summary>
    /// Represents a Spotify playlist
    /// </summary>
    public class Playlist
    {
        /// <summary>
        /// Gets or sets the playlist URI
        /// </summary>
        public string Uri { get; set; }
        
        /// <summary>
        /// Gets or sets the playlist Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the playlist tracks
        /// </summary>
        public List<string> Tracks { get; set; }

        /// <summary>
        /// Gets or sets when the playlist was added to memory
        /// </summary>
        public DateTime WhenAdded { get; set; }
    }
}