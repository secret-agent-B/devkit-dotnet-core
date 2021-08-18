// -----------------------------------------------------------------------
// <copyright file="StoresIntegrationTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test
{
    using Devkit.Test;
    using Logistics.Store.API;
    using Logistics.Store.Test.Fakers;

    /// <summary>
    /// The Stores API integration test base.
    /// </summary>
    public class StoresIntegrationTestBase<TRequest> : IntegrationTestBase<TRequest, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoresIntegrationTestBase{TRequest}"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public StoresIntegrationTestBase(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
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