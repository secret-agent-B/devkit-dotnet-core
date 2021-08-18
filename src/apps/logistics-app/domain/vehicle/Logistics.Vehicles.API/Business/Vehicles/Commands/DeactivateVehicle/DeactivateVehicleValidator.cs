// -----------------------------------------------------------------------
// <copyright file="DeactivateVehicleValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.DeactivateVehicle
{
    using FluentValidation;

    /// <summary>
    /// Validator for deactivating vehicles.
    /// </summary>
    public class DeactivateVehicleValidator : AbstractValidator<DeactivateVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeactivateVehicleValidator"/> class.
        /// </summary>
        public DeactivateVehicleValidator()
        {
            this.RuleFor(x => x.Id)
                .NotNull()
                .NotEmpty()
                .Length(24);
        }
    }
}