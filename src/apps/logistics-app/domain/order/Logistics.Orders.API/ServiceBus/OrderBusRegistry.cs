// -----------------------------------------------------------------------
// <copyright file="OrderBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.ServiceBus
{
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// THe OrderBusRegistry registers the consumers and request clients for the Orders API.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class OrderBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumersFromNamespaceContaining<OrderBusRegistry>();
        }
    }
}