//-----------------------------------------------------------------------
// <copyright file="ImportPlugins.cs" company="Andy Young">
//     Copyright (c) Andy Young. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace SpotCon
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using SpotCon.PlaylistImporter;

    /// <summary>
    /// Methods for input plugins
    /// </summary>
    public partial class SpotConForm : Form
    {
        /// <summary>
        /// Loads any playlist import plugins
        /// </summary>
        private void LoadImportPlugins()
        {
            Type pluginType = typeof(IPlaylistImporter);
            var plugins = new List<IPlaylistImporter>();
            foreach (string filename in Directory.GetFiles(Path.Combine(SpotConForm.AppDataFolder, "Plugins"), "*.dll"))
            {
                Assembly currentAssembly = Assembly.LoadFrom(filename);
                foreach (Type type in currentAssembly.GetExportedTypes().Where(t => t.GetInterface(pluginType.FullName) != null))
                {
                    try
                    {
                        IPlaylistImporter plugin = (IPlaylistImporter)Assembly.GetAssembly(pluginType).CreateInstance(
                            typeName: type.FullName,
                            args: null,
                            ignoreCase: false,
                            bindingAttr: BindingFlags.CreateInstance,
                            binder: null,
                            culture: null,
                            activationAttributes: null);

                        plugins.Add(plugin);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.ToString(), Properties.Resources.PluginLoadError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            foreach (var plugin in plugins)
            {
                ToolStripItem item = toolStripMenuItemImportPlaylist.DropDownItems.Add(plugin.MenuItem);
                item.Click += (sender, e) =>
                {
                    plugin.MainForm.ShowDialog(this);
                    this.FindNewTracks(plugin);
                };
            }
        }
    }
}
