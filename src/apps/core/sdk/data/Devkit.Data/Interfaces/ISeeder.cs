﻿// -----------------------------------------------------------------------
// <copyright file="ISeeder.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// The seeder interface.
    /// </summary>
    public interface ISeeder
    {
        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        Task Execute();
    }
}