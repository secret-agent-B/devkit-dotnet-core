// -----------------------------------------------------------------------
// <copyright file="GetProductsHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Products.Queries.GetProducts
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// Handler for GetProductsQuery.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{GetProductsQuery, ResponseSet{ProductVM}}" />
    public class GetProductsHandler : QueryHandlerBase<GetProductsQuery, ResponseSet<ProductVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductsHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetProductsHandler(IRepository repository) : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken) =>
            Task.Run(() =>
            {
                var products = this.Repository
                    .GetMany<Product>(x
                        => (x.ActiveDate <= DateTime.UtcNow && x.InactiveDate > DateTime.UtcNow));

                this.Response.Items.AddRange(products.Select(x => new ProductVM(x)));
            }, cancellationToken);
    }
}