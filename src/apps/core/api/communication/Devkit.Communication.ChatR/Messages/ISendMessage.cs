// -----------------------------------------------------------------------
// <copyright file="ISendMessage.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.ChatR.Messages
{
    using System;

    /// <summary>
    ///     Command for sending a message to the chat app.
    /// </summary>
    public interface ISendMessage
    {
        /// <summary>
        ///     Gets a value indicating whether this instance is system.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is system; otherwise, <c>false</c>.
        /// </value>
        bool IsSystem { get; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }

        /// <summary>
        ///     Gets the timestamp.
        /// </summary>
        /// <value>
        ///     The timestamp.
        /// </value>
        DateTime Timestamp { get; }

        /// <summary>
        ///     Gets the user name.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        string UserName { get; }
    }
}