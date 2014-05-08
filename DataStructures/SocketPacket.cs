//-----------------------------------------------------------------------
// <copyright file="SocketPacket.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.DataStructures
{
    using System.Net.Sockets;

    /// <summary>
    /// Represents a packet
    /// </summary>
    public class SocketPacket
    {
        /// <summary>
        /// Initializes a new instance of the SocketPacket class
        /// </summary>
        public SocketPacket()
        {
            this.Buffer = new byte[1];
        }

        /// <summary>
        /// Gets or sets the corresponding socket
        /// </summary>
        public Socket Socket { get; set; }

        /// <summary>
        /// Gets or sets the buffer to store packet data
        /// </summary>
        public byte[] Buffer { get; set; }
    }
}
