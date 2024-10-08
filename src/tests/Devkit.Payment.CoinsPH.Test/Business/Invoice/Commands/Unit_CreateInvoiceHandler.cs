﻿// -----------------------------------------------------------------------
// <copyright file="Unit_CreateInvoiceHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Test.Business.Invoice.Commands
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;
    using Devkit.Payment.CoinsPH.Configurations;
    using Devkit.Payment.CoinsPH.Test.Fakers;
    using Devkit.Test;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// The Unit_CreateInvoiceHandler is the unit test for CreateInvoiceHandler.
    /// </summary>
    public class Unit_CreateInvoiceHandler : UnitTestBase<(CreateInvoiceCommand command, CreateInvoiceHandler handler)>
    {
        /// <summary>
        /// Should be able to create invoice.
        /// </summary>
        [TestCase(TestName = "Should be able to create invoice")]
        public async Task Should_be_able_to_create_invoice()
        {
            var (command, handler) = this.Build();

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.IsSuccessful);
            Assert.That(string.IsNullOrEmpty(response.InvoiceId), Is.False);
            Assert.Equals(command.TransactionId, response.TransactionId);
            Assert.Equals(command.Amount, response.Amount);
            Assert.Equals(command.Amount, response.AmountDue);
            Assert.Equals(command.Currency, response.Currency);
            Assert.That(response.PaymentUrl, Is.Not.Null);
            Assert.That(response.CreatedAt, Is.Not.Null);
            Assert.That(response.UpdatedAt, Is.Not.Null);
            Assert.That(response.ExpiresAt, Is.Not.Null);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "ONly used for unit tests.")]
        protected override (CreateInvoiceCommand command, CreateInvoiceHandler handler) Build()
        {
            const string baseAddress = "http://testing-coins-ph.com";
            const string invoiceCallbackUrl = "/coins-ph/callback";
            const string invoiceRoute = "/invoice";

            var mockHttpHandler = new Mock<HttpMessageHandler>(MockBehavior.Strict);

            var command = new CreateInvoiceCommandFaker().Generate();
            var invoice = new InvoiceHttpResponseFaker().Generate();

            invoice.ExternalTransaction = command.TransactionId;
            invoice.Amount = command.Amount;
            invoice.AmountDue = command.Amount;
            invoice.Currency = command.Currency;

            mockHttpHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(invoice))
                })
                .Verifiable();

            var httpClient = new HttpClient(mockHttpHandler.Object)
            {
                BaseAddress = new Uri(baseAddress)
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            var mockIOptions = new Mock<IOptions<CoinsPHConfiguration>>();

            mockIOptions
                .Setup(x => x.Value)
                .Returns(new CoinsPHConfiguration
                {
                    BaseAddress = baseAddress,
                    InvoiceCallbackUrl = invoiceCallbackUrl,
                    InvoiceRoute = invoiceRoute
                });

            var handler = new CreateInvoiceHandler(this.Repository, mockHttpClientFactory.Object, mockIOptions.Object);

            return (command, handler);
        }
    }
}