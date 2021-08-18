// -----------------------------------------------------------------------
// <copyright file="FakeGetUsersConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Security.Fakes.Consumers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.DTOs;
    using Devkit.Communication.Security.Messages;
    using Devkit.ServiceBus.Interfaces;
    using Devkit.ServiceBus.Test;
    using MassTransit;

    /// <summary>
    /// The TestGetUsersConsumer is a test consumer for the IGetUsers message.
    /// </summary>
    public class FakeGetUsersConsumer : FakeMessageConsumerBase<IGetUsers>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ConsumeRequest(ConsumeContext<IGetUsers> context)
        {
            await context.RespondAsync<IListResponse<IUserDTO>>(new
            {
                Items = context.Message.UserNames.Select(x => new
                {
                    this.Faker.Person.FirstName,
                    this.Faker.Person.LastName,
                    UserName = x,
                    PhoneNumber = this.Faker.Person.Phone
                }).ToList()
            });
        }
    }
}