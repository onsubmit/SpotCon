//-----------------------------------------------------------------------
// <copyright file="AddNewCollection.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// AddNewCollection dialog
    /// </summary>
    public partial class AddNewCollection : Form
    {
        /// <summary>
        /// Initializes a new instance of the AddNewCollection class.
        /// </summary>
        public AddNewCollection()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets the new collection name
        /// </summary>
        public string CollectionName { get; private set; }

        /// <summary>
        /// buttonOK Click event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            this.CollectionName = this.textBoxCollection.Text;
        }

        /// <summary>
        /// textBoxCollection TextChanged event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void TextBoxCollection_TextChanged(object sender, EventArgs e)
        {
            this.buttonOK.Enabled = !string.IsNullOrEmpty(this.textBoxCollection.Text);
        }
    }
}
