// -----------------------------------------------------------------------
// <copyright file="GetVehiclesQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles
{
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Vehicles.API.Business.ViewModels;

    /// <summary>
    /// The GetVehiclesQuery is used to pull vehicles owned by a specific user.
    /// </summary>
    public class GetVehiclesQuery : QueryRequestBase<ResponseSet<VehicleVM>>
    {
        /// <summary>
        /// Gets or sets the name of the owner user.
        /// </summary>
        /// <value>
        /// The name of the owner user.
        /// </value>
        public string OwnerUserName { get; set; }

        /// <summary>
        /// Gets or sets the is active.
        /// </summary>
        /// <value>
        /// The is active.
        /// </value>
        public bool? IsActive { get; set; }
    }
}