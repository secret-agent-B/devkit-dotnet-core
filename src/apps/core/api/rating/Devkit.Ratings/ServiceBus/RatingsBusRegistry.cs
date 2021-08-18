// -----------------------------------------------------------------------
// <copyright file="FileStoreRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.ServiceBus
{
    using Devkit.ServiceBus.Interfaces;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The FileStore API service bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class RatingsBusRegistry : IBusRegistry
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