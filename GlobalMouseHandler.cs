//-----------------------------------------------------------------------
// <copyright file="GlobalMouseHandler.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Global mouse handler
    /// </summary>
    public class GlobalMouseHandler : IMessageFilter
    {
        /// <summary>
        /// M_XBUTTONDOWN
        /// </summary>
        private const int M_XBUTTONDOWN = 0x020B;

        /// <summary>
        /// MK_XBUTTON1
        /// </summary>
        private const int MK_XBUTTON1 = 65568;

        /// <summary>
        /// MK_XBUTTON2
        /// </summary>
        private const int MK_XBUTTON2 = 131136;

        /// <summary>
        /// Gets or sets the action to execute when the back button is pressed
        /// </summary>
        public Action BackAction { get; set; }

        /// <summary>
        /// Gets or sets the action to execute when the forward button is pressed
        /// </summary>
        public Action ForwardAction { get; set; }

        /// <summary>
        /// Filters out a message before it is dispatched.
        /// </summary>
        /// <param name="message">The message to be dispatched. You cannot modify this message.</param>
        /// <code>http://stackoverflow.com/questions/804374/capturing-mouse-events-from-every-component-on-c-sharp-winform</code>
        /// <returns>true to filter the message and stop it from being dispatched; false to allow the message to continue to the next filter or control.</returns>
        public bool PreFilterMessage(ref Message message)
        {
            if (message.Msg == M_XBUTTONDOWN)
            {
                int wParam = message.WParam.ToInt32();
                if (wParam == MK_XBUTTON1)
                {
                    this.BackAction.Invoke();
                    return true;
                }
                else if (wParam == MK_XBUTTON2)
                {
                    this.ForwardAction.Invoke();
                    return true;
                }
            }

            return false;
        }
    }
}
