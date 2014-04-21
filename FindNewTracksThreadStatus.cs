//-----------------------------------------------------------------------
// <copyright file="FindNewTracksThreadStatus.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System.Diagnostics.CodeAnalysis;
using SpotifyWebSharp.SpotifyResponses.Lookup;
using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Status of FindNewTracks background thread
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public class FindNewTracksThreadStatus
    {
        /// <summary>
        /// Gets or sets the current index, used for status messaging
        /// </summary>
        public int Counter { get; set; }

        /// <summary>
        /// Gets or sets the maximum index, used for status messaging
        /// </summary>
        public int Maximum { get; set; }

        /// <summary>
        /// Gets or sets the message to display
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// Gets or sets the artist name
        /// </summary>
        public string ArtistName { get; set; }

        /// <summary>
        /// Gets or sets the artist href
        /// </summary>
        public string ArtistHref { get; set; }

        /// <summary>
        /// Gets or sets the track name
        /// </summary>
        public string TrackName { get; set; }

        /// <summary>
        /// Gets or sets the Artists Spotify response
        /// </summary>
        public Artists Artists { get; set; }

        /// <summary>
        /// Gets or sets the Tracks Spotify response
        /// </summary>
        public Tracks Tracks { get; set; }

        /// <summary>
        /// Gets or sets the Artist
        /// </summary>
        public SearchArtist Artist { get; set; }

        /// <summary>
        /// Gets or sets the Track
        /// </summary>
        public TrackEx Track { get; set; }

        /// <summary>
        /// Gets or sets the thread status
        /// </summary>
        public FindNewTracksStatus Status { get; set; }
    }
}
