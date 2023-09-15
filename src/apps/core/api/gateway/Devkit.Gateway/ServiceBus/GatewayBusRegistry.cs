// -----------------------------------------------------------------------
// <copyright file="OrderBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway.ServiceBus
{
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;

    /// <summary>
    /// THe OrderBusRegistry registers the consumers and request clients for the Orders API.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class GatewayBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumersFromNamespaceContaining<GatewayBusRegistry>();
        }
    }
}