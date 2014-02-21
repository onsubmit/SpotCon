    // -----------------------------------------------------------------------
    // <copyright file="TrackRowComparer.cs" company="Andy Young">
    // Copyright (c) Andy Young. All rights reserved.
    // </copyright>
    // -----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

        /// <summary>
        /// RowComparer class
        /// </summary>
        public class TrackRowComparer : System.Collections.IComparer
        {
            #region Fields

            /// <summary>
            /// Column containing the values being compared
            /// </summary>
            private int column;

            /// <summary>
            /// Indicates if we are sorting by ascending or descending values
            /// </summary>
            private int sortOrderModifier = 1;

            /// <summary>
            /// Type of values being compared
            /// </summary>
            private Type type;

            #endregion Fields

            #region Constructor

            /// <summary>
            /// Initializes a new instance of the RowComparer class
            /// </summary>
            /// <param name="column">Column to sort by</param>
            /// <param name="sortOrder">Ascending or descending</param>
            /// <param name="type">Type of values being compared</param>
            public TrackRowComparer(int column, SortOrder sortOrder, Type type)
            {
                this.column = column;
                this.type = type;
                if (sortOrder == SortOrder.Descending)
                {
                    this.sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    this.sortOrderModifier = 1;
                }
            }

            #endregion Constructor

            #region Methods

            /// <summary>
            /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
            /// </summary>
            /// <param name="x">First object to be compared</param>
            /// <param name="y">Second object to be compared</param>
            /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
            public int Compare(object x, object y)
            {
                DataGridViewRow row1 = (DataGridViewRow)x;
                DataGridViewRow row2 = (DataGridViewRow)y;

                if (null == row1.Cells[this.column].Value && null != row2.Cells[this.column].Value)
                {
                    return -1;
                }

                if (null == row1.Cells[this.column].Value && null == row2.Cells[this.column].Value)
                {
                    return 0;
                }

                if (null != row1.Cells[this.column].Value && null == row2.Cells[this.column].Value)
                {
                    return 1;
                }

                if (this.type == typeof(double))
                {
                    double doubleA = (double)row1.Cells[this.column].Tag;
                    double doubleB = (double)row2.Cells[this.column].Tag;
                    return doubleA.CompareTo(doubleB) * this.sortOrderModifier;
                }

                return string.Compare(row1.Cells[this.column].Value.ToString(), row2.Cells[this.column].Value.ToString()) * this.sortOrderModifier;
            }

            #endregion Methods
    }

}
