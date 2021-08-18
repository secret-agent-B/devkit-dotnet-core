// -----------------------------------------------------------------------
// <copyright file="CreateUserHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Users.Commands.CreateUser
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.Ratings.Data.Models;

    /// <summary>
    /// Handler for creating a user.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{CreateUserCommand, UserResponse}" />
    public class CreateUserHandler : CommandHandlerBase<CreateUserCommand, UserVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CreateUserHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var user = this.Repository.GetOneOrDefault<User>(x => x.UserName == this.Request.UserName);

            if (user == null)
            {
                user = new User
                {
                    UserName = this.Request.UserName
                };

                this.Repository.AddWithAudit(user);
            }

            this.Response.UserName = user.UserName;

            return Task.CompletedTask;
        }
    }
}