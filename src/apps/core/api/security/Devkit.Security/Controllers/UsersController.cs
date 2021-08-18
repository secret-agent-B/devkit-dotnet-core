// -----------------------------------------------------------------------
// <copyright file="UsersController.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Controllers
{
    using System.Threading.Tasks;
    using Devkit.Security.Business.Users.Commands.RegisterUser;
    using Devkit.Security.Business.Users.Commands.UpdateUser;
    using Devkit.Security.Business.Users.Queries.GetMyProfile;
    using Devkit.Security.Business.ViewModels;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The users controller.
    /// </summary>
    /// <seealso cref="DevkitControllerBase" />
    [Route("[controller]")]
    public class UsersController : DevkitControllerBase
    {
        /// <summary>
        /// Gets the user information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        // TODO: THIS ROUTE EXPOSES USERS PERSONALLY IDENTIFIABLE INFORMATION (fix before end of jan 20201)
        // TODO: THIS ROUTE NEEDS TO BE PROTECTED (fix before end of jan 20201)
        [HttpGet("{userName}")]
        public async Task<UserVM> GetMyProfile([FromRoute] GetMyProfileQuery request) => await this.Mediator.Send(request);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <param name="request">The request.</param>
        /// <returns>
        /// An register user response.
        /// </returns>
        [HttpPost("{role}/register/")]
        public async Task<UserVM> RegisterUser([FromRoute] string role, [FromBody] RegisterUserCommand request)
        {
            request.Role = role;
            return await this.Mediator.Send(request);
        }

        /// <summary>
        /// Updates the user information.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPatch("")]
        public async Task<UserVM> UpdateUpdate([FromBody] UpdateUserCommand command) => await this.Mediator.Send(command);
    }
}