// -----------------------------------------------------------------------
// <copyright file="GetVehiclesHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;

    /// <summary>
    /// The GetVehiclesHandler handles the GetVehiclesQuery request.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{GetVehiclesQuery, ResponseSet{VehicleVM}}" />
    public class GetVehiclesHandler : QueryHandlerBase<GetVehiclesQuery, ResponseSet<VehicleVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetVehiclesHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var vehicles = this.Repository.GetMany<Vehicle>(x =>
                x.OwnerUserName == this.Request.OwnerUserName
                && (this.Request.IsActive == null || x.IsActive == this.Request.IsActive));

            this.Response.Items.AddRange(vehicles.Select(x => new VehicleVM(x)));

            return Task.CompletedTask;
        }
    }
}