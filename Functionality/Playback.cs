//-----------------------------------------------------------------------
// <copyright file="Playback.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net.Sockets;
    using System.Windows.Forms;
    using SpotifyWebSharp.SpotifyResponses.Lookup;
    using SpotifyWebSharp.SpotifyResponses.Search;

    /// <summary>
    /// Playback methods
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// panelPlayPause MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPlayPause_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelPlayPause.Capture = true;
        }

        /// <summary>
        /// panelPlayPause MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPlayPause_MouseUp(object sender, MouseEventArgs e)
        {
            this.panelPlayPause.Capture = false;
            if (this.currentStatus.Playing)
            {
                this.panelPlayPause.BackgroundImage = Properties.Resources.Pause;
            }
            else
            {
                this.panelPlayPause.BackgroundImage = Properties.Resources.Play;
            }
        }

        /// <summary>
        /// panelPrevious MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPrevious_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelPrevious.Capture = true;
        }

        /// <summary>
        /// panelPrevious MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPrevious_MouseUp(object sender, MouseEventArgs e)
        {
            this.panelPrevious.Capture = false;
        }

        /// <summary>
        /// panelNext MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelNext_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelNext.Capture = true;
        }

        /// <summary>
        /// panelNext MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelNext_MouseUp(object sender, MouseEventArgs e)
        {
            this.panelNext.Capture = false;
        }

        /// <summary>
        /// panelPrevious Click
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                this.SendToServer("|Previous");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// panelPlayPause Click
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelPlayPause_Click(object sender, EventArgs e)
        {
            try
            {
                this.SendToServer("|Pause");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }

        /// <summary>
        /// panelNext Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelNext_Click(object sender, EventArgs e)
        {
            try
            {
                this.SendToServer("|Next");
            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }
        }
    }
}
