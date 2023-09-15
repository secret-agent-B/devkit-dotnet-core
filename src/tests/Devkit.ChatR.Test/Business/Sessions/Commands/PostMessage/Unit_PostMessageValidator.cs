// -----------------------------------------------------------------------
// <copyright file="Unit_PostMessageValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.Business.Sessions.Commands.PostMessage
{
    using Devkit.ChatR.Business.Sessions.Commands.PostMessage;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// Unit test for PostMessageValidator.
    /// </summary>
    public class Unit_PostMessageValidator : UnitTestBase<PostMessageValidator>
    {
        /// <summary>
        /// Fails if message is empty.
        /// </summary>
        [TestCase(TestName = "Fails if message is empty")]
        public void Fail_if_message_is_empty()
        {
            var validator = this.Build()
                .TestValidate(new PostMessageCommand
                {
                    Message = string.Empty
                });
            validator.ShouldHaveValidationErrorFor(x => x.Message);
        }

        /// <summary>
        /// Fails if reply to is not valid hexadecimal identifier.
        /// </summary>
        [TestCase(TestName = "Fails if reply to is not valid hexadecimal identifier")]
        public void Fail_if_reply_to_is_not_valid_hex_id()
        {
            var validator = this.Build();

            validator
                .TestValidate(new PostMessageCommand
                {
                    ReplyTo = this.Faker.Random.Hexadecimal(23, string.Empty)
                })
                .ShouldHaveValidationErrorFor(x => x.ReplyTo);

            validator
                .TestValidate(new PostMessageCommand
                {
                    ReplyTo = this.Faker.Random.AlphaNumeric(1)
                })
                .ShouldHaveValidationErrorFor(x => x.ReplyTo);
        }

        /// <summary>
        /// Fails if session id is empty.
        /// </summary>
        [TestCase(TestName = "Fails if session id is empty")]
        public void Fail_if_session_id_is_empty()
        {
            var validator = this.Build()
                .TestValidate(new PostMessageCommand
                {
                    SessionId = string.Empty
                });

            validator.ShouldHaveValidationErrorFor(x => x.SessionId);
        }

        /// <summary>
        /// Fails if username is empty.
        /// </summary>
        [TestCase(TestName = "Fails if username is empty")]
        public void Fail_if_username_is_empty()
        {
            var validator = this.Build()
                .TestValidate(new PostMessageCommand
                {
                    UserName = string.Empty
                });

            validator.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        /// <summary>
        /// Passes if command is valid.
        /// </summary>
        [TestCase(TestName = "Passes if command is valid")]
        public void Pass_if_command_is_valid()
        {
            var validator = this.Build()
                .TestValidate(new PostMessageCommand
                {
                    UserName = this.Faker.Person.Email,
                    Message = this.Faker.Rant.Review(),
                    ReplyTo = this.Faker.Random.Hexadecimal(24, string.Empty),
                    SessionId = $"{this.Faker.Random.Hexadecimal(24, string.Empty)}_{this.Faker.Person.Email}",
                });

            validator.ShouldNotHaveValidationErrorFor(x => x.UserName);
            validator.ShouldNotHaveValidationErrorFor(x => x.Message);
            validator.ShouldNotHaveValidationErrorFor(x => x.ReplyTo);
            validator.ShouldNotHaveValidationErrorFor(x => x.SessionId);
        }
    }
}