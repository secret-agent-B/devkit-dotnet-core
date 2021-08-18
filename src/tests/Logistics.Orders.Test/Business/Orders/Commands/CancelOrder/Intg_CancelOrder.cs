// -----------------------------------------------------------------------
// <copyright file="Intg_CancelOrder.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.CancelOrder
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Orders.Commands.CancelOrder;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The integration test for CancelOrder.
    /// </summary>
    public class Intg_CancelOrder : OrdersIntegrationTestBase<CancelOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_CancelOrder"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_CancelOrder(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to cancel an order if not picked up.
        /// </summary>
        public async Task Should_be_able_to_cancel_an_order_if_not_picked_up()
        {
            var command = this.Build();
            var response = await this.PatchAsync<OrderVM>($"/orders/{command.Id}/cancel", command);

            var lastStatus = response.Payload.Statuses.Last();

            Assert.True(response.IsSuccessfulStatusCode);

            Assert.Equal(StatusCode.ClientDisputed.Value, lastStatus.Value);
            Assert.Equal(command.UserName, lastStatus.UserName);
            Assert.Equal(command.Comment, lastStatus.Comments);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override CancelOrderCommand Build()
        {
            var orders = this.Repository.All<Order>().ToList();
            var random = this.Faker.PickRandom(orders.Where(x => x.Statuses.Last().Value == StatusCode.Booked.Value));

            return new CancelOrderCommand
            {
                Id = random.Id,
                Comment = this.Faker.Rant.Review(),
                UserName = random.ClientUserName
            };
        }
    }
}