// -----------------------------------------------------------------------
// <copyright file="ProductSeeder.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Seeders
{
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Data.Seeding;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// This product seeder.
    /// </summary>
    /// <seealso cref="JsonSeederBase{Product}" />
    public class ProductSeeder : ExcelSeederBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductSeeder" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seederConfig">The seeder configuration.</param>
        public ProductSeeder(IRepository repository, ISeederConfig seederConfig)
            : base(repository, seederConfig)
        {
        }

        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        public override Task Execute()
        {
            var products = this.DeserializeTabToList<Product>("Products");

            foreach (var product in products)
            {
                if (this.Repository.GetOneOrDefault<Product>(x => x.Code == product.Code) != null)
                {
                    continue;
                }

                this.Repository.AddWithAudit(product);
            }

            return Task.CompletedTask;
        }
    }
}