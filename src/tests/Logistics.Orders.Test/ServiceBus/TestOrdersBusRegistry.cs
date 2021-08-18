// -----------------------------------------------------------------------
// <copyright file="TestOrdersBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.ServiceBus
{
    using Devkit.Communication.FileStore.Fakes.Consumers;
    using Devkit.Communication.Security.Fakes.Consumers;
    using Devkit.ServiceBus.Interfaces;
    using Logistics.Communication.Store.Fakes.Consumers;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The OrdersBusRegistry registers test consumers and request clients for integration test.
    /// </summary>
    public class TestOrdersBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<FakeGetUserConsumer>();
            configurator.AddConsumer<FakeGetUsersConsumer>();
            configurator.AddConsumer<FakeGetAccountConsumer>();
            configurator.AddConsumer<FakeUploadBase64ImageConsumer>();
        }
    }
}