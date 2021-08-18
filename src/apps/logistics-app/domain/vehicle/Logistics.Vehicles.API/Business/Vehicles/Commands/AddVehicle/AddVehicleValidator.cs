// -----------------------------------------------------------------------
// <copyright file="AddVehicleValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle
{
    using System;
    using Devkit.Patterns.Extensions;
    using FluentValidation;

    /// <summary>
    /// The AddVehicleValidator validates the AddVehicleCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{AddVehicleCommand}" />
    public class AddVehicleValidator : AbstractValidator<AddVehicleCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddVehicleValidator"/> class.
        /// </summary>
        public AddVehicleValidator()
        {
            this.RuleFor(x => x.Photo)
                .NotNull()
                .NotEmpty()
                .ValidBase64Image();

            this.RuleFor(x => x.Year)
                .GreaterThan(DateTime.Now.Year - 100)
                .LessThanOrEqualTo(DateTime.Now.Year + 1);

            this.RuleFor(x => x.Manufacturer)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            this.RuleFor(x => x.Model)
                .NotNull()
                .NotEmpty()
                .MaximumLength(25);

            this.RuleFor(x => x.OwnerUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.PlateNumber)
                .NotNull()
                .NotEmpty()
                .MaximumLength(15);

            this.RuleFor(x => x.VIN)
                .NotNull()
                .NotEmpty();
        }
    }
}