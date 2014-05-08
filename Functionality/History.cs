//-----------------------------------------------------------------------
// <copyright file="History.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// History methods
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// History of actions that can be traversed
        /// </summary>
        private List<Action> history = new List<Action>();

        /// <summary>
        /// Current index in the history
        /// </summary>
        private int historyIndex = 0;

        /// <summary>
        /// panelForward MouseMove event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelForward_MouseMove(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelForward.Tag)
            {
                if (this.panelForward.Bounds.Contains(this.panelForward.Parent.PointToClient(Control.MousePosition)))
                {
                    this.panelForward.Capture = true;
                    this.panelForward.BackgroundImage = Properties.Resources.ForwardHover;
                }
                else
                {
                    this.panelForward.Capture = false;
                    this.panelForward.BackgroundImage = Properties.Resources.Forward;
                }
            }
        }

        /// <summary>
        /// panelBack MouseMove event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelBack_MouseMove(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelBack.Tag)
            {
                if (panelBack.Bounds.Contains(panelBack.Parent.PointToClient(Control.MousePosition)))
                {
                    this.panelBack.Capture = true;
                    this.panelBack.BackgroundImage = Properties.Resources.BackHover;
                }
                else
                {
                    this.panelBack.Capture = false;
                    this.panelBack.BackgroundImage = Properties.Resources.Back;
                }
            }
        }

        /// <summary>
        /// panelBack MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelBack_MouseDown(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelBack.Tag)
            {
                this.panelBack.BackgroundImage = Properties.Resources.BackClicked;
            }
        }

        /// <summary>
        /// panelForward MouseDown event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelForward_MouseDown(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelForward.Tag)
            {
                this.panelForward.BackgroundImage = Properties.Resources.ForwardClicked;
            }
        }

        /// <summary>
        /// panelBack MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelBack_MouseUp(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelBack.Tag)
            {
                this.panelBack.BackgroundImage = Properties.Resources.Back;
            }
        }

        /// <summary>
        /// panelForward MouseUp event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelForward_MouseUp(object sender, MouseEventArgs e)
        {
            if ((bool)this.panelForward.Tag)
            {
                this.panelForward.BackgroundImage = Properties.Resources.Forward;
            }
        }

        /// <summary>
        /// panelBack MouseClick event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelBack_MouseClick(object sender, MouseEventArgs e)
        {
            this.GoBack();
        }

        /// <summary>
        /// panelForward Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void panelForward_Click(object sender, EventArgs e)
        {
            this.GoForward();
        }

        /// <summary>
        /// Adds an action to the history queue
        /// </summary>
        /// <param name="action">Action to add</param>
        private void AddHistoryAction(Action action)
        {
            this.history.RemoveRange(this.historyIndex + 1, this.history.Count - this.historyIndex - 1);
            this.history.Add(action);
            this.historyIndex++;

            if (this.history.Count > 1)
            {
                this.panelBack.Tag = true;
                this.panelBack.BackgroundImage = Properties.Resources.Back;
            }

            if (this.historyIndex == this.history.Count - 1)
            {
                this.panelForward.Tag = false;
                this.panelForward.BackgroundImage = Properties.Resources.ForwardDisabled;
            }
        }

        /// <summary>
        /// Goes back in the history
        /// </summary>
        private void GoBack()
        {
            if ((bool)this.panelBack.Tag)
            {
                this.history[--this.historyIndex].Invoke();

                this.panelForward.Tag = true;
                this.panelForward.BackgroundImage = Properties.Resources.Forward;

                if (this.historyIndex == 0)
                {
                    this.panelBack.Tag = false;
                    this.panelBack.BackgroundImage = Properties.Resources.BackDisabled;
                }
            }
        }

        /// <summary>
        /// Goes forward in the history
        /// </summary>
        private void GoForward()
        {
            if ((bool)this.panelForward.Tag)
            {
                this.history[++this.historyIndex].Invoke();

                this.panelBack.Tag = true;
                this.panelBack.BackgroundImage = Properties.Resources.Back;

                if (this.historyIndex == this.history.Count - 1)
                {
                    this.panelForward.Tag = false;
                    this.panelForward.BackgroundImage = Properties.Resources.ForwardDisabled;
                }
            }
        }
    }
}
