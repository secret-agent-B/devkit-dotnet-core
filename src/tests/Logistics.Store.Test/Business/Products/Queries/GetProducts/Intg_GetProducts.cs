// -----------------------------------------------------------------------
// <copyright file="Intg_GetProducts.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Products.Queries.GetProducts
{
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Store.API;
    using Logistics.Store.API.Business.Products.Queries.GetProduct;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Intg_GetProducts class is the integration test for GetProducts.
    /// </summary>
    public class Intg_GetProducts : StoresIntegrationTestBase<GetProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetProducts"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetProducts(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get products.
        /// </summary>
        [Fact(DisplayName = "Should be able to get products")]
        public async Task Should_be_able_to_get_products()
        {
            var response = await this.GetAsync<ResponseSet<ProductVM>>("/products");

            Assert.NotEmpty(response.Payload.Items);

            foreach (var item in response.Payload.Items)
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
    }
}