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
    using Xunit;

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
        [Fact(DisplayName = "Should be able to get my profile")]
        public async Task Should_be_able_to_get_my_profile()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.UserName, response.UserName);
            Assert.Equal(query.UserName, response.Email);
            Assert.NotEqual(default, response.FirstName);
            Assert.NotEqual(default, response.MiddleName);
            Assert.NotEqual(default, response.LastName);
            Assert.NotEqual(default, response.Address1);
            Assert.NotEqual(default, response.Address2);
            Assert.NotEqual(default, response.City);
            Assert.NotEqual(default, response.Province);
            Assert.NotEqual(default, response.Country);
            Assert.NotEqual(default, response.ZipCode);
            Assert.NotEmpty(response.IdentificationCards);

            foreach (var responseIdentificationCard in response.IdentificationCards)
            {
                Assert.NotEqual(default, responseIdentificationCard.Number);
                Assert.NotEqual(default, responseIdentificationCard.Type);
                Assert.NotEqual(default, responseIdentificationCard.ImageId);
            }
        }

        /// <summary>
        /// Should return not found if no match was found.
        /// </summary>
        [Fact(DisplayName = "Should return not found if no match was found")]
        public async Task Should_return_not_found_if_no_match_was_found()
        {
            var (query, _) = this.Build();

            var mock = new Mock<IUserStore<UserAccount>>();
            mock
                .Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string userName, CancellationToken token) => null);

            using var handler = new GetMyProfileHandler(this.Repository, mock.Object);

            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(query, CancellationToken.None));
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