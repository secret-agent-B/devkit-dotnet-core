// -----------------------------------------------------------------------
// <copyright file="OrdersIntegrationTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test
{
    using Devkit.ServiceBus.Interfaces;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.Test.Fakers;
    using Logistics.Orders.Test.ServiceBus;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// The Orders integration test base class.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request.</typeparam>
    /// <seealso cref="IntegrationTestBase{TRequest, Startup}" />
    public abstract class OrdersIntegrationTestBase<TRequest> : IntegrationTestBase<TRequest, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersIntegrationTestBase{TRequest}"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        protected OrdersIntegrationTestBase(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
            testFixture.ConfigureTestServices(services =>
            {
                services.AddSingleton<IBusRegistry, TestOrdersBusRegistry>();
                services.AddSingleton(new DeliveryOptionsFaker().Generate());
            });
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            this.Repository.AddRangeWithAudit(new OrderFaker().Generate(10));
        }
    }
}