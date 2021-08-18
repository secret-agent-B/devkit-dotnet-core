// -----------------------------------------------------------------------
// <copyright file="StoreBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.ServiceBus
{
    using Logistics.Store.API.ServiceBus.Consumers;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The store bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class StoreBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<UserCreatedConsumer>();
            configurator.AddConsumer<GetAccountConsumer>();
        }
    }
}