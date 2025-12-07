// -----------------------------------------------------------------------
// <copyright file="UnitTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Test
{
    using System;
    using Devkit.Data;
    using LiteDB;
    using NUnit.Framework;

    /// <summary>
    /// The base class that helps facilitate unit tests.
    /// </summary>
    /// <typeparam name="T">The type of input to the handler that is being tested.</typeparam>
    /// <seealso cref="TestBase{T}" />
    [Category("Unit Test")]
    public abstract class UnitTestBase<T> : TestBase<T>
    {
        /// <summary>
        /// The database.
        /// </summary>
        private readonly LiteDatabase _db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitTestBase{T}" /> class.
        /// </summary>
        protected UnitTestBase()
            : base()
        {
            // setup the database.
            this._db = new LiteDatabase(":memory:");
            this.Repository = new LiteDbRepository(this._db);

            // seed the database.
            this.SeedDatabase();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            this._db.Dispose();
        }
    }
}