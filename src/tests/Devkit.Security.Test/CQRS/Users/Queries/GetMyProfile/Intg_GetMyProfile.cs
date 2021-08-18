// -----------------------------------------------------------------------
// <copyright file="Intg_GetMyProfile.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.CQRS.Users.Queries.GetMyProfile
{
    using System.Threading.Tasks;
    using Devkit.Security.Business.Users.Queries.GetMyProfile;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Security.Data.Models;
    using Devkit.Test;
    using Xunit;

    /// <summary>
    /// Intg_GetMyProfile class is the integration test for GetMyProfile.
    /// </summary>
    public class Intg_GetMyProfile : SecurityIntegrationTestBase<GetMyProfileQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetMyProfile"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetMyProfile(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should return with a user view model.
        /// </summary>
        public async Task Should_return_with_a_user_view_model()
        {
            var query = this.Build();
            var response = await this.GetAsync<UserVM>($"/users/{query.UserName}");

            var user = this.Repository.GetOneOrDefault<UserAccount>(x => x.UserName == query.UserName);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.UserName, response.Payload.UserName);
        }
    }
}