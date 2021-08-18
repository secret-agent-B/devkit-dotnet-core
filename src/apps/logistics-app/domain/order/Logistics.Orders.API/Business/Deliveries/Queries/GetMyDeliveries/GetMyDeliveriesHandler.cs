// -----------------------------------------------------------------------
// <copyright file="GetMyDeliveriesHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// The handler for GetMyDeliveriesQuery.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{GetMyDeliveriesQuery, ResponseSet{OrderVM}}" />
    public class GetMyDeliveriesHandler : QueryHandlerBase<GetMyDeliveriesQuery, ResponseSet<OrderVM>>
    {
        /// <summary>
        /// The get user client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyDeliveriesHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public GetMyDeliveriesHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var clientFilter = Builders<Order>.Filter.Eq(x => x.DriverUserName, this.Request.DriverUserName);
            var startDateFilter = Builders<Order>.Filter.Gte(x => x.CreatedOn, this.Request.StartDate);
            var endDateFilter = Builders<Order>.Filter.Lte(x => x.CreatedOn, this.Request.EndDate);
            var statusFilter = Builders<Order>.Filter.Eq(x => x.CurrentStatus, this.Request.Status.Value);
            var combinedFilter = Builders<Order>.Filter.And(clientFilter, statusFilter, startDateFilter, endDateFilter);

            var collection = this.Repository.GetCollection<Order>();

            var orders = new List<Order>();

            using (var cursor = await collection.Find(combinedFilter).ToCursorAsync(cancellationToken))
            {
                while (await cursor.MoveNextAsync(cancellationToken))
                {
                    foreach (var order in cursor.Current)
                    {
                        orders.Add(order);
                    }
                }
            }

            var orderVMs = orders.Select(x => new OrderVM(x)).ToList();

            await orderVMs.SetUserInfos(this._getUsersClient);

            this.Response.Items.AddRange(orderVMs);
        }
    }
}