// -----------------------------------------------------------------------
// <copyright file="DeliveriesController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS;
    using Devkit.WebAPI;
    using Logistics.Orders.API.Business.Deliveries.Commands.AssignWork;
    using Logistics.Orders.API.Business.Deliveries.Commands.CancelWork;
    using Logistics.Orders.API.Business.Deliveries.Commands.CompleteWork;
    using Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork;
    using Logistics.Orders.API.Business.Deliveries.Commands.UpdateSpecialInstructions;
    using Logistics.Orders.API.Business.Deliveries.Queries.FindWork;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Order controller.
    /// </summary>
    [Route("[controller]")]
    public class DeliveriesController : DevkitControllerBase
    {
        /// <summary>
        ///     Assigns the work.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A delivery.</returns>
        [HttpPatch("assign")]
        public async Task<OrderVM> AssignWork([FromBody] AssignWorkCommand request)
        {
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Cancels the delivery work.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A delivery.</returns>
        [HttpPatch("cancel")]
        public async Task<OrderVM> CancelWork([FromBody] CancelWorkCommand request)
        {
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Completes the delivery work.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A delivery.</returns>
        [HttpPatch("complete")]
        public async Task<OrderVM> CompleteWork([FromBody] CompleteWorkCommand request)
        {
            return await this.Mediator.Send(request);
        }

        /// <summary>
        /// Search deliveries by geolocation.
        /// </summary>
        /// <param name="excludeUserName">Name of the exclude user.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     A list of deliveries.
        /// </returns>
        [HttpGet("{excludeUserName}")]
        public async Task<ResponseSet<OrderVM>> FindWork([FromRoute] string excludeUserName,
            [FromQuery] FindWorkQuery request)
        {
            request.ExcludeUserName = excludeUserName;
            return await this.Mediator.Send(request);
        }

        /// <summary>
        /// Gets my active deliveries.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A list of orders.
        /// </returns>
        [HttpGet("active/{driverUserName}")]
        public async Task<ResponseSet<OrderVM>> GetMyActiveDeliveries([FromRoute] GetMyActiveDeliveriesQuery request) => await this.Mediator.Send(request);

        /// <summary>
        ///     Gets my deliveries.
        /// </summary>
        /// <param name="driverUserName">Name of the driver user.</param>
        /// <param name="status">The status.</param>
        /// <param name="request">The request.</param>
        /// <returns>A list of deliveries.</returns>
        [HttpGet("{status}/{driverUserName}")]
        public async Task<ResponseSet<OrderVM>> GetMyDeliveries(
            [FromRoute] string driverUserName,
            [FromRoute] string status,
            [FromQuery] GetMyDeliveriesQuery request)
        {
            request.DriverUserName = driverUserName;
            request.Status = EnumerationBase.FromDisplayName<StatusCode>(status);
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Completes the delivery work.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A delivery.</returns>
        [HttpPatch("picked-up")]
        public async Task<OrderVM> PickUpWork([FromBody] PickUpWorkCommand request)
        {
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Updates the delivery.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An order.</returns>
        [HttpPatch("special-instructions")]
        public async Task<OrderVM> UpdateSpecialInstructions([FromBody] UpdateSpecialInstructionsCommand request)
        {
            return await this.Mediator.Send(request);
        }
    }
}