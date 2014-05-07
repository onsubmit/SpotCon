//-----------------------------------------------------------------------
// <copyright file="SpotConService.asmx.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace WebService
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using System.Web.Services;

    /// <summary>
    /// SpotCon web service
    /// </summary>
    [WebService(Namespace = "http://spotcon.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SpotConService : System.Web.Services.WebService
    {
        /// <summary>
        /// Persistent playlist data
        /// </summary>
        private static ConcurrentDictionary<string, Playlist> playlists = new ConcurrentDictionary<string, Playlist>();

        /// <summary>
        /// Stores the given playlist in memory
        /// </summary>
        /// <param name="uri">Playlist URI</param>
        /// <param name="name">Playlist Name</param>
        /// <param name="tracks">Playlist tracks</param>
        [WebMethod]
        public void SetPlaylist(string uri, string name, string tracks)
        {
            Playlist playlist = new Playlist() { Uri = uri, Name = name, Tracks = tracks.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(), WhenAdded = DateTime.Now };
            playlists.AddOrUpdate(uri, playlist, (key, oldValue) => playlist);
        }

        /// <summary>
        /// Gets the most recently added playlist
        /// </summary>
        /// <returns>See description</returns>
        [WebMethod]
        public Playlist GetLatestPlaylist()
        {
            var query = from p in playlists.Values
                        where p.RetrievedAsLatest == false
                        orderby p.WhenAdded descending
                        select p;

            if (query.Any())
            {
                Playlist playlist = query.First();
                playlist.RetrievedAsLatest = true;
                return playlist;
            }

            return null;
        }

        /// <summary>
        /// Gets the playlist of the given URI
        /// </summary>
        /// <param name="uri">Playlist URI</param>
        /// <returns>See description</returns>
        [WebMethod]
        public Playlist GetPlaylist(string uri)
        {
            Playlist playlist = null;
            if (playlists.ContainsKey(uri))
            {
                playlists.TryGetValue(uri, out playlist);
            }

            return playlist;
        }
    }
}