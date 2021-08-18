// -----------------------------------------------------------------------
// <copyright file="Unit_SubmitOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.SubmitOrder
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Communication.Orders.Messages.Events;
    using Logistics.Orders.API.Business.Deliveries.Commands.AssignWork;
    using Logistics.Orders.API.Business.Orders.Commands.SubmitOrder;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using MediatR;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit test for SubmitOrder handler.
    /// </summary>
    /// <seealso cref="OrdersUnitTestBase{(SubmitOrderCommand command, SubmitOrderHandler handler)}" />
    public class Unit_SubmitOrderHandler : OrdersUnitTestBase<(SubmitOrderCommand command, SubmitOrderHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_SubmitOrderHandler"/> class.
        /// </summary>
        public Unit_SubmitOrderHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to submit a new order.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Should be able to submit a new order")]
        public async Task Should_be_able_to_submit_a_new_order()
        {
            var (command, handler) = this.Build();
            command.DriverUserName = string.Empty;

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.True(!string.IsNullOrEmpty(response.Id));
            Assert.Equal(command.ClientUserName, response.ClientUserName);
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
            Assert.Equal(command.Cost.DriverFee, response.Cost.DriverFee);
            Assert.Equal(command.Cost.DistanceInKm, response.Cost.DistanceInKm);
            Assert.Equal(command.Cost.SystemFee, response.Cost.SystemFee);
            Assert.Equal(command.Cost.Tax, response.Cost.Tax);
            Assert.Equal(command.Cost.Total, response.Cost.Total);

            foreach (var item in command.SpecialInstructions)
            {
                Assert.True(response.SpecialInstructions.Any(x => x.Description == item && !x.IsCompleted));
            }
        }

        /// <summary>
        /// Should be able to submit a new order with assigned driver.
        /// </summary>
        [Fact(DisplayName = "Should be able to submit a new order with assigned driver")]
        public async Task Should_be_able_to_submit_a_new_order_with_assigned_driver()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful, string.Join(", ", response.Exceptions.Values.Select(x => x)));
            Assert.False(string.IsNullOrEmpty(response.Id));
            Assert.Equal(command.ClientUserName, response.ClientUserName);
            Assert.Equal(command.RecipientName, response.RecipientName);
            Assert.Equal(command.RecipientPhone, response.RecipientPhone);
            Assert.Equal(command.Destination.Lat, response.Destination.Lat);
            Assert.Equal(command.Destination.Lng, response.Destination.Lng);
            Assert.Equal(command.Destination.DisplayAddress, response.Destination.DisplayAddress);
            Assert.Equal(command.DriverUserName, response.DriverUserName);
            Assert.Equal(command.EstimatedDistance.Text, response.EstimatedDistance.Text);
            Assert.Equal(command.EstimatedDistance.Value, response.EstimatedDistance.Value);
            Assert.Equal(command.EstimatedItemWeight, response.EstimatedItemWeight);
            Assert.Equal(command.ItemName, response.ItemName);
            Assert.Equal(command.RequestSignature, response.RequestSignature);
            Assert.Equal(command.RequestInsulation, response.RequestInsulation);
            Assert.Equal(command.Cost.DriverFee, response.Cost.DriverFee);
            Assert.Equal(command.Cost.DistanceInKm, response.Cost.DistanceInKm);
            Assert.Equal(command.Cost.SystemFee, response.Cost.SystemFee);
            Assert.Equal(command.Cost.Tax, response.Cost.Tax);
            Assert.Equal(command.Cost.Total, response.Cost.Total);

            foreach (var item in command.SpecialInstructions)
            {
                Assert.True(response.SpecialInstructions.Any(x => x.Description == item));
            }

            Assert.True(await this.TestHarness.Published.Any<IOrderSubmitted>());
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (SubmitOrderCommand command, SubmitOrderHandler handler) Build()
        {
            var commandFaker = new SubmitOrderCommandFaker();
            var command = commandFaker.Generate();

            var mockOrderResponse = new OrderVM
            {
                Id = this.Faker.Random.Hexadecimal(24, string.Empty),
                ClientUserName = command.ClientUserName,
                CreatedOn = DateTime.UtcNow,
                Destination = new LocationVM
                {
                    DisplayAddress = command.Destination.DisplayAddress,
                    Lat = command.Destination.Lat,
                    Lng = command.Destination.Lng
                },
                DriverUserName = command.DriverUserName,
                EstimatedDistance = new DistanceVM
                {
                    Text = command.EstimatedDistance.Text,
                    Value = command.EstimatedDistance.Value
                },
                EstimatedItemWeight = command.EstimatedItemWeight,
                ItemName = command.ItemName,
                ItemPhoto = command.ItemPhoto,
                Origin = new LocationVM
                {
                    DisplayAddress = command.Origin.DisplayAddress,
                    Lat = command.Origin.Lat,
                    Lng = command.Origin.Lng
                },
                OriginPhoto = command.OriginPhoto,
                RequestInsulation = command.RequestInsulation,
                RequestSignature = command.RequestSignature
            };

            mockOrderResponse.SpecialInstructions.AddRange(
                command.SpecialInstructions.Select(x => new SpecialInstructionVM
                {
                    Description = x,
                    IsCompleted = false
                }));

            mockOrderResponse.Statuses.AddRange(new StatusVM[] {
                    new StatusVM(
                        new Status {
                            Value = StatusCode.Booked.Value,
                            Comments = $"Order created by {command.ClientUserName}.",
                            Timestamp = DateTime.UtcNow,
                            UserName = command.ClientUserName
                        }),
                    new StatusVM(
                        new Status {
                            Value = StatusCode.Assigned.Value,
                            Comments = $"Order ({this.Faker.Random.Hexadecimal(24, string.Empty)}) assigned to driver ({command.DriverUserName}).",
                            Timestamp = DateTime.UtcNow,
                            UserName = command.ClientUserName
                        })
                });

            var mockMediatr = new Mock<IMediator>();
            mockMediatr
                .Setup(x => x.Send(It.IsAny<AssignWorkCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockOrderResponse);

            var handler = new SubmitOrderHandler(this.Repository, mockMediatr.Object, this.TestHarness.Bus);

            return (command, handler);
        }
    }
}