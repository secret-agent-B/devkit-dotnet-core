// -----------------------------------------------------------------------
// <copyright file="GetMyDeliveriesValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries
{
    using System;
    using FluentValidation;
    using Logistics.Orders.API.Business.Validators;

    /// <summary>
    /// The GetMyDeliveriesValidator valudates the GetMyDeliveriesQuery.
    /// </summary>
    public class GetMyDeliveriesValidator : AbstractValidator<GetMyDeliveriesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyDeliveriesValidator"/> class.
        /// </summary>
        public GetMyDeliveriesValidator()
        {
            this.RuleFor(x => x.DriverUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.StartDate)
                .NotNull()
                .GreaterThan(DateTime.MinValue)
                .LessThan(q => q.EndDate);

            this.RuleFor(x => x.EndDate)
                .NotNull()
                .GreaterThan(q => q.StartDate)
                .LessThan(DateTime.MaxValue);

            this.RuleFor(x => x.Status)
                .SetValidator(new StatusCodeValidator());
        }
    }
}