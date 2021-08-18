// -----------------------------------------------------------------------
// <copyright file="IStartSession.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.ChatR.Messages
{
    using System.Collections.Generic;
    using Devkit.Communication.ChatR.DTOs;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// Start a new chat session.
    /// </summary>
    public interface IStartSession : IRequest
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the participants.
        /// </summary>
        /// <value>
        /// The participant users.
        /// </value>
        IList<IParticipantDTO> Participants { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Topic { get; }
    }
}