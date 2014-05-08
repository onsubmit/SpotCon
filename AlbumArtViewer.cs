//-----------------------------------------------------------------------
// <copyright file="AlbumArtViewer.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    /// <summary>
    /// Album art viewer
    /// </summary>
    public partial class AlbumArtViewer : Form
    {
        /// <summary>
        /// Name of album
        /// </summary>
        private string albumName;

        /// <summary>
        /// Image to display
        /// </summary>
        private Image image;

        /// <summary>
        /// Initializes a new instance of the <see cref="AlbumArtViewer"/> class
        /// </summary>
        /// <param name="albumName">Name of the album</param>
        /// <param name="image">Image to display</param>
        public AlbumArtViewer(string albumName, Image image)
        {
            this.albumName = albumName;
            this.image = image;
            this.InitializeComponent();
        }

        /// <summary>
        /// AlbumArtViewer Load event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void AlbumArtViewer_Load(object sender, EventArgs e)
        {
            this.Text = this.albumName;
            this.pictureBox.Image = this.image;
        }

        /// <summary>
        /// AlbumArtViewer Resize event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void AlbumArtViewer_Resize(object sender, EventArgs e)
        {
            this.Width = this.Height;
        }
    }
}
