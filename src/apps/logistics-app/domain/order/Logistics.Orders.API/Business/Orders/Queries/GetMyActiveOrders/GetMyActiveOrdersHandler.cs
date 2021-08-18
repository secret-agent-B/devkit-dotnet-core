// -----------------------------------------------------------------------
// <copyright file="GetMyActiveOrdersHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;

    /// <summary>
    /// GetMyActiveOrdersHandler class is the handler for GetMyActiveOrdersQuery.
    /// </summary>
    public class GetMyActiveOrdersHandler : QueryHandlerBase<GetMyActiveOrdersQuery, ResponseSet<OrderVM>>
    {
        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyActiveOrdersHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public GetMyActiveOrdersHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var orders = this.Repository.GetMany<Order>(x
                => x.ClientUserName == this.Request.ClientUserName &&
                   (x.CurrentStatus == StatusCode.Booked.Value
                   || x.CurrentStatus == StatusCode.Assigned.Value
                   || x.CurrentStatus == StatusCode.PickedUp.Value));

            var orderVMs = orders.Select(x => new OrderVM(x)).ToList();

            await orderVMs.SetUserInfos(this._getUsersClient);

            this.Response.Items.AddRange(orderVMs);
        }
    }
}