// -----------------------------------------------------------------------
// <copyright file="RatingsUnitTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test
{
    using System.Linq;
    using Devkit.Ratings.Test.Fakers;
    using Devkit.Test;
    using MassTransit.Testing;

    /// <summary>
    /// RatingsUnitTestBase class is the base class for Ratings Unit Tests.
    /// </summary>
    public class RatingsUnitTestBase<TSystemUnderTest> : UnitTestBase<TSystemUnderTest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingsUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        public RatingsUnitTestBase()
        {
            this.TestHarness = new InMemoryTestHarness();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="RatingsUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        ~RatingsUnitTestBase()
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
            var users = new UserFaker().Generate(5);
            var ratings = users
                .SelectMany(x => x.LastReceivedRatings)
                .Union(users.SelectMany(x => x.LastGivenRatings));

            this.Repository.AddRangeWithAudit(users);
            this.Repository.AddRangeWithAudit(ratings);
        }
    }
}