﻿//-----------------------------------------------------------------------
// <copyright file="PlaylistEx.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;

    /// <summary>
    /// Represents a Spotify playlist
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "'Spotify' is spelled correctly, dummy.")]
    public class PlaylistEx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistEx"/> class
        /// </summary>
        /// <param name="uri">Playlist URI</param>
        /// <param name="name">Playlist name</param>
        /// <param name="tracks">Playlist tracks</param>
        public PlaylistEx(string uri, string name, List<string> tracks)
        {
            this.Uri = uri;
            this.Name = name;
            this.Tracks = tracks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistEx"/> class
        /// </summary>
        /// <param name="doc">SOAP response from SpotCon webservice</param>
        public PlaylistEx(XmlDocument doc)
        {
            if (doc == null)
            {
                return;
            }

            XmlNamespaceManager ns = new XmlNamespaceManager(doc.NameTable);
            ns.AddNamespace("x", "http://spotcon.com/");

            XmlAttribute nil = doc.SelectSingleNode("x:Playlist", ns).Attributes["xsi:nil"];
            bool isNull = nil != null && nil.InnerText.Equals("true", StringComparison.OrdinalIgnoreCase);

            if (isNull)
            {
                return;
            }

            this.Uri = doc.SelectSingleNode("x:Playlist/x:Uri", ns).InnerText;
            this.Name = doc.SelectSingleNode("x:Playlist/x:Name", ns).InnerText;

            this.Tracks = new List<string>();
            foreach (XmlNode node in doc.SelectNodes("x:Playlist/x:Tracks/x:string", ns))
            {
                this.Tracks.Add(node.InnerText);
            }
        }

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

        /// <summary>
        /// Gets a value indicating whether the playlist object is emtpy
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(this.Uri) &&
                       string.IsNullOrEmpty(this.Name) &&
                       this.Tracks == null;
            }
        }
    }
}
