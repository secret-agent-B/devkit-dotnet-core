// -----------------------------------------------------------------------
// <copyright file="Intg_UpdateOrder.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.UpdateOrder
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Orders.Commands.UpdateOrder;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Integration test for UpdateOrder.
    /// </summary>
    public class Intg_UpdateOrder : OrdersIntegrationTestBase<UpdateOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_UpdateOrder"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_UpdateOrder(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to submit a new order.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Should be able to submit a new order")]
        public async Task Should_be_able_to_update_an_order()
        {
            var command = this.Build();
            var response = await this.PatchAsync<OrderVM>($"/orders/{command.Id}", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.True(response.Payload.IsSuccessful);
            Assert.True(!string.IsNullOrEmpty(response.Payload.Id));
            Assert.Equal(command.RecipientName, response.Payload.RecipientName);
            Assert.Equal(command.RecipientPhone, response.Payload.RecipientPhone);
            Assert.Equal(command.Destination.Lat, response.Payload.Destination.Lat);
            Assert.Equal(command.Destination.Lng, response.Payload.Destination.Lng);
            Assert.Equal(command.Destination.DisplayAddress, response.Payload.Destination.DisplayAddress);
            Assert.Equal(command.EstimatedDistance.Text, response.Payload.EstimatedDistance.Text);
            Assert.Equal(command.EstimatedDistance.Value, response.Payload.EstimatedDistance.Value);
            Assert.Equal(command.EstimatedItemWeight, response.Payload.EstimatedItemWeight);
            Assert.Equal(command.ItemName, response.Payload.ItemName);
            Assert.Equal(command.RequestSignature, response.Payload.RequestSignature);
            Assert.Equal(command.RequestInsulation, response.Payload.RequestInsulation);
            Assert.Equal(command.Cost.DriverFee, response.Payload.Cost.DriverFee);
            Assert.Equal(command.Cost.DistanceInKm, response.Payload.Cost.DistanceInKm);
            Assert.Equal(command.Cost.SystemFee, response.Payload.Cost.SystemFee);
            Assert.Equal(command.Cost.Tax, response.Payload.Cost.Tax);
            Assert.Equal(command.Cost.Total, response.Payload.Cost.Total);

            foreach (var item in command.SpecialInstructions)
            {
                Assert.True(response.Payload.SpecialInstructions.Any(x => x.Description == item.Description && x.IsCompleted == item.IsCompleted));
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override UpdateOrderCommand Build()
        {
            var command = new UpdateOrderCommandFaker().Generate();
            var order = this.Repository.All<Order>().First();

            command.Id = order.Id;

            return command;
        }
    }
}