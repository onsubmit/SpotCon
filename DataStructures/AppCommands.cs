//-----------------------------------------------------------------------
// <copyright file="AppCommands.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon.DataStructures
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// App commands
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "http://msdn.microsoft.com/en-us/library/windows/desktop/ms646275(v=vs.85).aspx")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "http://msdn.microsoft.com/en-us/library/windows/desktop/ms646275(v=vs.85).aspx.")]
    public class AppCommands
    {
        /// <summary>
        /// WM_APPCOMMAND
        /// </summary>
        public const int WM_APPCOMMAND = 0x0319;

        /// <summary>
        /// VK_CONTROL
        /// </summary>
        public const int VK_CONTROL = 0x11;

        /// <summary>
        /// VK_DOWN
        /// </summary>
        public const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// WM_KEYDOWN
        /// </summary>
        public const int WM_KEYUP = 0x0101;

        /// <summary>
        /// VK_UP
        /// </summary>
        public const int VK_UP = 0x26;

        /// <summary>
        /// VK_DOWN
        /// </summary>
        public const int VK_DOWN = 0x28;

        /// <summary>
        /// APPCOMMAND_VOLUME_MUTE (8)
        /// </summary>
        public const int APPCOMMAND_VOLUME_MUTE = 524288;

        /// <summary>
        /// APPCOMMAND_VOLUME_DOWN (9)
        /// </summary>
        public const int APPCOMMAND_VOLUME_DOWN = 589824;

        /// <summary>
        /// APPCOMMAND_VOLUME_UP (10)
        /// </summary>
        public const int APPCOMMAND_VOLUME_UP = 655360;

        /// <summary>
        /// APPCOMMAND_MEDIA_NEXTTRACK (11)
        /// </summary>
        public const int APPCOMMAND_MEDIA_NEXTTRACK = 720896;

        /// <summary>
        /// APPCOMMAND_MEDIA_PREVIOUSTRACK (12)
        /// </summary>
        public const int APPCOMMAND_MEDIA_PREVIOUSTRACK = 786432;

        /// <summary>
        /// APPCOMMAND_MEDIA_STOP (13)
        /// </summary>
        public const int APPCOMMAND_MEDIA_STOP = 851968;

        /// <summary>
        /// APPCOMMAND_MEDIA_PLAY_PAUSE (14)
        /// </summary>
        public const int APPCOMMAND_MEDIA_PLAY_PAUSE = 917504;
    }
}
