// -----------------------------------------------------------------------
// <copyright file="VehicleFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Fakers
{
    using System;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using Logistics.Vehicles.API.Data;

    /// <summary>
    /// The VehicleFaker creates fake vehicles.
    /// </summary>
    public class VehicleFaker : FakerBase<Vehicle>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override Vehicle Generate()
        {
            this.Faker
                .RuleFor(x => x.Manufacturer, f => f.Vehicle.Manufacturer())
                .RuleFor(x => x.Model, f => f.Vehicle.Model())
                .RuleFor(x => x.OwnerUserName, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Photo, "image/jpeg;base64,R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")
                .RuleFor(x => x.PlateNumber, f => f.Vehicle.GbRegistrationPlate(f.Date.Past(4), DateTime.Now))
                .RuleFor(x => x.VIN, f => f.Vehicle.Vin())
                .RuleFor(x => x.Year, DateTime.Now.Year)
                .RuleFor(x => x.IsActive, true);

            return this.Faker.Generate();
        }
    }
}