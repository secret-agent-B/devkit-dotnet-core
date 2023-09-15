// -----------------------------------------------------------------------
// <copyright file="PayMayaBusRegistry.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.PayMaya.ServiceBus
{
    using Devkit.Payment.PayMaya.ServiceBus.Consumers.CreateInvoice;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;

    /// <summary>
    /// The PayMayaBusRegistry handles registration of consumers and request clients for PayMaya service.
    /// </summary>
    public class PayMayaBusRegistry : IBusRegistry
    {
        /// <summary>
        /// Configure message consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        public void RegisterConsumers(IBusRegistrationConfigurator configurator)
        {
            configurator.AddConsumer<CreateInvoiceConsumer>();
        }
    }
}