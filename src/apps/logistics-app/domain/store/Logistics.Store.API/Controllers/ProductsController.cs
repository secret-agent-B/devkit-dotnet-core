// -----------------------------------------------------------------------
// <copyright file="TransactionsController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.WebAPI;
    using Logistics.Store.API.Business.Products.Queries.GetProduct;
    using Logistics.Store.API.Business.Products.Queries.GetProducts;
    using Logistics.Store.API.Business.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Products controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class ProductsController : DevkitControllerBase
    {
        /// <summary>
        /// Gets the Product.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An Product view model.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<ProductVM> GetProduct([FromRoute] GetProductQuery request) => await this.Mediator.Send(request);

        /// <summary>
        /// Gets the Product.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An Product view model.
        /// </returns>
        [HttpGet("")]
        public async Task<ResponseSet<ProductVM>> GetProduct(GetProductsQuery request) => await this.Mediator.Send(request);
    }
}