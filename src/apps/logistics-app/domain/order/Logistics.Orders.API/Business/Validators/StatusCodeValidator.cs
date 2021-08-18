// -----------------------------------------------------------------------
// <copyright file="StatusCodeValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Validators
{
    using System.Linq;
    using Devkit.Patterns;
    using FluentValidation;
    using Logistics.Orders.API.Constants;

    /// <summary>
    /// The status code validator.
    /// </summary>
    /// <seealso cref="AbstractValidator{LocationVM}" />
    public class StatusCodeValidator : AbstractValidator<StatusCode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCodeValidator"/> class.
        /// </summary>
        public StatusCodeValidator()
        {
            this.RuleFor(x => x.Value)
                .Must(value => EnumerationBase.GetAll<StatusCode>().Contains(StatusCode.FromValue<StatusCode>(value)));
        }
    }
}