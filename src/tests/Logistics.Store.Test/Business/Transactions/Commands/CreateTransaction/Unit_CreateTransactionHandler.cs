// -----------------------------------------------------------------------
// <copyright file="Unit_CreateTransactionHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Transactions.Commands.CreateTransaction
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Store.API.Business.Transactions.Commands.CreateTransaction;
    using Logistics.Store.API.Data.Models;
    using Logistics.Store.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Unit_CreateTransactionHandler tests the CreateTransactionHandler.
    /// </summary>
    public class Unit_CreateTransactionHandler : StoresUnitTestBase<(CreateTransactionCommand command, CreateTransactionHandler handler)>
    {
        /// <summary>
        /// Passes if command is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if command is valid")]
        public async Task Pass_if_command_is_valid()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.NotEmpty(response.Id);
            Assert.NotEmpty(response.Status);
            Assert.True(response.TotalAmount > 0);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (CreateTransactionCommand command, CreateTransactionHandler handler) Build()
        {
            var products = this.Repository.All<Product>();
            var accounts = this.Repository.All<Account>();

            var randomProducts = this.Faker.PickRandom(products, this.Faker.Random.Int(1, 5));
            var randomAccount = this.Faker.PickRandom(accounts, 1).First();

            var handler = new CreateTransactionHandler(this.Repository);
            var command = new CreateTransactionCommandFaker().Generate();

            command.UserName = randomAccount.UserName;

            foreach (var product in randomProducts)
            {
                command.OrderedProducts.Add(product.Id, this.Faker.Random.Int(1, 5));
            }

            return (command, handler);
        }
    }
}