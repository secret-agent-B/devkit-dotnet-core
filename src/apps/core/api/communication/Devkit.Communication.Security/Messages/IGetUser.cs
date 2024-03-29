﻿// -----------------------------------------------------------------------
// <copyright file="IGetUser.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Security.Messages
{
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The IGetUser is the message for getting user information.
    /// </summary>
    public interface IGetUser : IRequest
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }
    }
}