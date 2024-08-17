// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyProfileHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.CQRS.Users.Queries.GetMyProfile
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.Exceptions;
    using Devkit.Security.Business.Users.Queries.GetMyProfile;
    using Devkit.Security.Data.Models;
    using Devkit.Security.Test.Fakers;
    using Microsoft.AspNetCore.Identity;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Unit_GetMyProfileHandler class is the unit test for GetMyProfileHandler.
    /// </summary>
    /// <seealso cref="SecurityUnitTestBase{(GetMyProfileQuery query, GetMyProfileHandler handler)}" />
    public class Unit_GetMyProfileHandler : SecurityUnitTestBase<(GetMyProfileQuery query, GetMyProfileHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_GetMyProfileHandler"/> class.
        /// </summary>
        public Unit_GetMyProfileHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to get my profile.
        /// </summary>
        [TestCase(TestName = "Should be able to get my profile")]
        public async Task Should_be_able_to_get_my_profile()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.That(response.IsSuccessful);
            Assert.Equals(query.UserName, response.UserName);
            Assert.Equals(query.UserName, response.Email);
            Assert.AreNotEqual(default, response.FirstName);
            Assert.AreNotEqual(default, response.MiddleName);
            Assert.AreNotEqual(default, response.LastName);
            Assert.AreNotEqual(default, response.Address1);
            Assert.AreNotEqual(default, response.Address2);
            Assert.AreNotEqual(default, response.City);
            Assert.AreNotEqual(default, response.Province);
            Assert.AreNotEqual(default, response.Country);
            Assert.AreNotEqual(default, response.ZipCode);
            Assert.IsNotEmpty(response.IdentificationCards);

            foreach (var responseIdentificationCard in response.IdentificationCards)
            {
                Assert.AreNotEqual(default, responseIdentificationCard.Number);
                Assert.AreNotEqual(default, responseIdentificationCard.Type);
                Assert.AreNotEqual(default, responseIdentificationCard.ImageId);
            }
        }

        /// <summary>
        /// Should return not found if no match was found.
        /// </summary>
        [TestCase(TestName = "Should return not found if no match was found")]
        public void Should_return_not_found_if_no_match_was_found()
        {
            var (query, _) = this.Build();

            var mock = new Mock<IUserStore<UserAccount>>();
            mock
                .Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string userName, CancellationToken token) => null);

            using var handler = new GetMyProfileHandler(this.Repository, mock.Object);

            Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetMyProfileQuery query, GetMyProfileHandler handler) Build()
        {
            var query = new GetMyProfileQuery
            {
                UserName = this.Faker.Person.Email
            };

            var handler = new GetMyProfileHandler(this.Repository, new UserStoreFaker().Generate());

            return (query, handler);
        }
    }
}