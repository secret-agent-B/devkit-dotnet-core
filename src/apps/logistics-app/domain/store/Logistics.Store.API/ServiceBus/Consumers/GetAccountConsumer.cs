// -----------------------------------------------------------------------
// <copyright file="GetAccountConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.ServiceBus.Consumers
{
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Exceptions;
    using Logistics.Communication.Store.DTOs;
    using Logistics.Communication.Store.Messages;
    using Logistics.Store.API.Data.Models;
    using MassTransit;

    /// <summary>
    /// Consumer for IGetCredits query.
    /// </summary>
    /// <seealso cref="MessageConsumerBase{IGetCredits}" />
    public class GetAccountConsumer : MessageConsumerBase<IGetAccount>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountConsumer" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetAccountConsumer(IRepository repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ConsumeRequest(ConsumeContext<IGetAccount> context)
        {
            var account = this._repository.GetOneOrDefault<Account>(x => x.UserName == context.Message.UserName);

            if (account == null)
            {
                await context.RespondAsync<IConsumerException>(new
                {
                    ErrorMessage = $"User account ({context.Message.UserName}) not found."
                });
            }
            else
            {
                await context.RespondAsync<IAccountDTO>(new
                {
                    account.AvailableCredits,
                    Status = account.Status.ToString(),
                    account.UserName
                });
            }
        }
    }
}