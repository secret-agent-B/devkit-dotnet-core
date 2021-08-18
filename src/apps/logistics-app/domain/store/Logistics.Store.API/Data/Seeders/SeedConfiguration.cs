// -----------------------------------------------------------------------
// <copyright file="SeedConfiguration.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Seeders
{
    using Devkit.Data.Interfaces;

    /// <summary>
    /// The SeedConfiguration for the Store API.
    /// </summary>
    public class SeedConfiguration : ISeederConfig
    {
        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }
    }
}