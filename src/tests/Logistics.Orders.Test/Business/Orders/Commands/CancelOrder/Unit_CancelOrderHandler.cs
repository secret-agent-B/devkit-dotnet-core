// -----------------------------------------------------------------------
// <copyright file="Unit_CancelOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.CancelOrder
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Communication.Orders.Messages.Events;
    using Logistics.Orders.API.Business.Orders.Commands.CancelOrder;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The unit test for cancel order handler.
    /// </summary>
    public class Unit_CancelOrderHandler : OrdersUnitTestBase<(CancelOrderCommand command, CancelOrderHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_CancelOrderHandler" /> class.
        /// </summary>
        public Unit_CancelOrderHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Passes if order got cancelled.
        /// </summary>
        [Fact(DisplayName = "Passes if order got cancelled")]
        public async Task Pass_if_order_got_cancelled()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);

            var lastStatus = response.Statuses.Last();

            Assert.Equal(StatusCode.ClientDisputed.Value, lastStatus.Value);
            Assert.Equal(command.UserName, lastStatus.UserName);
            Assert.Equal(command.Comment, lastStatus.Comments);

            Assert.True(await this.TestHarness.Published.Any<IOrderCancelled>());
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (CancelOrderCommand command, CancelOrderHandler handler) Build()
        {
            var order = this.Repository.All<Order>().First();

            var command = new CancelOrderCommand
            {
                Id = order.Id,
                UserName = this.Faker.Person.Email,
                Comment = this.Faker.Rant.Review()
            };
            var handler = new CancelOrderHandler(this.Repository, this.TestHarness.Bus);

            return (command, handler);
        }
    }
}