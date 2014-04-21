//-----------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System.Windows.Forms;

    /// <summary>
    /// Extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Clears the rows of a DataGridView while ignoring selection changes (assuming the Tag is a boolean which, when false, disabled selection change event handling
        /// </summary>
        /// <param name="dataGridView">DataGridView to clear</param>
        public static void IgnoreSelectionChangesAndClearRows(this DataGridView dataGridView)
        {
            dataGridView.Tag = false;
            dataGridView.Rows.Clear();
            dataGridView.Tag = true;
        }
    }
}
