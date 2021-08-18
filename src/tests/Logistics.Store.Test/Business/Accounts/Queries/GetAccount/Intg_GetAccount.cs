// -----------------------------------------------------------------------
// <copyright file="Intg_GetAccount.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Accounts.Queries.GetAccount
{
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Store.API;
    using Logistics.Store.API.Business.Accounts.Queries;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The integration test for GetAccount.
    /// </summary>
    public class Intg_GetAccount : StoresIntegrationTestBase<GetAccountQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetAccount"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetAccount(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get the account using a valid account email.
        /// </summary>
        [Fact(DisplayName = "Should be able to get the account using a valid account email")]
        public async Task Should_be_able_to_get_the_account_using_a_valid_account_email()
        {
            var query = this.Build();
            var response = await this.GetAsync<AccountVM>($"/accounts/{query.UserName}");

            var account = this.Repository.GetOneOrDefault<Account>(x => x.UserName == query.UserName);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(account.Id, response.Payload.Id);
            Assert.Equal(query.UserName, response.Payload.UserName);
            Assert.Equal(account.UserName, response.Payload.UserName);
            Assert.Equal(account.AvailableCredits, response.Payload.AvailableCredits);
            Assert.Equal((int)account.Status, response.Payload.AccountStatus);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override GetAccountQuery Build()
        {
            var accounts = this.Repository.All<Account>();
            var account = this.Faker.PickRandom(accounts);

            return new GetAccountQuery { UserName = account.UserName };
        }
    }
}