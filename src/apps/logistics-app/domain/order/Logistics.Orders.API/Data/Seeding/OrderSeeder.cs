// -----------------------------------------------------------------------
// <copyright file="OrderSeeder.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Seeding
{
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Data.Seeding;
    using Logistics.Orders.API.Data.Models;
    using MongoDB.Driver;

    /// <summary>
    /// The delivery api database seeder.
    /// </summary>
    /// <seealso cref="ExcelSeederBase" />
    public class OrderSeeder : ExcelSeederBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSeeder"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="seederConfig">The seeder configuration.</param>
        public OrderSeeder(IRepository repository, ISeederConfig seederConfig)
            : base(repository, seederConfig)
        {
        }

        /// <summary>
        /// Executes the seeding process.
        /// </summary>
        public override async Task Execute()
        {
            await this.Repository.GetCollection<Order>().Indexes.CreateManyAsync(
                new[] {
                    new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Geo2DSphere(x => x.Origin.Coordinates)),
                    new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Geo2DSphere(x => x.Destination.Coordinates))
                }
            );
        }
    }
}