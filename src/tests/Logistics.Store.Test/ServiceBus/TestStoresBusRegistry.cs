// -----------------------------------------------------------------------
// <copyright file="TestStoresBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.ServiceBus
{
    using Devkit.ServiceBus.Interfaces;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The test Stores API bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class TestStoresBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
        }
    }
}