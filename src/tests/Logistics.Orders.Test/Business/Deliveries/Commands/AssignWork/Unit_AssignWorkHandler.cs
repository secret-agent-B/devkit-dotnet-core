// -----------------------------------------------------------------------
// <copyright file="Unit_AssignWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.AssignWork
{
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Deliveries.Commands.AssignWork;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The Unit_AssignWorkHandler is the unit test for AssignWorkHandler.
    /// </summary>
    public class Unit_AssignWorkHandler : OrdersUnitTestBase<(AssignWorkCommand command, AssignWorkHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_AssignWorkHandler"/> class.
        /// </summary>
        public Unit_AssignWorkHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to assign a driver to an order.
        /// </summary>
        [Fact(DisplayName = "Should be able to assign a driver to an order")]
        public async Task Should_be_able_to_assign_a_driver_to_an_order()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(response.DriverUserName, command.UserName);
        }

        /// <summary>
        /// Should not be able to assign driver if oder has already been assigned to a driver.
        /// </summary>
        [Fact(DisplayName = "Should not be able to assign driver if oder has already been assigned to a driver")]
        public async Task Should_not_be_able_to_assign_driver_if_oder_has_already_been_assigned_to_a_driver()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(response.DriverUserName, command.UserName);

            var reassignCommand = new AssignWorkCommand
            {
                UserName = this.Faker.Person.UserName,
                Id = command.Id
            };
            var reassignResponse = await handler.Handle(reassignCommand, CancellationToken.None);

            Assert.False(reassignResponse.IsSuccessful);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (AssignWorkCommand command, AssignWorkHandler handler) Build()
        {
            var order = this.Faker.PickRandom(this.Repository.All<Order>());
            var command = new AssignWorkCommand
            {
                UserName = this.Faker.Person.UserName,
                Id = order.Id
            };

            return (command, new AssignWorkHandler(this.Repository, this.TestHarness.Bus));
        }
    }
}