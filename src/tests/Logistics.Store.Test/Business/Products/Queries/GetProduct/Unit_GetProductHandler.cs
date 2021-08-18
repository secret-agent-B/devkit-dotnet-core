// -----------------------------------------------------------------------
// <copyright file="Unit_GetProductHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Products.Queries.GetProduct
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns.Exceptions;
    using Logistics.Store.API.Business.Products.Queries.GetProduct;
    using Logistics.Store.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Unit_GetProductHandler class is the unit test for the GetProductHandler.
    /// </summary>
    public class Unit_GetProductHandler : StoresUnitTestBase<(GetProductQuery query, GetProductHandler handler)>
    {
        /// <summary>
        /// Should be able to fetch a product.
        /// </summary>
        [Fact(DisplayName = "Should be able to fetch a product")]
        public async Task Should_be_able_to_fetch_a_product()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            var product = this.Repository.GetOneOrDefault<Product>(x => x.Id == query.Id);

            Assert.True(response.IsSuccessful);
            Assert.Equal(product.Id, response.Id);
            Assert.Equal(product.Code, response.Code);
            Assert.Equal(product.Description, response.Description);
            Assert.Equal(product.Highlights, response.Highlights);
            Assert.Equal(product.Name, response.Name);
            Assert.Equal(product.PricePerUnit, response.PricePerUnit);
        }

        /// <summary>
        /// Should throw not found exception.
        /// </summary>
        [Fact(DisplayName = "Should throw not found exception")]
        public async Task Should_throw_not_found_exception()
        {
            var (query, handler) = this.Build();
            query.Id = this.Faker.Random.Hexadecimal(24, string.Empty);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(query, CancellationToken.None);
            });
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetProductQuery query, GetProductHandler handler) Build()
        {
            var products = this.Repository.All<Product>();
            var product = this.Faker.PickRandom(products);
            var query = new GetProductQuery { Id = product.Id };

            var handler = new GetProductHandler(this.Repository);

            return (query, handler);
        }
    }
}