//-----------------------------------------------------------------------
// <copyright file="SpotConWebService.asmx.cs" company="Andy Young">
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
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://spotcon.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class SpotConWebService : System.Web.Services.WebService
    {
        private static ConcurrentDictionary<string, Playlist> playlists = new ConcurrentDictionary<string, Playlist>();

        [WebMethod]
        public void SetPlaylist(string uri, string name, string tracks)
        {
            Playlist playlist = new Playlist() { Uri = uri, Name = name, Tracks = tracks.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(), WhenAdded = DateTime.Now };
            playlists.AddOrUpdate(uri, playlist, (key, oldValue) => playlist);
        }

        [WebMethod]
        public Playlist GetLatestPlaylist()
        {
            var query = from p in playlists.Values
                        orderby p.WhenAdded
                        select p;

            if (query.Any())
            {
                return query.First();
            }

            return null;
        }

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