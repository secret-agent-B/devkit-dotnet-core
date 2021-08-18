// -----------------------------------------------------------------------
// <copyright file="GetSessionHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.Sessions.Queries.GetSession
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using StackExchange.Redis.Extensions.Core.Abstractions;

    /// <summary>
    /// The handler for getting the chat session.
    /// </summary>
    public class GetSessionHandler : QueryHandlerBase<GetSessionQuery, SessionVM>
    {
        /// <summary>
        /// The cache client.
        /// </summary>
        private readonly IRedisCacheClient _cacheClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cacheClient">The cache client.</param>
        public GetSessionHandler(IRepository repository, IRedisCacheClient cacheClient)
            : base(repository)
        {
            this._cacheClient = cacheClient;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var sessionExist = await this._cacheClient.Db0.ExistsAsync(this.Request.SessionId);

            if (!sessionExist)
            {
                this.Response.Exceptions.Add(
                    nameof(this.Request.SessionId),
                    new List<string> { $"ChatR session not found ({this.Request.SessionId})." });

                return;
            }

            this.Response = await this._cacheClient.Db0.GetAsync<SessionVM>(this.Request.SessionId);
        }
    }
}