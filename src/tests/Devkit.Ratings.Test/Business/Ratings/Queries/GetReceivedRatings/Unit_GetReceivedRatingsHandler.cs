// -----------------------------------------------------------------------
// <copyright file="Unit_GetReceivedRatingsHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Business.Ratings.Queries.GetReceivedRatings
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Ratings.Business.Ratings.Queries.GetReceivedRatings;
    using Devkit.Ratings.Data.Models;
    using Devkit.Ratings.Test.Fakers;
    using Devkit.Test;
    using NUnit.Framework;

    /// <summary>
    /// Unit_GetReceivedRatingsHandler class is the unit test for GetReceivedRatingsHandler.
    /// </summary>
    public class Unit_GetReceivedRatingsHandler : UnitTestBase<(GetReceivedRatingsHandler handler, GetReceivedRatingsQuery query)>
    {
        /// <summary>
        /// Should be able to pull received ratings.
        /// </summary>
        [TestCase(TestName = "Should be able to pull received ratings")]
        public async Task Should_be_able_to_pull_received_ratings()
        {
            var (handler, query) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.Items.Select(x => x.ReceiverUserName).Count(), Is.EqualTo(1));
            Assert.That(response.Items.First().ReceiverUserName, Is.EqualTo(query.ReceiverUserName));

            foreach (var responseItem in response.Items)
            {
                Assert.That(responseItem.CreatedOn >= query.StartDate && responseItem.CreatedOn <= query.EndDate, Is.True);
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetReceivedRatingsHandler handler, GetReceivedRatingsQuery query) Build()
        {
            var ratings = new RatingFaker().Generate(10);
            this.Repository.Add<Rating>(ratings);

            var rating = this.Faker.PickRandom(ratings);
            var handler = new GetReceivedRatingsHandler(this.Repository);
            var query = new GetReceivedRatingsQuery
            {
                ReceiverUserName = rating.ReceiverUserName,
                EndDate = rating.CreatedOn.AddDays(1),
                StartDate = rating.CreatedOn.AddDays(-1)
            };

            return (handler, query);
        }
    }
}