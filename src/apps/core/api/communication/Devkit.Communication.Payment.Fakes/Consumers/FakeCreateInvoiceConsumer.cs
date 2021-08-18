// -----------------------------------------------------------------------
// <copyright file="FakeCreateInvoiceConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.Payment.Fakes.Consumers
{
    using System.Threading.Tasks;
    using Devkit.Communication.Payment.DTOs;
    using Devkit.Communication.Payment.Messages;
    using Devkit.ServiceBus.Test;
    using MassTransit;

    /// <summary>
    /// The FakeCreateInvoiceConsumer consumes the ICreateInvoice request.
    /// </summary>
    public class FakeCreateInvoiceConsumer : FakeMessageConsumerBase<ICreateInvoice>
    {
        /// <summary>
        /// Consumes the specified message.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ConsumeRequest(ConsumeContext<ICreateInvoice> context)
        {
            await context.RespondAsync<IInvoiceDTO>(new
            {
                Amount = context.Message.Amount,
                TransactionId = context.Message.TransactionId,
                InvoiceId = this.Faker.Random.Hexadecimal(24, string.Empty),
                Status = "PENDING"
            });
        }
    }
}