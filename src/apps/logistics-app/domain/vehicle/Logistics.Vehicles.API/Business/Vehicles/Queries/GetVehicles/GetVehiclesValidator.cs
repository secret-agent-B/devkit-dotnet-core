// -----------------------------------------------------------------------
// <copyright file="GetVehiclesValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles
{
    using FluentValidation;

    /// <summary>
    /// The GetVehiclesValidator validates the GetVehiclesQuery.
    /// </summary>
    /// <seealso cref="AbstractValidator{GetVehiclesQuery}" />
    public class GetVehiclesValidator : AbstractValidator<GetVehiclesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehiclesValidator"/> class.
        /// </summary>
        public GetVehiclesValidator()
        {
            this.RuleFor(x => x.OwnerUserName)
                .NotNull()
                .NotEmpty();
        }
    }
}