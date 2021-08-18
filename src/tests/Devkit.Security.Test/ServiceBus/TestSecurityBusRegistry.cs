// -----------------------------------------------------------------------
// <copyright file="TestSecurityBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.ServiceBus
{
    using Devkit.Communication.FileStore.Fakes.Consumers;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// The test security bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class TestSecurityBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<FakeUploadBase64ImageConsumer>();
        }
    }
}