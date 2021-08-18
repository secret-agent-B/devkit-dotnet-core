// -----------------------------------------------------------------------
// <copyright file="Unit_PickUpWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.PickUpWork
{
    using System.Linq;
    using System.Threading;
    using Xunit;
    using System;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using MongoDB.Driver;

    /// <summary>
    /// Unit_PickUpWorkHandler class is the unit test for PickUpWorkHandler.
    /// </summary>
    public class Unit_PickUpWorkHandler : OrdersUnitTestBase<(PickUpWorkCommand command, PickUpWorkHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_PickUpWorkHandler"/> class.
        /// </summary>
        public Unit_PickUpWorkHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to update an order to picked up status.
        /// </summary>
        public async Task Should_be_able_to_update_an_order_to_picked_up_status()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.NotNull(response.PickedUpPhoto);
            Assert.Equal(command.Id, response.Id);
            Assert.Equal(command.UserName, response.Statuses.Last().UserName);
            Assert.Equal(command.Comments, response.Statuses.Last().Comments);
            Assert.Equal(StatusCode.Completed.Value, response.CurrentStatus);
        }

        /// <summary>
        /// Builds the modules that need to be tested.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (PickUpWorkCommand command, PickUpWorkHandler handler) Build()
        {
            var orders = this.Repository.All<Order>();
            var order = this.Faker.PickRandom(orders);
            var command = new PickUpWorkCommand
            {
                UserName = this.Faker.Person.Email,
                Id = order.Id,
                Comments = this.Faker.Rant.Review(),
                Photo = "image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs="
            };

            var statuses = order.Statuses;

            statuses.Add(new Status
            {
                Value = StatusCode.PickedUp.Value,
                UserName = command.UserName,
                Comments = this.Faker.Rant.Review(),
                Timestamp = DateTime.UtcNow
            });

            this.Repository.UpdateWithAudit<Order>(
                x => x.Id == command.Id,
                builder => builder
                    .Set(x => x.DriverUserName, command.UserName)
                    .Set(x => x.CurrentStatus, StatusCode.PickedUp.Value)
                    .Set(x => x.Statuses, statuses));

            var handler = new PickUpWorkHandler(this.Repository, this.TestHarness.Bus);

            return (command, handler);
        }
    }
}