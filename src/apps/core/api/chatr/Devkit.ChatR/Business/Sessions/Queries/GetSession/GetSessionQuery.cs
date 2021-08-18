// -----------------------------------------------------------------------
// <copyright file="GetSessionQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.Sessions.Queries.GetSession
{
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.Patterns.CQRS.Query;

    /// <summary>
    /// Gets a chat session.
    /// </summary>
    /// <seealso cref="QueryRequestBase{SessionVM}" />
    public class GetSessionQuery : QueryRequestBase<SessionVM>
    {
        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        public string SessionId { get; set; }
    }
}