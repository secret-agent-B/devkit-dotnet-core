// -----------------------------------------------------------------------
// <copyright file="AssignWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.AssignWork
{
    using FluentValidation;

    /// <summary>
    /// Validator for assigning work.
    /// </summary>
    /// <seealso cref="AbstractValidator{AssignWorkCommand}" />
    public class AssignWorkValidator : AbstractValidator<AssignWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignWorkValidator"/> class.
        /// </summary>
        public AssignWorkValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Id)
                .Length(24);
        }
    }
}