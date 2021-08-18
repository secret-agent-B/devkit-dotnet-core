// -----------------------------------------------------------------------
// <copyright file="UserCreatedConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.ServiceBus.Consumers
{
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages.Events;
    using Devkit.Ratings.Business.Users.Commands.CreateUser;
    using Devkit.ServiceBus;
    using MassTransit;
    using MediatR;

    /// <summary>
    /// Event handler for when a user gets registered.
    /// </summary>
    /// <seealso cref="EventConsumerBase{IUserCreated}" />
    public class UserCreatedConsumer : EventConsumerBase<IUserCreated>
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly Mediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCreatedConsumer" /> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public UserCreatedConsumer(Mediator mediator)
        {
            this._mediator = mediator;
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
            _ = this._mediator.Send(new CreateUserCommand
            {
                UserName = context.Message.UserName
            });

            return Task.CompletedTask;
        }
    }
}