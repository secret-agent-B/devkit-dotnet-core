// -----------------------------------------------------------------------
// <copyright file="Intg_GetProduct.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Products.Queries.GetProduct
{
    using System.Net;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Store.API;
    using Logistics.Store.API.Business.Products.Queries.GetProduct;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Intg_GetProduct class is the integration test base for GetProduct.
    /// </summary>
    public class Intg_GetProduct : StoresIntegrationTestBase<GetProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetProduct"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetProduct(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to fetch a product.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Fact(DisplayName = "Should be able to fetch a product")]
        public async Task Should_be_able_to_fetch_a_product()
        {
            var query = this.Build();
            var response = await this.GetAsync<ProductVM>($"/products/{query.Id}");

            var product = this.Repository.GetOneOrDefault<Product>(x => x.Id == query.Id);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(product.Id, response.Payload.Id);
            Assert.Equal(product.Code, response.Payload.Code);
            Assert.Equal(product.Description, response.Payload.Description);
            Assert.Equal(product.Highlights, response.Payload.Highlights);
            Assert.Equal(product.Name, response.Payload.Name);
            Assert.Equal(product.PricePerUnit, response.Payload.PricePerUnit);
        }

        /// <summary>
        /// Should return not found exception.
        /// </summary>
        [Fact(DisplayName = "Should return not found exception")]
        public async Task Should_return_not_found_exception()
        {
            var response = await this.GetAsync<ProductVM>($"/products/{this.Faker.Random.Hexadecimal(24, string.Empty)}");

            Assert.False(response.IsSuccessfulStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override GetProductQuery Build()
        {
            var products = this.Repository.All<Product>();
            var product = this.Faker.PickRandom(products);

            return new GetProductQuery { Id = product.Id };
        }
    }
}