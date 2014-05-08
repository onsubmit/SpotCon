//-----------------------------------------------------------------------
// <copyright file="Volume.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Volume functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// panelVolumeKnob MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelVolumeKnob_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelVolumeKnob.Tag = e.X;
            this.panelVolumeKnob.Capture = true;
            this.panelVolumeKnob.BackgroundImage = Properties.Resources.VolumeKnobClicked;
        }

        /// <summary>
        /// panelVolumeKnob MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelVolumeKnob_MouseUp(object sender, MouseEventArgs e)
        {
            this.panelVolumeKnob.Capture = false;
            this.panelVolumeKnob.BackgroundImage = Properties.Resources.VolumeKnob;
            this.ChangeVolume();
        }

        /// <summary>
        /// panelVolumeKnob MouseMove event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelVolumeKnob_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.panelVolumeKnob.Capture)
            {
                this.MoveVolumeKnob(e.X);
            }
        }

        /// <summary>
        /// Moves the volume knob to the given x-coordinate
        /// </summary>
        /// <param name="x">Coordinate to which to move the volume knob</param>
        private void MoveVolumeKnob(int x)
        {
            int halfKnob = this.panelVolumeKnob.Width / 2;
            panelVolumeKnob.Left = Math.Min(Math.Max(this.panelVolumeLeftEnd.Left - halfKnob, x + this.panelVolumeKnob.Left - halfKnob), this.panelVolumeRightEnd.Right - halfKnob);
            this.ResizeVolumePanels();
        }

        /// <summary>
        /// Resizes the volume panels based on the positions of the volume knob
        /// </summary>
        private void ResizeVolumePanels()
        {
            this.panelVolumeLeft.Width = panelVolumeKnob.Left - this.panelVolumeLeftEnd.Right;
            this.panelVolumeRight.Left = panelVolumeKnob.Right;
            this.panelVolumeRight.Width = this.panelVolumeRightEnd.Left - this.panelVolumeRight.Left;
        }

        /// <summary>
        /// panelVolume MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelVolume_MouseDown(object sender, MouseEventArgs e)
        {
            this.panelVolumeKnob.Tag = e.X;
            this.panelVolumeKnob.Capture = true;
            this.MoveVolumeKnob(e.X);
        }

        /// <summary>
        /// panelVolume MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelVolume_MouseUp(object sender, MouseEventArgs e)
        {
            this.panelVolumeKnob.Capture = false;
            this.ChangeVolume();
        }

        /// <summary>
        /// Changes the volume
        /// </summary>
        private void ChangeVolume()
        {
            double level = (double)((this.panelVolumeKnob.Left + (this.panelVolumeKnob.Width / 2)) - this.panelVolumeLeftEnd.Left) / (double)(this.panelVolumeRightEnd.Right - this.panelVolumeLeftEnd.Left);
            this.SendToServer(level + ":" + this.currentStatus.Volume + "|SetVolume");
        }
    }
}
