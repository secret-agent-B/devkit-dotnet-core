// -----------------------------------------------------------------------
// <copyright file="SessionVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.ViewModels
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS;

    /// <summary>
    /// The session view model.
    /// </summary>
    public class SessionVM : ResponseBase
    {
        /// <summary>
        /// Gets or sets the subject identifier.
        /// </summary>
        /// <value>
        /// The topic identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        /// <value>
        /// The messages.
        /// </value>
        public IList<MessageVM> Messages { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        /// <value>
        /// The participant users.
        /// </value>
        public IList<ParticipantVM> Participants { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Topic { get; set; }
    }
}