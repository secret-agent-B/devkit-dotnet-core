// -----------------------------------------------------------------------
// <copyright file="DeactivateVehicleCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.DeactivateVehicle
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Vehicles.API.Business.ViewModels;

    /// <summary>
    /// Command for deactivating a command.
    /// </summary>
    public class DeactivateVehicleCommand : CommandRequestBase<VehicleVM>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}