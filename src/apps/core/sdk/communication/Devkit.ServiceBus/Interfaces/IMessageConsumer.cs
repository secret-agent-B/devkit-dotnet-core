// -----------------------------------------------------------------------
// <copyright file="IMessageConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using MassTransit;

    /// <summary>
    /// The IMessageConsumer.
    /// </summary>
    public interface IMessageConsumer<in TMessage> : IConsumer<TMessage>
        where TMessage : class, IRequest
    {
    }
}