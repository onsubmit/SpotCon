//-----------------------------------------------------------------------
// <copyright file="Caching.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows.Forms;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Caching methods
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Maps artist name to a Spotify artist href
        /// </summary>
        /// <example>The Naked and Famous|||spotify:artist:0oeUpvxWsC8bWS6SnpU8b9</example>
        private Dictionary<string, string> cachedArtistHrefs = new Dictionary<string, string>();

        /// <summary>
        /// Maps artist name input to actual Spotify artist name
        /// </summary>
        /// <example>Smashing Pumpkins|||The Smashing Pumpkins</example>
        private Dictionary<string, string> misspelledArtists = new Dictionary<string, string>();

        /// <summary>
        /// Maps artist name and track name input to actual Spotify track name
        /// </summary>
        /// <example>spotify:artist:4tZwfgrHOc3mvqYlEYSvVi:::Get Lucky (feat. Pharrell Williams &amp; Nile Rodgers)|||Get Lucky</example>
        private Dictionary<string, string> misspelledTracks = new Dictionary<string, string>();

        /// <summary>
        /// Maps track ID to Lookup Track
        /// </summary>
        private Dictionary<string, Track> cachedLookupTracks = new Dictionary<string, Track>();

        /// <summary>
        /// Maps artist href to Lookup Album
        /// </summary>
        private Dictionary<string, Album> cachedLookupAlbums = new Dictionary<string, Album>();

        /// <summary>
        /// Maps artist (href) and track name combination to Spotify Search Track
        /// </summary>
        /// <example>spotify:artist:3jOstUTkEu2JkjvRdBA5Gu:::Buddy Holly -> SearchTrack serialized on disk</example>
        private Dictionary<string, SearchTrack> cachedSearchTracks = new Dictionary<string, SearchTrack>();

        /// <summary>
        /// Saves the artist cache
        /// </summary>
        private void SaveArtistCache()
        {
            StringCollection sc = new StringCollection();
            sc.AddRange(this.cachedArtistHrefs.Select(kvp => string.Format("{0}|||{1}", kvp.Key, kvp.Value)).ToArray());
            Properties.Settings.Default.CachedArtistHrefs = sc;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Saves the misspelled artist cache
        /// </summary>
        private void SaveMisspelledArtistCache()
        {
            StringCollection sc = new StringCollection();
            sc.AddRange(this.misspelledArtists.Select(kvp => string.Format("{0}|||{1}", kvp.Key, kvp.Value)).ToArray());
            Properties.Settings.Default.CachedMisspelledArtists = sc;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Saves the misspelled track cache
        /// </summary>
        private void SaveMisspelledTrackCache()
        {
            StringCollection sc = new StringCollection();
            sc.AddRange(this.misspelledTracks.Select(kvp => string.Format("{0}|||{1}", kvp.Key, kvp.Value)).ToArray());
            Properties.Settings.Default.CachedMisspelledTracks = sc;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Reads the artist href cache
        /// </summary>
        private void ReadArtistCache()
        {
            foreach (string cachedArtistHrefLine in Properties.Settings.Default.CachedArtistHrefs)
            {
                string[] split = cachedArtistHrefLine.Split(new string[] { "|||" }, StringSplitOptions.None);
                this.cachedArtistHrefs[split[0]] = split[1];
            }
        }

        /// <summary>
        /// Reads the cached list of misspelled artists
        /// </summary>
        private void ReadMisspelledArtistCache()
        {
            foreach (string artist in Properties.Settings.Default.CachedMisspelledArtists)
            {
                string[] split = artist.Split(new string[] { "|||" }, StringSplitOptions.None);
                this.misspelledArtists[split[0]] = split[1];
            }
        }

        /// <summary>
        /// Reads the cached list of misspelled tracks
        /// </summary>
        private void ReadMisspelledTrackCache()
        {
            foreach (string track in Properties.Settings.Default.CachedMisspelledTracks)
            {
                string[] split = track.Split(new string[] { "|||" }, StringSplitOptions.None);
                this.misspelledTracks[split[0]] = split[1];
            }
        }

        /// <summary>
        /// Reads the track cache
        /// </summary>
        private void ReadCachedTracks()
        {
            foreach (string cachedTrackUrlLine in Properties.Settings.Default.CachedSearchTracks)
            {
                this.cachedSearchTracks.Add(cachedTrackUrlLine, null);
            }
        }

        /// <summary>
        /// Saves the track cache
        /// </summary>
        private void SaveSearchTrackCache()
        {
            StringCollection sc = new StringCollection();
            sc.AddRange(this.cachedSearchTracks.Select(kvp => kvp.Key).ToArray());
            Properties.Settings.Default.CachedSearchTracks = sc;
            Properties.Settings.Default.Save();
        }
    }
}
