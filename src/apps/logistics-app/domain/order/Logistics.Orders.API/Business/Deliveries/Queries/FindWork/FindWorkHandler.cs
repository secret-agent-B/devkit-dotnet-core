// -----------------------------------------------------------------------
// <copyright file="FindWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.FindWork
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
    using Logistics.Orders.API.Options;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    /// <summary>
    /// The handler for finding work for a driver.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{FindWorkQuery, ResponseSet{OrderVM}}" />
    public class FindWorkHandler : QueryHandlerBase<FindWorkQuery, ResponseSet<OrderVM>>
    {
        /// <summary>
        /// The Earth radius.
        /// </summary>
        private const int _earthRadius = 6371;

        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// The search work options.
        /// </summary>
        private readonly SearchWorkOptions _searchWorkOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindWorkHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="searchWorkOptionsAccessor">The search work options accessor.</param>
        /// <param name="bus">The bus.</param>
        public FindWorkHandler(IRepository repository, [NotNull] IOptions<SearchWorkOptions> searchWorkOptionsAccessor, IBus bus)
            : base(repository)
        {
            this._searchWorkOptions = searchWorkOptionsAccessor.Value;
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
            var orderCollection = this.Repository.GetCollection<Order>();
            var pageSize = this._searchWorkOptions.MaxPageSize < this.Request.PageSize
                ? this._searchWorkOptions.MaxPageSize
                : this.Request.PageSize;

            var statusFilter = Builders<Order>
                .Filter
                .Where(x => x.CurrentStatus == StatusCode.Booked.Value);

            var geoFilter = Builders<Order>
                .Filter
                    .NearSphere(
                        x => x.Origin.Coordinates,
                        this.Request.Lng,
                        this.Request.Lat,
                        this._searchWorkOptions.MaxSearchDistanceInKm / _earthRadius);

            var orderFilter = Builders<Order>
                .Filter
                    .Where(x => x.ClientUserName != this.Request.ExcludeUserName);

            var orders = new List<Order>();

            using (var cursor = await orderCollection
                .Find(statusFilter & geoFilter & orderFilter)
                .Skip((this.Request.Page - 1) * this.Request.PageSize)
                .Limit(pageSize)
                .ToCursorAsync(cancellationToken))
            {
                while (await cursor.MoveNextAsync(cancellationToken))
                {
                    orders.AddRange(cursor.Current);
                }
            }

            var orderVms = orders.Select(x => new OrderVM(x)).ToList();

            await orderVms.SetUserInfos(this._getUsersClient);

            this.Response.Items.AddRange(orderVms);
        }
    }
}