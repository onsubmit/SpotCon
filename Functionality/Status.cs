//-----------------------------------------------------------------------
// <copyright file="Status.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using SpotCon.Enums;
    using SpotifyWebHelperSharp;

    /// <summary>
    /// Status functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// The current status of Spotify
        /// </summary>
        private Responses.Status currentStatus;

        /// <summary>
        /// Updates the status
        /// </summary>
        /// <param name="status">Status to update</param>
        /// <returns>True if success</returns>
        private bool UpdateStatus(Responses.Status status)
        {
            if (status == null || status.OpenGraphState == null || status.OpenGraphState.PrivateSession)
            {
                this.SetStatusError(Properties.Resources.PrivateSessionError);
                return false;
            }

            if (status.Error != null)
            {
                this.SetStatusError(string.Format(Properties.Resources.StatusError, status.Error.Type));
                return false;
            }

            if (status.Track == null)
            {
                this.SetStatusError(string.Format(Properties.Resources.StatusError, string.Empty));
                return false;
            }

            if (this.linkLabelArtist.Text != status.Track.ArtistResource.Name)
            {
                this.linkLabelArtist.Text = status.Track.ArtistResource.Name;
                this.linkLabelArtist.Tag = status.Track.ArtistResource.Uri;
                this.linkLabelArtist.Enabled = true;
            }

            if (this.linkLabelTrack.Text != status.Track.TrackResource.Name)
            {
                this.linkLabelTrack.Text = status.Track.TrackResource.Name;
                this.linkLabelTrack.Tag = status.Track.TrackResource.Uri;
                this.linkLabelTrack.Enabled = true;
            }

            if (!panelPlayPause.Capture)
            {
                if (status.Playing)
                {
                    this.panelPlayPause.BackgroundImage = Properties.Resources.Pause;
                }
                else
                {
                    this.panelPlayPause.BackgroundImage = Properties.Resources.Play;
                }

                if (status.IsPlayEnabled)
                {
                    this.panelPlayPause.Enabled = true;
                }
                else
                {
                    this.panelPlayPause.Enabled = false;
                    this.panelPlayPause.BackgroundImage = Properties.Resources.PlayDisabled;
                }
            }

            if (!this.panelPrevious.Capture)
            {
                if (status.IsPrevEnabled)
                {
                    this.panelPrevious.Enabled = true;
                    this.panelPrevious.BackgroundImage = Properties.Resources.Previous;
                }
                else
                {
                    this.panelPrevious.Enabled = false;
                    this.panelPrevious.BackgroundImage = Properties.Resources.PreviousDisabled;
                }
            }

            if (!this.panelNext.Capture)
            {
                this.panelNext.Enabled = true;
            }

            if (!this.panelVolumeKnob.Capture)
            {
                this.panelVolumeKnob.Left = this.panelVolumeLeftEnd.Left + (int)((this.panelVolumeRightEnd.Right - this.panelVolumeLeftEnd.Left - (this.panelVolumeKnob.Width / 2)) * status.Volume / 1.0);
                this.ResizeVolumePanels();
            }

            if (status.Playing || this.panelTrackKnob.Tag == null)
            {
                string position = TimeSpan.FromSeconds(Math.Ceiling(status.PlayingPosition)).ToString(@"m\:ss");
                string end = TimeSpan.FromSeconds((double)status.Track.Length).ToString(@"m\:ss");
                this.labelTrackTime.Text = position;
                this.labelTrackLength.Text = end;

                this.MoveTrackKnob(status);
            }

            this.panelShuffle.BackgroundImage = status.Shuffle ? Properties.Resources.ShuffleGreen : Properties.Resources.Shuffle;
            this.panelRepeat.BackgroundImage = status.Repeat ? Properties.Resources.RepeatGreen : Properties.Resources.Repeat;

            return true;
        }

        /// <summary>
        /// linkLabelTrack LinkClicked event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void linkLabelTrack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.LookupAlbum(this.currentStatus.Track.AlbumResource.Uri, this.currentStatus.Track.TrackResource.Uri, this.currentStatus.Track.TrackResource.Name);
        }

        /// <summary>
        /// linkLabelArtist LinkClicked event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void linkLabelArtist_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.queuedIndices.Clear();
            this.searchQueue.Enqueue(new Tuple<string, int?>(linkLabelArtist.Text, null));
            this.Search(SearchType.Artist);
        }

        /// <summary>
        /// Clears the current status
        /// </summary>
        private void ClearStatus()
        {
            this.SetStatus(string.Empty);
        }

        /// <summary>
        /// Sets the status to busy
        /// </summary>
        /// <param name="message">Message to set</param>
        private void SetStatusBusy(string message)
        {
            this.SetStatus(message);
            this.SetBusyState(true);
        }

        /// <summary>
        /// Sets the status after an error
        /// </summary>
        /// <param name="message">Message to set</param>
        private void SetStatusError(string message)
        {
            this.SetStatus(message, Color.Red);
            this.SetBusyState(false);
        }

        /// <summary>
        /// Sets the status after a successful event
        /// </summary>
        /// <param name="message">Message to set</param>
        private void SetStatusSuccess(string message)
        {
            this.SetStatus(message);
            this.SetBusyState(false);
        }

        /// <summary>
        /// Sets the status
        /// </summary>
        /// <param name="message">Message to set</param>
        private void SetStatus(string message)
        {
            this.SetStatus(message, Color.FromArgb(148, 149, 153));
        }

        /// <summary>
        /// Sets the status
        /// </summary>
        /// <param name="message">Message to set</param>
        /// <param name="color">Message color to use</param>
        private void SetStatus(string message, Color color)
        {
            labelStatus.Text = message;
            labelStatus.ForeColor = color;
        }

        /// <summary>
        /// Sets busy state
        /// </summary>
        /// <param name="busy">True to use wait cursor</param>
        private void SetBusyState(bool busy)
        {
            Cursor.Current = busy ? Cursors.WaitCursor : Cursors.Default;
            this.Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            dataGridViewTracks.Cursor = busy ? Cursors.WaitCursor : Cursors.Default; // hack?
            this.UseWaitCursor = busy;
            Application.UseWaitCursor = busy;
        }

        /// <summary>
        /// Sets the value of the progress bar
        /// </summary>
        /// <param name="valueToSet">Value to set</param>
        /// <param name="maxValueToSet">Maximum value to set</param>
        private void SetProgressBar(int valueToSet, int maxValueToSet = -1)
        {
            if (maxValueToSet >= 0)
            {
                this.progressBar.Maximum = maxValueToSet;
                this.progressBar.Visible = true;
            }

            this.progressBar.Value = valueToSet;
        }

        /// <summary>
        /// Hides the progress bar
        /// </summary>
        private void HideProgressBar()
        {
            this.progressBar.Visible = false;
        }
    }
}
