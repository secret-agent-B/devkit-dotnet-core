// -----------------------------------------------------------------------
// <copyright file="Unit_AssignWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.CancelWork
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Deliveries.Commands.CancelWork;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The Unit_AssignWorkHandler is the unit test for AssignWorkHandler.
    /// </summary>
    public class Unit_CancelWorkHandler : OrdersUnitTestBase<(CancelWorkCommand command, CancelWorkHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_CancelWorkHandler"/> class.
        /// </summary>
        public Unit_CancelWorkHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should fail if order identifier is invalid.
        /// </summary>
        [Fact(DisplayName = "Should fail if order identifier is invalid")]
        public async Task Should_fail_if_order_id_is_invalid()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is booked.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is booked")]
        public async Task Should_fail_if_status_is_booked()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.Booked);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is client disputed.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is client disputed")]
        public async Task Should_fail_if_status_is_client_disputed()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.ClientDisputed);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is completed.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is completed")]
        public async Task Should_fail_if_status_is_completed()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.Completed);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is driver disputed.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is driver disputed")]
        public async Task Should_fail_if_status_is_driver_disputed()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.DriverDisputed);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is in transit.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is in transit")]
        public async Task Should_fail_if_status_is_in_transit()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.PickedUp);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Should fail if status is unknown.
        /// </summary>
        [Fact(DisplayName = "Should fail if status is unknown")]
        public async Task Should_fail_if_status_is_unknown()
        {
            var (command, handler) = this.Build();
            this.SetOrderStatus(command, StatusCode.Unknown);
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.False(response.IsSuccessful);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (CancelWorkCommand command, CancelWorkHandler handler) Build()
        {
            var orders = this.Repository.All<Order>().Where(x => x.Statuses.Last().Value == StatusCode.Booked.Value);
            var order = this.Faker.PickRandom(orders);
            var command = new CancelWorkCommand
            {
                Id = order.Id,
                UserName = this.Faker.Person.Email,
                Comment = this.Faker.Rant.Review()
            };
            var handler = new CancelWorkHandler(this.Repository, this.TestHarness.Bus);

            return (command, handler);
        }

        /// <summary>
        /// Sets the order status.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="statusCode">The status code.</param>
        private void SetOrderStatus(CancelWorkCommand command, StatusCode statusCode)
        {
            var order = this.Repository.GetOneOrDefault<Order>(x => x.Id == command.Id);
            var statuses = order.Statuses;

            statuses.Add(new Status
            {
                Value = statusCode.Value,
                UserName = command.UserName,
                Comments = this.Faker.Rant.Review(),
                Timestamp = DateTime.UtcNow
            });

            this.Repository.UpdateWithAudit<Order>(
                x => x.Id == command.Id,
                builder => builder.Set(x => x.Statuses, statuses));
        }
    }
}