// -----------------------------------------------------------------------
// <copyright file="ISessionDTO.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.ChatR.DTOs
{
    using System.Collections.Generic;

    /// <summary>
    /// A chat session.
    /// </summary>
    public interface ISessionDTO
    {
        /// <summary>
        /// Gets the subject identifier.
        /// </summary>
        /// <value>
        /// The topic identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        IList<IMessageDTO> Messages { get; }

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