//-----------------------------------------------------------------------
// <copyright file="FindNewTracksStatus.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.Enums
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Status of FindNewTracks background thread
    /// </summary>
    public enum FindNewTracksStatus
    {
        /// <summary>
        /// No specific event occurred
        /// </summary>
        None,

        /// <summary>
        /// A new track was added
        /// </summary>
        AddNewTrack,

        /// <summary>
        /// The track was skipped
        /// </summary>
        TrackSkipped,

        /// <summary>
        /// The artist and track could not be parsed
        /// </summary>
        ParseError,

        /// <summary>
        /// An invalid regex was entered
        /// </summary>
        InvalidRegex,

        /// <summary>
        /// Unknown artist encountered
        /// </summary>
        UnknownArtist,

        /// <summary>
        /// Unknown track encountered
        /// </summary>
        UnknownTrack,

        /// <summary>
        /// User wants to cancel this process of finding missing information
        /// </summary>
        Cancel,
    }
}
