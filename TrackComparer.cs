// -----------------------------------------------------------------------
// <copyright file="TrackComparer.cs" company="Andy Young">
// Copyright (c) Andy Young. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace SpotCon
{
    using System.Linq;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Determines how similar two tracks are 
    /// </summary>
    public static class TrackComparer
    {
        /// <summary>
        /// Determines how similar two tracks are
        /// </summary>
        /// <param name="t1">Track #1</param>
        /// <param name="t2">Track #2</param>
        /// <returns>A value between 0-100 describing how similar the two tracks are</returns>
        public static double Compare(object t1, object t2)
        {
            string id1, artistName1, trackName1, albumName1;
            string id2, artistName2, trackName2, albumName2;

            if (t1 is Track)
            {
                Track track = t1 as Track;
                id1 = track.Id.Value;
                artistName1 = track.Album.Name;
                trackName1 = track.Name;
                albumName1 = track.Album.Name;
            }
            else
            {
                SearchTrack track = t1 as SearchTrack;
                id1 = track.Id.Value;
                artistName1 = track.Album.Name;
                trackName1 = track.Name;
                albumName1 = track.Album.Name;
            }

            if (t2 is Track)
            {
                Track track = t2 as Track;
                id2 = track.Id.Value;
                artistName2 = track.Album.Name;
                trackName2 = track.Name;
                albumName2 = track.Album.Name;
            }
            else
            {
                SearchTrack track = t2 as SearchTrack;
                id2 = track.Id.Value;
                artistName2 = track.Album.Name;
                trackName2 = track.Name;
                albumName2 = track.Album.Name;
            }

            if (id1 == id2)
            {
                return 100;
            }

            double artistSimilarity = CompareSentences(artistName1, artistName1);
            double trackSimilarity = CompareSentences(trackName1, trackName2);
            double albumSimiliarity = CompareSentences(albumName1, albumName2);
            return (artistSimilarity + trackSimilarity + albumSimiliarity) / 3.0;
        }

        /// <summary>
        /// Compares two sentences for similarity
        /// </summary>
        /// <param name="sentence1">Sentence #1</param>
        /// <param name="sentence2">Sentence #2</param>
        /// <returns>A value between 0-100 describing how similar the two sentences are</returns>
        private static double CompareSentences(string sentence1, string sentence2)
        {
            string[] words1 = sentence1.Split(' ');
            string[] words2 = sentence2.Split(' ');
            var common = words1.Intersect(words2);
            return (double)(100 * (common.Count() * 2)) / (words1.Length + words2.Length);
        }
    }
}
