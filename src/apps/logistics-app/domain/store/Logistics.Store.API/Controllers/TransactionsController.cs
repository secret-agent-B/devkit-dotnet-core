// -----------------------------------------------------------------------
// <copyright file="TransactionsController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.WebAPI;
    using Logistics.Store.API.Business.Transactions.Commands.CreateTransaction;
    using Logistics.Store.API.Business.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The transactions controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class TransactionsController : DevkitControllerBase
    {
        /// <summary>
        /// Creates the transaction that is used for generating an invoice for payment vendors.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A transaction view model.</returns>
        [HttpPost("")]
        public async Task<TransactionVM> CreateTransaction([FromBody] CreateTransactionCommand request) => await this.Mediator.Send(request);
    }
}