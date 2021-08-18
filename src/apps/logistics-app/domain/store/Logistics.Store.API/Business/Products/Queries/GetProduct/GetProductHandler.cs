// -----------------------------------------------------------------------
// <copyright file="GetProductHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Products.Queries.GetProduct
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Patterns.Exceptions;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// GetProductHandler class is the handler for GetProductQuery.
    /// </summary>
    public class GetProductHandler : QueryHandlerBase<GetProductQuery, ProductVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetProductHandler(IRepository repository)
            : base(repository)
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
                var product = this.Repository.GetOneOrDefault<Product>(x => x.Id == this.Request.Id);

                if (product == null)
                {
                    throw new NotFoundException(nameof(Product), this.Request.Id);
                }

                this.Response = new ProductVM(product);
            }, cancellationToken);
    }
}