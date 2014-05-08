//-----------------------------------------------------------------------
// <copyright file="RefineFilterData.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.DataStructures
{
    using System;

    /// <summary>
    /// Data used when refining a filter
    /// </summary>
    public class RefineFilterData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefineFilterData"/> class
        /// </summary>
        public RefineFilterData()
        {
            this.RowIndex = 0;
            this.Buffer = null;
            this.LastKeyPressTime = DateTime.MinValue;
        }

        /// <summary>
        /// Gets or sets the index of the row where the search begins
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the last visible row
        /// </summary>
        public int LastVisibleRowIndex { get; set; }

        /// <summary>
        /// Gets or sets the search buffer
        /// </summary>
        public string Buffer { get; set; }

        /// <summary>
        /// Gets or sets the time at which the last key was pressed
        /// </summary>
        public DateTime LastKeyPressTime { get; set; }
    }
}
