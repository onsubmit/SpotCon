using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpotCon
{
    public partial class AlbumArtViewer : Form
    {
        private string albumName;
        private Image image;

        public AlbumArtViewer(string albumName, Image image)
        {
            this.albumName = albumName;
            this.image = image;
            InitializeComponent();
        }

        private void AlbumArtViewer_Load(object sender, EventArgs e)
        {
            this.Text = this.albumName;
            this.pictureBox.Image = this.image;
        }

        private void AlbumArtViewer_Resize(object sender, EventArgs e)
        {
            this.Width = this.Height;
        }
    }
}
