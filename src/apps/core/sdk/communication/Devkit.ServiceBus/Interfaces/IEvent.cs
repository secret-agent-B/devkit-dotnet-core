// -----------------------------------------------------------------------
// <copyright file="IMessage.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Interfaces
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// IEvent represents a message that can be published through the IBus interface and does not return a value.
    /// </summary>
    [SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Marker interface.")]
    public interface IEvent
    {
    }
}