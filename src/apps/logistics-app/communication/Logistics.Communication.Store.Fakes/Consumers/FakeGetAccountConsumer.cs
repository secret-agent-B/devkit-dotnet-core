// -----------------------------------------------------------------------
// <copyright file="FakeGetAccountConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Store.Fakes.Consumers
{
    using System.Threading.Tasks;
    using Devkit.ServiceBus.Test;
    using Logistics.Communication.Store.DTOs;
    using Logistics.Communication.Store.Messages;
    using MassTransit;

    /// <summary>
    /// Fake consumer for IGetAccount message.
    /// </summary>
    /// <seealso cref="FakeMessageConsumerBase{IGetAccount}" />
    public class FakeGetAccountConsumer : FakeMessageConsumerBase<IGetAccount>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ConsumeRequest(ConsumeContext<IGetAccount> context)
        {
            await context.RespondAsync<IAccountDTO>(new
            {
                AvailableCredits = this.Faker.Random.Double(1000, 2000),
                Status = "Active",
                UserName = context.Message.UserName
            });
        }
    }
}