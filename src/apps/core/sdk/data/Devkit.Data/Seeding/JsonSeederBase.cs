﻿// -----------------------------------------------------------------------
// <copyright file="JsonSeederBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data.Seeding
{
    using System.IO;
    using Devkit.Data.Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// Json seeder base class.
    /// </summary>
    /// <typeparam name="TSource">The type of the seed.</typeparam>
    /// <seealso cref="SeederBase{TSeed}" />
    public abstract class JsonSeederBase<TSource> : SeederBase<TSource>
    {
        /// <summary>
        /// The seed file.
        /// </summary>
        private readonly string _seedFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSeederBase{TSeed}" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seedPath">The seed path.</param>
        protected JsonSeederBase(IRepository repository, string seedPath)
            : base(repository)
        {
            this._seedFile = seedPath;
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        public override void InitializeSource()
        {
            var jsonString = File.ReadAllText(this._seedFile);
            this.Source = JsonConvert.DeserializeObject<TSource>(jsonString);
        }
    }
}