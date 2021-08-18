// -----------------------------------------------------------------------
// <copyright file="Intg_AssignWork.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.CancelWork
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Commands.CancelWork;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using MongoDB.Driver;
    using Xunit;

    /// <summary>
    /// The Intg_CancelWork is the integration test for cancelling work to a driver.
    /// </summary>
    public class Intg_CancelWork : OrdersIntegrationTestBase<CancelWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_CancelWork"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_CancelWork(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to cancel driver's work.
        /// </summary>
        [Fact(DisplayName = "Should be able to cancel driver's work")]
        public async Task Should_be_able_to_cancel_drivers_work()
        {
            var command = this.Build();
            var order = this.Repository.GetOneOrDefault<Order>(x => x.Id == command.Id);
            var statuses = order.Statuses;

            statuses.Add(new Status
            {
                Value = StatusCode.Assigned.Value,
                UserName = command.UserName,
                Comments = this.Faker.Random.AlphaNumeric(25),
                Timestamp = DateTime.UtcNow
            });

            this.Repository.UpdateWithAudit<Order>(
                x => x.Id == command.Id,
                builder => builder
                    .Set(x => x.Statuses, statuses)
                    .Set(x => x.DriverUserName, command.UserName));

            var response = await this.PatchAsync<OrderVM>("/deliveries/cancel/", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.NotEmpty(response.Payload.Statuses.Where(x => x.Code == StatusCode.DriverDisputed.DisplayName && x.UserName == command.UserName));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override CancelWorkCommand Build()
        {
            var order = this.Faker.PickRandom(this.Repository.All<Order>());

            var command = new CancelWorkCommand
            {
                UserName = this.Faker.Person.UserName,
                Id = order.Id,
                Comment = this.Faker.Rant.Review()
            };

            return command;
        }
    }
}