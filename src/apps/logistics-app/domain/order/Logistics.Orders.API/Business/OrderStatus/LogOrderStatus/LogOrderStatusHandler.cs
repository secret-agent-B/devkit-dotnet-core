// -----------------------------------------------------------------------
// <copyright file="TrackStatusHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.OrderStatus.LogOrderStatus
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Patterns.Exceptions;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Data.Models;

    /// <summary>
    /// The UpdateStatusHandler processes the UpdateStatusCommand.
    /// </summary>
    public class LogOrderStatusHandler : CommandHandlerBase<LogOrderStatusCommand, OrderVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOrderStatusHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public LogOrderStatusHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var statuses = this.Repository.GetOneOrDefault<Order>(x => x.Id == this.Request.OrderId)?.Statuses;

            if (statuses == null)
            {
                throw new AppException($"Could not find {nameof(Order)} ({this.Request.OrderId}).");
            }

            statuses.Add(new Status
            {
                Value = this.Request.Code.Value,
                Comments = this.Request.Comments,
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserId
            });

            var updatedOrder = this.Repository.Update<Order>(
                x => x.Id == this.Request.OrderId,
                builder => builder
                    .Set(x => x.Statuses, statuses));

            return Task.CompletedTask;
        }
    }
}