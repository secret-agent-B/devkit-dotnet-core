// -----------------------------------------------------------------------
// <copyright file="Unit_UpdateOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.UpdateOrder
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Orders.Commands.UpdateOrder;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Unit test for UpdateOrder.
    /// </summary>
    public class Unit_UpdateOrderHandler : OrdersUnitTestBase<(UpdateOrderCommand command, UpdateOrderHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_UpdateOrderHandler"/> class.
        /// </summary>
        public Unit_UpdateOrderHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to update an order.
        /// </summary>
        [Fact(DisplayName = "Should be able to update an order")]
        public async Task Should_be_able_to_update_an_order()
        {
            var (command, handler) = this.Build();

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.False(string.IsNullOrEmpty(response.Id));
            Assert.Equal(command.RecipientName, response.RecipientName);
            Assert.Equal(command.RecipientPhone, response.RecipientPhone);
            Assert.Equal(command.Destination.Lat, response.Destination.Lat);
            Assert.Equal(command.Destination.Lng, response.Destination.Lng);
            Assert.Equal(command.Destination.DisplayAddress, response.Destination.DisplayAddress);
            Assert.Equal(command.EstimatedDistance.Text, response.EstimatedDistance.Text);
            Assert.Equal(command.EstimatedDistance.Value, response.EstimatedDistance.Value);
            Assert.Equal(command.EstimatedItemWeight, response.EstimatedItemWeight);
            Assert.Equal(command.ItemName, response.ItemName);
            Assert.Equal(command.RequestSignature, response.RequestSignature);
            Assert.Equal(command.RequestInsulation, response.RequestInsulation);
            Assert.Equal(command.UserName, response.Statuses.Last().UserName);
            Assert.Equal(command.Cost.DriverFee, response.Cost.DriverFee);
            Assert.Equal(command.Cost.DistanceInKm, response.Cost.DistanceInKm);
            Assert.Equal(command.Cost.SystemFee, response.Cost.SystemFee);
            Assert.Equal(command.Cost.Tax, response.Cost.Tax);
            Assert.Equal(command.Cost.Total, response.Cost.Total);

            foreach (var item in command.SpecialInstructions)
            {
                Assert.True(response.SpecialInstructions.Any(x => x.Description == item.Description && x.IsCompleted == item.IsCompleted));
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (UpdateOrderCommand command, UpdateOrderHandler handler) Build()
        {
            var command = new UpdateOrderCommandFaker().Generate();
            var handler = new UpdateOrderHandler(this.Repository, this.TestHarness.Bus);

            command.Id = this.Repository.All<Order>().First().Id;

            return (command, handler);
        }
    }
}