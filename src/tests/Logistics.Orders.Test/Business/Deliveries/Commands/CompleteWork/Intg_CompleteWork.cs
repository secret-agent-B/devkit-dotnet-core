// -----------------------------------------------------------------------
// <copyright file="Intg_CompleteWork.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.CompleteWork
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Commands.CompleteWork;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using MongoDB.Driver;
    using Xunit;

    /// <summary>
    /// Integration test for completing work.
    /// </summary>
    public class Intg_CompleteWork : OrdersIntegrationTestBase<CompleteWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_CompleteWork"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_CompleteWork(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to complete work if status is in transit.
        /// </summary>
        [Fact(DisplayName = "Should be able to complete work if status is in transit")]
        public async Task Should_be_able_to_complete_work_if_status_is_in_transit()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.PickedUp);
            var response = await this.PatchAsync<OrderVM>("/deliveries/complete", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.NotNull(response.Payload.CompletedPhoto);
            Assert.Equal(command.Id, response.Payload.Id);
            Assert.Equal(command.UserName, response.Payload.Statuses.Last().UserName);
            Assert.Equal(command.Comments, response.Payload.Statuses.Last().Comments);
            Assert.Equal(StatusCode.Completed.Value, response.Payload.CurrentStatus);
        }

        /// <summary>
        /// Should not be able to complete if status is already complete.
        /// </summary>
        [Fact(DisplayName = "Should not be able to complete if status is already complete")]
        public async Task Should_not_be_able_to_complete_if_status_is_already_complete()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.Completed);
            var response = await this.PutAsync<OrderVM>("/deliveries/complete", command);

            Assert.False(response.IsSuccessfulStatusCode);
        }

        /// <summary>
        /// Should not be able to complete if status is client disputed.
        /// </summary>
        [Fact(DisplayName = "Should not be able to complete if status is client disputed")]
        public async Task Should_not_be_able_to_complete_if_status_is_client_disputed()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.ClientDisputed);

            var response = await this.PutAsync<OrderVM>("/deliveries/complete", command);

            Assert.False(response.IsSuccessfulStatusCode);
        }

        /// <summary>
        /// Should not be able to complete if status is driver disputed.
        /// </summary>
        [Fact(DisplayName = "Should not be able to complete if status is driver disputed")]
        public async Task Should_not_be_able_to_complete_if_status_is_driver_disputed()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.DriverDisputed);
            var response = await this.PutAsync<OrderVM>("/deliveries/complete", command);

            Assert.False(response.IsSuccessfulStatusCode);
        }

        /// <summary>
        /// Should not be able to complete if status is just booked.
        /// </summary>
        [Fact(DisplayName = "Should not be able to complete if status is just booked")]
        public async Task Should_not_be_able_to_complete_if_status_is_just_booked()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.Booked);
            var response = await this.PutAsync<OrderVM>("/deliveries/complete", command);

            Assert.False(response.IsSuccessfulStatusCode);
        }

        /// <summary>
        /// Should not be able to complete if it is driver disputed.
        /// </summary>
        [Fact(DisplayName = "Should not be able to complete if status is unknown")]
        public async Task Should_not_be_able_to_complete_if_status_is_unknown()
        {
            var command = this.Build();
            this.SetOrderStatus(command, StatusCode.Unknown);
            var response = await this.PutAsync<OrderVM>("/deliveries/complete", command);

            Assert.False(response.IsSuccessfulStatusCode);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override CompleteWorkCommand Build()
        {
            var orders = this.Repository.All<Order>();
            var order = this.Faker.PickRandom(orders);

            return new CompleteWorkCommand
            {
                Id = order.Id,
                UserName = order.DriverUserName,
                Comments = this.Faker.Rant.Review(),
                Photo = "image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs="
            };
        }

        /// <summary>
        /// Sets the order status.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="statusCode">The status code.</param>
        private void SetOrderStatus(CompleteWorkCommand command, StatusCode statusCode)
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
                builder => builder
                    .Set(x => x.CurrentStatus, statusCode.Value)
                    .Set(x => x.Statuses, statuses));
        }
    }
}