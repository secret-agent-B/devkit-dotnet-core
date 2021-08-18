// -----------------------------------------------------------------------
// <copyright file="IBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The IServiceBusConfiguration configures message consumers for MassTransit.
    /// </summary>
    public interface IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        void RegisterConsumers(IServiceCollectionBusConfigurator configurator);
    }
}