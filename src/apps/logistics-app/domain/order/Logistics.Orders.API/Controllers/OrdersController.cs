// -----------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS;
    using Devkit.WebAPI;
    using FluentValidation;
    using Logistics.Orders.API.Business.Orders.Commands.CancelOrder;
    using Logistics.Orders.API.Business.Orders.Commands.SubmitOrder;
    using Logistics.Orders.API.Business.Orders.Commands.UpdateOrder;
    using Logistics.Orders.API.Business.Orders.Queries.CalculateCost;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyOrders;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    ///     Order controller.
    /// </summary>
    [Route("[controller]")]
    public class OrdersController : DevkitControllerBase
    {
        /// <summary>
        ///     Calculates the cost.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>
        ///     A delivery cost view model.
        /// </returns>
        [HttpGet("cost")]
        public async Task<DeliveryCostVM> CalculateCost([FromQuery] CalculateCostQuery query)
        {
            return await this.Mediator.Send(query);
        }

        /// <summary>
        ///     Disputes the specified request.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     An order.
        /// </returns>
        [HttpPatch("{id}/cancel")]
        public async Task<OrderVM> CancelOrder([FromRoute] string id, [FromBody] CancelOrderCommand request)
        {
            if (request == null)
            {
                throw new ValidationException($"{nameof(request)} cannot be null.");
            }

            request.Id = id;
            return await this.Mediator.Send(request);
        }

        /// <summary>
        /// Gets my active orders.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// A list of orders.
        /// </returns>
        [HttpGet("active/{clientUserName}")]
        public async Task<ResponseSet<OrderVM>> GetMyActiveOrders([FromRoute] GetMyActiveOrdersQuery request) => await this.Mediator.Send(request);

        /// <summary>
        ///     Gets my orders.
        /// </summary>
        /// <param name="clientUserName">Name of the client user.</param>
        /// <param name="status">The status.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     A list of orders.
        /// </returns>
        [HttpGet("{status}/{clientUserName}")]
        public async Task<ResponseSet<OrderVM>> GetMyOrders(
            [FromRoute] string clientUserName,
            [FromRoute] string status,
            [FromQuery] GetMyOrdersQuery request)
        {
            if (request == null)
            {
                throw new ValidationException($"{nameof(request)} cannot be null.");
            }

            request.ClientUserName = clientUserName;
            request.Status = EnumerationBase.FromDisplayName<StatusCode>(status);
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Creates the Order.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>An order.</returns>
        [HttpPost("")]
        public async Task<OrderVM> SubmitOrder([FromBody] SubmitOrderCommand request)
        {
            return await this.Mediator.Send(request);
        }

        /// <summary>
        ///     Updates the Order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        ///     An order.
        /// </returns>
        [HttpPatch("{id}")]
        public async Task<OrderVM> UpdateOrder([FromRoute] string id, [FromBody] UpdateOrderCommand request)
        {
            if (request == null)
            {
                throw new ValidationException($"{nameof(request)} cannot be null.");
            }

            request.Id = id;
            return await this.Mediator.Send(request);
        }
    }
}