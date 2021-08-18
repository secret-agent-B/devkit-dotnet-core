// -----------------------------------------------------------------------
// <copyright file="UserCreatedConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.ServiceBus.Consumers
{
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages.Events;
    using Devkit.Data.Interfaces;
    using Devkit.ServiceBus;
    using Logistics.Store.API.Constant;
    using Logistics.Store.API.Data.Models;
    using MassTransit;

    /// <summary>
    /// User created event consumer.
    /// </summary>
    /// <seealso cref="Devkit.ServiceBus.EventConsumerBase{Devkit.Communication.Security.Messages.Events.IUserCreated}" />
    public class UserCreatedConsumer : EventConsumerBase<IUserCreated>
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedConsumer"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UserCreatedConsumer(IRepository repository)
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
        protected override Task ConsumeEvent(ConsumeContext<IUserCreated> context)
        {
            var account = this._repository.GetOneOrDefault<Account>(x => x.UserName == context.Message.UserName);

            if (account == null)
            {
                this._repository.AddWithAudit(new Account
                {
                    Status = AccountStatuses.Active,
                    AvailableCredits = 50000.00,
                    UserName = context.Message.UserName
                });
            }

            return Task.CompletedTask;
        }
    }
}