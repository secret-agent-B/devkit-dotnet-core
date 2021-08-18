// -----------------------------------------------------------------------
// <copyright file="VehiclesController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.WebAPI;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle;
    using Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The VehiclesController is the controller for vehicle collection commands and queries..
    /// </summary>
    [Route("[controller]")]
    public class VehiclesController : DevkitControllerBase
    {
        /// <summary>
        /// Creates the transaction that is used for generating an invoice for payment vendors.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A vehicle view model.</returns>
        [HttpPost("")]
        public async Task<VehicleVM> AddVehicle([FromBody] AddVehicleCommand request) => await this.Mediator.Send(request);

        /// <summary>
        /// Gets the vehicles.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A collection vehicle view models.
        /// </returns>
        [HttpGet("{ownerUserName}")]
        public async Task<ResponseSet<VehicleVM>> GetVehicles([FromRoute] GetVehiclesQuery request) => await this.Mediator.Send(request);

        /// <summary>
        /// Updates the vehicle.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A vehicle view model.</returns>
        [HttpPut("")]
        public async Task<VehicleVM> UpdateVehicle([FromBody] UpdateVehicleCommand request) => await this.Mediator.Send(request);

        /// <summary>
        /// Updates the vehicle.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A vehicle view model.</returns>
        [HttpDelete("")]
        public async Task<IActionResult> DeleteVehicle([FromBody] UpdateVehicleCommand request)
        {
            var response = await this.Mediator.Send(request);

            if (response.IsSuccessful)
            {
                return this.NoContent();
            }
            else
            {
                return this.BadRequest();
            }
        }
    }
}