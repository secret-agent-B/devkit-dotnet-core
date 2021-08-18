// -----------------------------------------------------------------------
// <copyright file="FindWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.FindWork
{
    using FluentValidation;

    /// <summary>
    /// The FindWorkValidator validates the FindWorkQuery.
    /// </summary>
    public class FindWorkValidator : AbstractValidator<FindWorkQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindWorkValidator" /> class.
        /// </summary>
        public FindWorkValidator()
        {
            this.RuleFor(x => x.ExcludeUserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            this.RuleFor(x => x.Page)
                .GreaterThan(0);

            this.RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(10)
                .LessThanOrEqualTo(50);
        }
    }
}