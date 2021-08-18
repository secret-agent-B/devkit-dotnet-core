// -----------------------------------------------------------------------
// <copyright file="Unit_GetProductsHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Products.Queries.GetProducts
{
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Store.API.Business.Products.Queries.GetProducts;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Unit_GetProductsHandler class is the unit test for the GetProductsHandler.
    /// </summary>
    /// <seealso cref="StoresUnitTestBase{(GetProductsQuery query, GetProductsHandler handler)}" />
    public class Unit_GetProductsHandler : StoresUnitTestBase<(GetProductsQuery query, GetProductsHandler handler)>
    {
        /// <summary>
        /// Should be able to get products.
        /// </summary>
        [Fact(DisplayName = "Should be able to get products")]
        public async Task Should_be_able_to_get_products()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.NotEmpty(response.Items);

            foreach (var item in response.Items)
            {
                var product = this.Repository.GetOneOrDefault<Product>(x => x.Id == item.Id);

                Assert.Equal(product.Id, item.Id);
                Assert.Equal(product.Code, item.Code);
                Assert.Equal(product.Description, item.Description);
                Assert.Equal(product.Highlights, item.Highlights);
                Assert.Equal(product.Name, item.Name);
                Assert.Equal(product.PricePerUnit, item.PricePerUnit);
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetProductsQuery query, GetProductsHandler handler) Build()
        {
            return (new GetProductsQuery(), new GetProductsHandler(this.Repository));
        }
    }
}