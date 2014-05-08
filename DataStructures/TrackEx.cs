//-----------------------------------------------------------------------
// <copyright file="TrackEx.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.DataStructures
{
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Wrapper class for the different kinds of tracks
    /// </summary>
    public class TrackEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackEx"/> class
        /// </summary>
        /// <param name="track">Search Track</param>
        public TrackEx(SearchTrack track)
        {
            this.Name = track.Name;
            this.Album = new AlbumEx(track.Album);
            this.Artist = new ArtistEx(track.Artist);
            this.Popularity = track.Popularity ?? 0;
            this.Length = track.Length ?? 0;
            this.TrackNumber = track.TrackNumber ?? 0;
            this.DiscNumber = track.DiscNumber ?? 0;
            this.Href = track.Href;
            this.Original = track;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackEx"/> class
        /// </summary>
        /// <param name="track">Lookup Track</param>
        public TrackEx(Track track)
        {
            this.Name = track.Name;
            this.Album = new AlbumEx(track.Album);
            this.Artist = new ArtistEx(track.Artist);
            this.Popularity = track.Popularity ?? 0;
            this.Length = track.Length ?? 0;
            this.TrackNumber = track.TrackNumber ?? 0;
            this.DiscNumber = track.DiscNumber ?? 0;
            this.Href = track.Href;
            this.Original = track;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackEx"/> class
        /// </summary>
        /// <param name="track">Artist Track</param>
        /// <param name="album">Lookup Album</param>
        public TrackEx(ArtistTrack track, Album album)
        {
            this.Name = track.Name;
            this.Album = new AlbumEx(album);
            this.Artist = new ArtistEx(track.Artist);
            this.Popularity = track.Popularity ?? 0;
            this.Length = track.Length ?? 0;
            this.TrackNumber = track.TrackNumber ?? 0;
            this.DiscNumber = track.DiscNumber ?? 0;
            this.Href = track.Href;
            this.Original = track;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackEx"/> class
        /// </summary>
        /// <param name="track">Artist Track</param>
        /// <param name="album">Wrapped album</param>
        public TrackEx(ArtistTrack track, AlbumEx album)
        {
            this.Name = track.Name;
            this.Album = album;
            this.Artist = new ArtistEx(track.Artist);
            this.Popularity = track.Popularity ?? 0;
            this.Length = track.Length ?? 0;
            this.TrackNumber = track.TrackNumber ?? 0;
            this.DiscNumber = track.DiscNumber ?? 0;
            this.Href = track.Href;
            this.Original = track;
        }

        /// <summary>
        /// Gets the track name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the track album
        /// </summary>
        public AlbumEx Album { get; private set; }
        
        /// <summary>
        /// Gets the track artist
        /// </summary>
        public ArtistEx Artist { get; private set; }
        
        /// <summary>
        /// Gets the track popularity
        /// </summary>
        public double Popularity { get; private set; }
        
        /// <summary>
        /// Gets the track length
        /// </summary>
        public double Length { get; private set; }
        
        /// <summary>
        /// Gets the track number
        /// </summary>
        public int TrackNumber { get; private set; }

        /// <summary>
        /// Gets the disc number
        /// </summary>
        public int DiscNumber { get; private set; }

        /// <summary>
        /// Gets the track hypertext reference
        /// </summary>
        public string Href { get; private set; }
        
        /// <summary>
        /// Gets the original, unwrapped track information
        /// </summary>
        public object Original { get; private set; }

        /// <summary>
        /// Converts the track to a string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return string.Format("{0} - {1} | {2}", this.TrackNumber, this.Name, this.Album.ToString());
        }
    }
}
