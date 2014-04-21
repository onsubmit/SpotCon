//-----------------------------------------------------------------------
// <copyright file="ArtistEx.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    /// <summary>
    /// Wrapper class for the different kinds of artists
    /// </summary>
    public class ArtistEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistEx"/> class
        /// </summary>
        /// <param name="artist">Lookup Artist</param>
        public ArtistEx(SpotifyWebSharp.SpotifyResponses.Lookup.Response.BaseArtist artist)
        {
            this.Href = artist.Href;
            this.Name = artist.Name;
        }

        /// <summary>
        /// Gets the artist hypertext reference
        /// </summary>
        public string Href { get; private set; }

        /// <summary>
        /// Gets the artist name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Converts the artist to a string
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
