// -----------------------------------------------------------------------
// <copyright file="ChatRRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.ServiceBus
{
    using Devkit.ChatR.ServiceBus.Consumers;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;

    /// <summary>
    /// The ChatR bus registry.
    /// </summary>
    public class ChatRRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<StartSessionConsumer>();
        }
    }
}