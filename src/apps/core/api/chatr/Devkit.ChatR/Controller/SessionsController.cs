// -----------------------------------------------------------------------
// <copyright file="SessionsController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Controller
{
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.Sessions.Queries.GetSession;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The chat sessions controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class SessionsController : DevkitControllerBase
    {
        /// <summary>
        /// Gets my deliveries.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns>
        /// A list of deliveries.
        /// </returns>
        [HttpGet("{sessionId}")]
        public async Task<SessionVM> GetSession([FromRoute] string sessionId) =>
            await this.Mediator.Send(new GetSessionQuery
            {
                SessionId = sessionId
            });
    }
}