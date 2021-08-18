// -----------------------------------------------------------------------
// <copyright file="PostMessageValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.Sessions.Commands.PostMessage
{
    using FluentValidation;

    /// <summary>
    /// The validator for PostMessageCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{PostMessageCommand}" />
    public class PostMessageValidator : AbstractValidator<PostMessageCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostMessageValidator"/> class.
        /// </summary>
        public PostMessageValidator()
        {
            this.RuleFor(x => x.SessionId)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Message)
                .NotNull()
                .NotEmpty()
                .MaximumLength(255);

            this.RuleFor(x => x.ReplyTo)
                .Length(24);
        }
    }
}