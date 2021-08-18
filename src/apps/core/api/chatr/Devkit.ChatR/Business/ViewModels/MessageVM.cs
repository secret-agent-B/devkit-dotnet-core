// -----------------------------------------------------------------------
// <copyright file="MessageVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.ViewModels
{
    using System;
    using Devkit.Patterns.CQRS;

    /// <summary>
    /// The message view model.
    /// </summary>
    public class MessageVM : ResponseBase
    {
        /// <summary>
        /// Gets or sets the name of the author user.
        /// </summary>
        /// <value>
        /// The name of the author user.
        /// </value>
        public string AuthorUserName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }
    }
}