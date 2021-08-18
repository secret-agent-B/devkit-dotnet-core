// -----------------------------------------------------------------------
// <copyright file="StartSessionConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.ServiceBus.Consumers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.ChatR.Configurations;
    using Devkit.Communication.ChatR.Messages;
    using Devkit.ServiceBus;
    using MassTransit;
    using Microsoft.Extensions.Options;
    using MongoDB.Bson;
    using StackExchange.Redis.Extensions.Core.Abstractions;

    /// <summary>
    /// Start session consumer.
    /// </summary>
    /// <seealso cref="MessageConsumerBase{IStartSession}" />
    public class StartSessionConsumer : MessageConsumerBase<IStartSession>
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
        /// Initializes a new instance of the <see cref="StartSessionConsumer" /> class.
        /// </summary>
        /// <param name="cacheClient">The cache client.</param>
        /// <param name="options">The options.</param>
        public StartSessionConsumer(IRedisCacheClient cacheClient, IOptions<ChatRConfiguration> options)
        {
            this._cacheClient = cacheClient;
            this._configuration = options.Value;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ConsumeRequest(ConsumeContext<IStartSession> context)
        {
            var sessionExist = await this._cacheClient.Db0.ExistsAsync(context.Message.Id);

            if (sessionExist)
            {
                // Session already exist no need to create a new one.
                return;
            }

            var session = new SessionVM
            {
                Messages = new List<MessageVM>
                {
                    new MessageVM
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        AuthorUserName = this._configuration.SystemName,
                        IsDeleted = false,
                        Message = this._configuration.WelcomeMessage,
                        Timestamp = DateTime.UtcNow
                    }
                },
                Participants = context.Message.Participants
                    .Select(x => new ParticipantVM
                    {
                        UserName = x.UserName,
                        Role = x.Role,
                        PhoneNumber = x.PhoneNumber
                    }).ToList(),
                Id = context.Message.Id,
                Topic = context.Message.Topic
            };

            // Add system as one of the participants.
            session.Participants.Add(new ParticipantVM
            {
                UserName = this._configuration.SystemName,
                Role = this._configuration.SystemRole,
                PhoneNumber = this._configuration.SupportPhoneNumber
            });

            await this._cacheClient.Db0.AddAsync(context.Message.Id, session);
        }
    }
}