// -----------------------------------------------------------------------
// <copyright file="Unit_GetAccountHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Accounts.Queries.GetAccount
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.Exceptions;
    using Logistics.Store.API.Business.Accounts.Queries;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The unit test for GetAccountHandler.
    /// </summary>
    public class Unit_GetAccountHandler : StoresUnitTestBase<(GetAccountQuery query, GetAccountHandler handler)>
    {
        /// <summary>
        /// Should be able to find account.
        /// </summary>
        [Fact(DisplayName = "Should be able to find account")]
        public async Task Should_be_able_to_find_account()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            var account = this.Repository.GetOneOrDefault<Account>(x => x.UserName == query.UserName);

            Assert.True(response.IsSuccessful);
            Assert.Equal(account.Id, response.Id);
            Assert.Equal(query.UserName, response.UserName);
            Assert.Equal(account.UserName, response.UserName);
            Assert.Equal(account.AvailableCredits, response.AvailableCredits);
            Assert.Equal((int)account.Status, response.AccountStatus);
        }

        /// <summary>
        /// Should return error if account is not found.
        /// </summary>
        [Fact(DisplayName = "Should return error if account is not found")]
        public async Task Should_return_error_if_account_is_not_found()
        {
            var (query, handler) = this.Build();
            query.UserName = this.Faker.Person.Email;

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetAccountQuery query, GetAccountHandler handler) Build()
        {
            var handler = new GetAccountHandler(this.Repository);
            var accounts = this.Repository.All<Account>();
            var account = this.Faker.PickRandom(accounts);

            var query = new GetAccountQuery
            {
                UserName = account.UserName
            };

            return (query, handler);
        }
    }
}