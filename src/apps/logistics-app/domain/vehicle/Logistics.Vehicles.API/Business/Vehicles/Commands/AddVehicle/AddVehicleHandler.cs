// -----------------------------------------------------------------------
// <copyright file="AddVehicleHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;

    /// <summary>
    /// The AddVehicleHandler handles the AddVehicleCommand.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{AddVehicleCommand, VehicleVM}" />
    public class AddVehicleHandler : CommandHandlerBase<AddVehicleCommand, VehicleVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public AddVehicleHandler(IRepository repository)
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
            var vehicle = new Vehicle
            {
                Manufacturer = this.Request.Manufacturer,
                Model = this.Request.Model,
                OwnerUserName = this.Request.OwnerUserName,
                Photo = this.Request.Photo,
                PlateNumber = this.Request.PlateNumber,
                VIN = this.Request.VIN,
                Year = this.Request.Year,
                IsActive = true
            };

            this.Repository.AddWithAudit(vehicle);

            this.Response = new VehicleVM(vehicle);

            return Task.CompletedTask;
        }
    }
}