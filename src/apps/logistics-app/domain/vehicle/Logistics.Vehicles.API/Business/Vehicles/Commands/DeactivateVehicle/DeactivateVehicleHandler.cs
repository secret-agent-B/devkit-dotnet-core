// -----------------------------------------------------------------------
// <copyright file="DeactivateVehicleHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.DeactivateVehicle
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;

    /// <summary>
    /// The handler for deactivating a vehicle.
    /// </summary>
    public class DeactivateVehicleHandler : CommandHandlerBase<DeactivateVehicleCommand, VehicleVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateVehicleHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DeactivateVehicleHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this.Repository.UpdateWithAudit<Vehicle>(
                x => x.Id == this.Request.Id,
                builder => builder.Set(x => x.IsActive, false));

            return Task.CompletedTask;
        }
    }
}