// -----------------------------------------------------------------------
// <copyright file="GetMyOrdersValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.GetMyOrders
{
    using System;
    using FluentValidation;
    using Logistics.Orders.API.Business.Validators;

    /// <summary>
    /// The GetMyOrdersValidator valudates the GetMyOrdersQuery.
    /// </summary>
    public class GetMyOrdersValidator : AbstractValidator<GetMyOrdersQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyOrdersValidator"/> class.
        /// </summary>
        public GetMyOrdersValidator()
        {
            this.RuleFor(x => x.ClientUserName)
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