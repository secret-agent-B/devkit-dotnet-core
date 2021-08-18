// -----------------------------------------------------------------------
// <copyright file="Intg_CreateTransaction.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Transactions.Commands.CreateTransaction
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Store.API;
    using Logistics.Store.API.Business.Transactions.Commands.CreateTransaction;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;
    using Logistics.Store.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Intg_CreateTransaction is the integration test for CreateTransaction.
    /// </summary>
    public class Intg_CreateTransaction : StoresIntegrationTestBase<CreateTransactionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_CreateTransaction"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_CreateTransaction(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to create a transaction.
        /// </summary>
        [Fact(DisplayName = "Should be able to create a transaction")]
        public async Task Should_be_able_to_create_a_transaction()
        {
            var command = this.Build();
            var response = await this.PostAsync<TransactionVM>("/transactions", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.True(response.Payload.IsSuccessful);
            Assert.Equal(command.UserName, response.Payload.Account.UserName);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override CreateTransactionCommand Build()
        {
            var products = this.Repository.All<Product>();
            var accounts = this.Repository.All<Account>();

            var randomProducts = this.Faker.PickRandom(products, this.Faker.Random.Int(1, 5));
            var randomAccount = this.Faker.PickRandom(accounts, 1).First();

            var command = new CreateTransactionCommandFaker().Generate();

            command.UserName = randomAccount.UserName;

            foreach (var product in randomProducts)
            {
                command.OrderedProducts.Add(product.Id, this.Faker.Random.Int(1, 5));
            }

            return command;
        }
    }
}