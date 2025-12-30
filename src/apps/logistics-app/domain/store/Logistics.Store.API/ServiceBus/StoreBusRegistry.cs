// -----------------------------------------------------------------------
// <copyright file="StoreBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.ServiceBus
{
    using Devkit.ServiceBus.Interfaces;
    using Logistics.Store.API.ServiceBus.Consumers;
    using MassTransit;

    /// <summary>
    /// The store bus registry.
    /// </summary>
    /// <seealso cref="IBusRegistry" />
    public class StoreBusRegistry : IBusRegistry
    {
        public void RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<UserCreatedConsumer>();
            configurator.AddConsumer<GetAccountConsumer>();
        }
    }
}