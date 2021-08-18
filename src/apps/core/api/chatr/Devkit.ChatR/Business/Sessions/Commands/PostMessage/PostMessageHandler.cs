// -----------------------------------------------------------------------
// <copyright file="PostMessageHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.Sessions.Commands.PostMessage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.ChatR.Configurations;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using StackExchange.Redis.Extensions.Core.Abstractions;

    /// <summary>
    /// The handler for PostMessageCommand.
    /// </summary>
    public class PostMessageHandler : CommandHandlerBase<PostMessageCommand, MessageVM>
    {
        /// <summary>
        /// The cache client.
        /// </summary>
        private readonly IRedisCacheClient _cacheClient;

        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly ChatRConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostMessageHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="cacheClient">The cache client.</param>
        /// <param name="options">The options.</param>
        public PostMessageHandler(IRepository repository, IRedisCacheClient cacheClient, IOptions<ChatRConfiguration> options)
            : base(repository)
        {
            this._cacheClient = cacheClient;
            this._configuration = options.Value;
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
            // Check if session id is valid.
            var sessionExist = await this._cacheClient.Db0.ExistsAsync(this.Request.SessionId);

            if (!sessionExist)
            {
                this.Response.Exceptions.Add(
                    nameof(this.Request.SessionId),
                    new List<string> { $"{this._configuration.SystemName} session not found ({this.Request.SessionId})." });

                return;
            }

            // Check if participant is valid.

            var session = await this._cacheClient.Db0.GetAsync<SessionVM>(this.Request.SessionId);
            var isParticipant = session.Participants.Any(x => x.UserName == this.Request.UserName);

            if (!isParticipant)
            {
                this.Response.Exceptions.Add(
                    nameof(this.Request.UserName),
                    new List<string> { $"Invalid session participant ({this.Request.UserName})." });

                return;
            }

            // Add message to the session.

            var newMessage = new MessageVM
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Message = this.Request.Message,
                Timestamp = DateTime.UtcNow,
                AuthorUserName = this.Request.UserName,
                IsDeleted = false
            };

            session.Messages.Add(newMessage);

            this.Response = newMessage;

            await this._cacheClient.Db0.ReplaceAsync(this.Request.SessionId, session);
        }
    }
}