// -----------------------------------------------------------------------
// <copyright file="IMessage.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The IRequest represents a message that can be invoked using a RequestClient and returns a response.
    /// The response can be an object if successful or an IMessageBusException if the request failed.
    /// </summary>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker interface.")]
    public interface IRequest
    {
    }
}