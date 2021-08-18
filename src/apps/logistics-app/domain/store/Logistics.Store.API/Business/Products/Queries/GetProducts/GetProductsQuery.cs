// -----------------------------------------------------------------------
// <copyright file="GetProductsQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Products.Queries.GetProducts
{
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Store.API.Business.ViewModels;

    /// <summary>
    /// Query that returns all active products.
    /// </summary>
    public class GetProductsQuery : QueryRequestBase<ResponseSet<ProductVM>>
    {
    }
}