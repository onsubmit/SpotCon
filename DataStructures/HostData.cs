//-----------------------------------------------------------------------
// <copyright file="HostData.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.DataStructures
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Sockets;
    using System.Windows.Forms;
    using SpotCon.Enums;

    /// <summary>
    /// Represents host information
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "'Spotify' is spelled correctly, dummy.")]
    public class HostData
    {
        /// <summary>
        /// Gets or sets the hosts' IP address
        /// </summary>
        public IPAddress IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the host's associated socket
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Gets or sets the hosts' connection status
        /// </summary>
        public ClientConnectStatus ConnectionStatus { get; set; }

        /// <summary>
        /// Gets or sets the associated row in the host data grid
        /// </summary>
        public DataGridViewRow Row { get; set; }

        /// <summary>
        /// Gets or sets the timer used to update the Spotify status
        /// </summary>
        public Timer Timer { get; set; }
    }
}
