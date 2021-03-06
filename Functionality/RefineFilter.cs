﻿//-----------------------------------------------------------------------
// <copyright file="RefineFilter.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Windows.Forms;
    using SpotCon.DataStructures;

    /// <summary>
    /// Refining filter functionality
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Artist filter data
        /// </summary>
        private RefineFilterData artistFilterData = new RefineFilterData();

        /// <summary>
        /// Album filter data
        /// </summary>
        private RefineFilterData albumFilterData = new RefineFilterData();

        /// <summary>
        /// Refines filter data 
        /// </summary>
        /// <param name="dataGrid">Data grid for which to refine the filter</param>
        /// <param name="newChar">Newly typed character</param>
        /// <param name="filterData">Filter data</param>
        private void RefineFilter(DataGridView dataGrid, char newChar, ref RefineFilterData filterData)
        {
            bool found = false;
            if (newChar == (int)Keys.Escape)
            {
                filterData = new RefineFilterData();
                found = true;
            }
            else
            {
                if (DateTime.Now > filterData.LastKeyPressTime + TimeSpan.FromSeconds(2.0))
                {
                    filterData.RowIndex = 1;
                    filterData.Buffer = newChar.ToString();
                }
                else
                {
                    filterData.Buffer += newChar;
                }

                filterData.LastKeyPressTime = DateTime.Now;

                while (filterData.RowIndex < dataGrid.Rows.Count)
                {
                    DataGridViewRow row = dataGrid.Rows[filterData.RowIndex];
                    string value = row.Cells[0].Value.ToString();

                    if (row.Visible)
                    {
                        filterData.LastVisibleRowIndex = filterData.RowIndex;

                        if (found = value.StartsWith(filterData.Buffer, StringComparison.OrdinalIgnoreCase))
                        {
                            break;
                        }

                        if (string.Compare(value, filterData.Buffer) > 0)
                        {
                            filterData.RowIndex = filterData.LastVisibleRowIndex;
                            break;
                        }
                    }

                    filterData.RowIndex++;
                }
            }

            if (found)
            {
                dataGrid.FirstDisplayedScrollingRowIndex = filterData.RowIndex;
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (row.Visible)
                    {
                        row.Selected = row.Index == filterData.RowIndex;
                    }
                }
            }
        }

        /// <summary>
        /// dataGridViewArtists KeyPress event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewArtists_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RefineFilter(this.dataGridViewArtists, e.KeyChar, ref this.artistFilterData);
        }

        /// <summary>
        /// dataGridViewAlbums KeyPress event
        /// </summary>
        /// <param name="sender">What raised the event</param>
        /// <param name="e">Event arguments</param>
        private void dataGridViewAlbums_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.RefineFilter(this.dataGridViewAlbums, e.KeyChar, ref this.albumFilterData);
        }
    }
}
