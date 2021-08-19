// -----------------------------------------------------------------------
// <copyright file="FakeGetUserConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Security.Fakes.Consumers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.ServiceBus.Test;
    using MassTransit;

    /// <summary>
    /// The GetUserConsumer.
    /// </summary>
    /// <seealso cref="FakeMessageConsumerBase{IGetUser}" />
    public class FakeGetUserConsumer : FakeMessageConsumerBase<IGetUser>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Parameter provided by MassTransit.")]
        protected async override Task ConsumeRequest(ConsumeContext<IGetUser> context)
        {
            await context.RespondAsync<IUserDTO>(new
            {
                this.Faker.Person.FirstName,
                this.Faker.Person.LastName,
                PhoneNumber = this.Faker.Phone.PhoneNumber()
            });
        }
    }
}