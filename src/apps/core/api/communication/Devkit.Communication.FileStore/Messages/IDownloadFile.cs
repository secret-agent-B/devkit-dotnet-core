// -----------------------------------------------------------------------
// <copyright file="DownloadFileConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Messages
{
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// A request that is sent to the bus to download a file.
    /// </summary>
    public interface IDownloadFile : IRequest
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }
    }
}