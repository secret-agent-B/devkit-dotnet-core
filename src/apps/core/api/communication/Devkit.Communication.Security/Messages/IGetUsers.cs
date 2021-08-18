// -----------------------------------------------------------------------
// <copyright file="IGetUsers.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Security.Messages
{
    using System.Collections.Generic;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The IGetUsers is a request that pulls 2 or more user information.
    /// </summary>
    public interface IGetUsers : IRequest
    {
        /// <summary>
        /// Gets the user names.
        /// </summary>
        /// <value>
        /// The user names.
        /// </value>
        IList<string> UserNames { get; }
    }
}