// -----------------------------------------------------------------------
// <copyright file="StoresUnitTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test
{
    using Devkit.Test;
    using Logistics.Store.Test.Fakers;
    using MassTransit.Testing;

    /// <summary>
    /// The Stores API unit test base.
    /// </summary>
    public class StoresUnitTestBase<TSystemUnderTest> : UnitTestBase<TSystemUnderTest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoresUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        public StoresUnitTestBase()
        {
            this.TestHarness = new InMemoryTestHarness();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="StoresUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        ~StoresUnitTestBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the test harness.
        /// </summary>
        /// <value>
        /// The test harness.
        /// </value>
        protected InMemoryTestHarness TestHarness { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            this.TestHarness.Stop();
            this.TestHarness.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            var productFaker = new ProductFaker();
            var accountFaker = new AccountFaker();

            this.Repository.AddRangeWithAudit(accountFaker.Generate(10));
            this.Repository.AddRangeWithAudit(productFaker.Generate(100));
        }
    }
}