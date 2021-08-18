// -----------------------------------------------------------------------
// <copyright file="RatingsIntegrationTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test
{
    using Devkit.Ratings.Test.Fakers;
    using Devkit.Test;

    /// <summary>
    /// RatingsIntegrationTestBase class is the integration test base for Ratings API.
    /// </summary>
    public class RatingsIntegrationTestBase<TRequest> : IntegrationTestBase<TRequest, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingsIntegrationTestBase{TRequest}"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public RatingsIntegrationTestBase(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            this.Repository.AddRangeWithAudit(new RatingFaker().Generate(10));
        }
    }
}