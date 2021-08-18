// -----------------------------------------------------------------------
// <copyright file="UpdateVehicleCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Vehicles.API.Business.ViewModels;

    /// <summary>
    /// The UpdateVehicleCommand is the input command for updating a vehicle.
    /// </summary>
    public class UpdateVehicleCommand : CommandRequestBase<VehicleVM>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the make.
        /// </summary>
        /// <value>
        /// The make.
        /// </value>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the plate number.
        /// </summary>
        /// <value>
        /// The plate number.
        /// </value>
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets the vehicle identification number.
        /// </summary>
        /// <value>
        /// The vin.
        /// </value>
        public string VIN { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public int Year { get; set; }
    }
}