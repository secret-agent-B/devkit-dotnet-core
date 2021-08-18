// -----------------------------------------------------------------------
// <copyright file="GetProductQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Products.Queries.GetProduct
{
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Store.API.Business.ViewModels;

    /// <summary>
    /// Query that returns a single product.
    /// </summary>
    public class GetProductQuery : QueryRequestBase<ProductVM>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}