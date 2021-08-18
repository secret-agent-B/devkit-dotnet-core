// -----------------------------------------------------------------------
// <copyright file="Intg_AssignWork.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.AssignWork
{
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Commands.AssignWork;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The Intg_AssignWork is the integration test for assigning work to a driver.
    /// </summary>
    public class Intg_AssignWork : OrdersIntegrationTestBase<AssignWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_AssignWork"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_AssignWork(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to assign work to a driver.
        /// </summary>
        [Fact(DisplayName = "Should be able to assign work to a driver")]
        public async Task Should_be_able_to_assign_work_to_a_driver()
        {
            var command = this.Build();
            var response = await this.PatchAsync<OrderVM>("/deliveries/assign/", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.NotEmpty(response.Payload.Statuses.Where(x => x.Code == StatusCode.Assigned.DisplayName && x.UserName == command.UserName));
        }

        /// <summary>
        /// Should not be able to assign work if status is invalid.
        /// </summary>
        [Fact(DisplayName = "Should not be able to assign work if status is invalid")]
        public async Task Should_not_be_able_to_assign_work_if_status_is_invalid()
        {
            var command = this.Build();
            var assignResponse = await this.PatchAsync<OrderVM>("/deliveries/assign/", command);

            Assert.True(assignResponse.IsSuccessfulStatusCode);
            Assert.NotEmpty(assignResponse.Payload.Statuses.Where(x => x.Code == StatusCode.Assigned.DisplayName && x.UserName == command.UserName));

            var reassignResponse = await this.PatchAsync<OrderVM>("/deliveries/assign/", command);

            Assert.False(reassignResponse.IsSuccessfulStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, reassignResponse.StatusCode);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override AssignWorkCommand Build()
        {
            var order = this.Faker.PickRandom(this.Repository.All<Order>());
            var command = new AssignWorkCommand
            {
                UserName = this.Faker.Person.UserName,
                Id = order.Id
            };

            return command;
        }
    }
}