// -----------------------------------------------------------------------
// <copyright file="Unit_PostMessageValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.Business.Sessions.Commands.PostMessage
{
    using Xunit;
    using Devkit.ChatR.Business.Sessions.Commands.PostMessage;
    using Devkit.Test;
    using FluentValidation.TestHelper;

    /// <summary>
    /// Unit test for PostMessageValidator.
    /// </summary>
    public class Unit_PostMessageValidator : UnitTestBase<PostMessageValidator>
    {
        /// <summary>
        /// Fails if message is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if message is empty")]
        public void Fail_if_message_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Message, string.Empty);
        }

        /// <summary>
        /// Fails if reply to is not valid hexadecimal identifier.
        /// </summary>
        [Fact(DisplayName = "Fails if reply to is not valid hexadecimal identifier")]
        public void Fail_if_reply_to_is_not_valid_hex_id()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.ReplyTo, this.Faker.Random.Hexadecimal(23, string.Empty));
            validator.ShouldHaveValidationErrorFor(x => x.ReplyTo, this.Faker.Random.AlphaNumeric(1));
        }

        /// <summary>
        /// Fails if session id is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if session id is empty")]
        public void Fail_if_session_id_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.SessionId, string.Empty);
        }

        /// <summary>
        /// Fails if username is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if username is empty")]
        public void Fail_if_username_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.UserName, string.Empty);
        }

        /// <summary>
        /// Passes if command is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if command is valid")]
        public void Pass_if_command_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, this.Faker.Person.Email);
            validator.ShouldNotHaveValidationErrorFor(x => x.Message, this.Faker.Rant.Review());
            validator.ShouldNotHaveValidationErrorFor(x => x.ReplyTo, this.Faker.Random.Hexadecimal(24, string.Empty));
            validator.ShouldNotHaveValidationErrorFor(x => x.SessionId, $"{this.Faker.Random.Hexadecimal(24, string.Empty)}_{this.Faker.Person.Email}");
        }
    }
}