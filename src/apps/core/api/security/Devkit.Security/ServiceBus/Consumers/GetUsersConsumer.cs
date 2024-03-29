﻿// -----------------------------------------------------------------------
// <copyright file="GetUsersConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.ServiceBus.Consumers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.Security.Data.Models;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Interfaces;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// The GetUsersConsumer consumes the message IGetUser and returns multiple instances of user information.
    /// </summary>
    /// <seealso cref="MessageConsumerBase{IGetUser}" />
    public class GetUsersConsumer : MessageConsumerBase<IGetUsers>
    {
        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<UserAccount> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUsersConsumer" /> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        public GetUsersConsumer(UserManager<UserAccount> userManager)
        {
            this._userManager = userManager;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ConsumeRequest(ConsumeContext<IGetUsers> context)
        {
            var userAccounts = new List<UserAccount>();

            foreach (var userName in context.Message.UserNames)
            {
                if (string.IsNullOrEmpty(userName))
                {
                    continue;
                }

                var userAccount = await this._userManager.FindByNameAsync(userName);

                if (userAccount != null)
                {
                    userAccounts.Add(userAccount);
                }
            }

            await context.RespondAsync<IListResponse<IUserDTO>>(new
            {
                Items = userAccounts.Select(x => new
                {
                    x.Profile.FirstName,
                    x.Profile.LastName,
                    x.UserName,
                    x.PhoneNumber
                }).ToList()
            });
        }
    }
}