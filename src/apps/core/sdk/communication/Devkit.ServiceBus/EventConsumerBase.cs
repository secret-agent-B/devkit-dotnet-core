// -----------------------------------------------------------------------
// <copyright file="EventConsumerBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus
{
    using System;
    using System.Threading.Tasks;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using Serilog;

    /// <summary>
    /// The event consumer base class.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <seealso cref="IMessageConsumer{TEvent}" />
    public abstract class EventConsumerBase<TEvent> : IEventConsumer<TEvent>
        where TEvent : class, IEvent
    {
        /// <summary>
        /// Consumes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task Consume(ConsumeContext<TEvent> context)
        {
            try
            {
                await this.ConsumeEvent(context);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected abstract Task ConsumeEvent(ConsumeContext<TEvent> context);
    }
}