// -----------------------------------------------------------------------
// <copyright file="IGetCredits.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Store.Messages
{
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// IGetCredits interface is the contract for querying an accounts credits.
    /// </summary>
    public interface IGetAccount : IRequest
    {
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; }
    }
}