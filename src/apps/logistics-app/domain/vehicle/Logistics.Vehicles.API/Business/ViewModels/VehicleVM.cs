// -----------------------------------------------------------------------
// <copyright file="VehicleVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Devkit.Patterns.CQRS;
    using Logistics.Vehicles.API.Data;

    /// <summary>
    /// The VehicleVM represents a vehicle of a driver.
    /// </summary>
    public class VehicleVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleVM"/> class.
        /// </summary>
        public VehicleVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleVM"/> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public VehicleVM([NotNull] Vehicle vehicle)
        {
            this.Id = vehicle.Id;
            this.CreatedOn = vehicle.CreatedOn;
            this.LastUpdatedOn = vehicle.LastUpdatedOn;
            this.Manufacturer = vehicle.Manufacturer;
            this.Model = vehicle.Model;
            this.OwnerUserName = vehicle.OwnerUserName;
            this.Photo = vehicle.Photo;
            this.PlateNumber = vehicle.PlateNumber;
            this.VIN = vehicle.VIN;
            this.Year = vehicle.Year;
        }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the last updated on.
        /// </summary>
        /// <value>
        /// The last updated on.
        /// </value>
        public DateTime? LastUpdatedOn { get; set; }

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
        /// Gets or sets the vin.
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