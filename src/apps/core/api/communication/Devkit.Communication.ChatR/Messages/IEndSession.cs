// -----------------------------------------------------------------------
// <copyright file="IEndSession.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.ChatR.Messages
{
    /// <summary>
    /// End chat session.
    /// </summary>
    public interface IEndSession
    {
        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        string Key { get; }
    }
}