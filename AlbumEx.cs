//-----------------------------------------------------------------------
// <copyright file="AlbumEx.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System.Collections.Generic;
    using System.Linq;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Wrapper class for the different kinds of albums
    /// </summary>
    public class AlbumEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="album">Artist Album</param>
        public AlbumEx(ArtistAlbum album)
        {
            this.Href = album.Href;
            this.Name = album.Name;
            this.Released = album.Released ?? 0;
            this.Tracks = new List<TrackEx>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="album">Search Album</param>
        public AlbumEx(SearchAlbum album)
        {
            this.Href = album.Href;
            this.Name = album.Name;
            this.Released = album.Released;
            this.Tracks = new List<TrackEx>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="album">Search Track Album</param>
        public AlbumEx(SearchTrackAlbum album)
        {
            this.Href = album.Href;
            this.Name = album.Name;
            this.Released = album.Released ?? 0;
            this.Tracks = new List<TrackEx>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="album">Lookup Album</param>
        public AlbumEx(Album album)
        {
            this.Href = album.Href;
            this.Name = album.Name;
            this.Released = album.Released ?? 0;
            this.Tracks = album.Tracks.Select(t => new TrackEx(t, this)).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="tracks">List of tracks</param>
        public AlbumEx(List<Track> tracks)
        {
            this.Href = tracks[0].Album.Href;
            this.Name = tracks[0].Album.Name;
            this.Released = tracks[0].Album.Released;
            this.Tracks = tracks.Select(t => new TrackEx(t)).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="tracks">List of tracks</param>
        public AlbumEx(List<SearchTrack> tracks)
        {
            this.Href = tracks[0].Album.Href;
            this.Name = tracks[0].Album.Name;
            this.Released = tracks[0].Album.Released ?? 0;
            this.Tracks = tracks.Select(t => new TrackEx(t)).ToList();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="tracks">List of tracks</param>
        public AlbumEx(List<TrackEx> tracks)
        {
            this.Href = tracks[0].Album.Href;
            this.Name = tracks[0].Album.Name;
            this.Released = tracks[0].Album.Released;
            this.Tracks = tracks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumEx"/> class
        /// </summary>
        /// <param name="album">Lookup response album</param>
        public AlbumEx(SpotifyWebSharp.SpotifyResponses.Lookup.Response.BaseAlbum album)
        {
            this.Href = album.Href;
            this.Name = album.Name;
            this.Released = album.Released;
            this.Tracks = new List<TrackEx>();
        }

        /// <summary>
        /// Gets the album hypertext reference
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// Gets the album name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the album release year
        /// </summary>
        public int Released { get; private set; }

        /// <summary>
        /// Gets the album's list of tracks
        /// </summary>
        public List<TrackEx> Tracks { get; private set; }

        /// <summary>
        /// Converts the album to a string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return string.Format("({0}) {1}", this.Released, this.Name);
        }
    }
}
