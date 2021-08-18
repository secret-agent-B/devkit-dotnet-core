// -----------------------------------------------------------------------
// <copyright file="TransactionsController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.WebAPI;
    using Logistics.Store.API.Business.Accounts.Queries;
    using Logistics.Store.API.Business.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The accounts controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class AccountsController : DevkitControllerBase
    {
        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An account view model.
        /// </returns>
        [HttpGet("{userName}")]
        public async Task<AccountVM> GetAccount([FromRoute] GetAccountQuery request) => await this.Mediator.Send(request);
    }
}