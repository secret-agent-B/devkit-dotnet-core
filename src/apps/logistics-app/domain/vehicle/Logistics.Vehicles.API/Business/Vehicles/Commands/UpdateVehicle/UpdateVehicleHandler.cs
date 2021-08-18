// -----------------------------------------------------------------------
// <copyright file="UpdateVehicleHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;
    using MongoDB.Driver;

    /// <summary>
    /// The UpdateVehicleHandler takes in an UpdateVehicleCommand and updates a vehicle.
    /// </summary>
    public class UpdateVehicleHandler : CommandHandlerBase<UpdateVehicleCommand, VehicleVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVehicleHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public UpdateVehicleHandler(IRepository repository)
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
            var vehicle = this.Repository.UpdateWithAudit<Vehicle>(
                x => x.Id == this.Request.Id,
                builder => builder
                    .Set(x => x.Manufacturer, this.Request.Manufacturer)
                    .Set(x => x.Model, this.Request.Model)
                    .Set(x => x.OwnerUserName, this.Request.OwnerUserName)
                    .Set(x => x.Photo, this.Request.Photo)
                    .Set(x => x.PlateNumber, this.Request.PlateNumber)
                    .Set(x => x.VIN, this.Request.VIN)
                    .Set(x => x.Year, this.Request.Year));

            this.Response = new VehicleVM(vehicle);

            return Task.CompletedTask;
        }
    }
}