// -----------------------------------------------------------------------
// <copyright file="IEventConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using MassTransit;

    /// <summary>
    /// A contract for event consumers.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <seealso cref="IConsumer{TEvent}" />
    public interface IEventConsumer<in TEvent> : IConsumer<TEvent>
        where TEvent : class, IEvent
    {
    }
}