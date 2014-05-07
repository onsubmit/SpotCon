namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
using System.Xml;

    /// <summary>
    /// Represents a Spotify playlist
    /// </summary>
    public class PlaylistEx
    {
        /// <summary>
        /// Gets the playlist URI
        /// </summary>
        public string Uri { get; private set; }

        /// <summary>
        /// Gets the playlist Name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the playlist tracks
        /// </summary>
        public List<string> Tracks { get; private set; }

        public PlaylistEx(string uri, string name)
        {
            this.Uri = uri;
            this.Name = name;
        }

        public PlaylistEx(XmlDocument doc)
        {
            if (doc == null)
            {
                return;
            }

            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("x", "http://spotcon.com/");

            this.Uri = doc.SelectSingleNode("x:Playlist/x:Uri", ns).InnerText;
            this.Name = doc.SelectSingleNode("x:Playlist/x:Name", ns).InnerText;

            this.Tracks = new List<string>();
            foreach (XmlNode node in doc.SelectNodes("x:Playlist/x:Tracks/x:string", ns))
            {
                this.Tracks.Add(node.InnerText);
            }
        }
    }
}
