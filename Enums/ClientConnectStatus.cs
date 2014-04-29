//-----------------------------------------------------------------------
// <copyright file="ClientConnectStatus.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.Enums
{
    /// <summary>
    /// Represents the connect status of the client
    /// </summary>
    public enum ClientConnectStatus
    {
        /// <summary>
        /// Client is disconnected
        /// </summary>
        Disconnected,

        /// <summary>
        /// Client is connected
        /// </summary>
        Connected,

        /// <summary>
        /// Client couldn't connect
        /// </summary>
        ConnectionFailure
    }
}
