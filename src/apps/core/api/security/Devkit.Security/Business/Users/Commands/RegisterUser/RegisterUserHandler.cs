// -----------------------------------------------------------------------
// <copyright file="RegisterUserHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.RegisterUser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.Communication.Security.Messages.Events;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Patterns.Exceptions;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Security.Data.Models;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Handler for registering a new user.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{RegisterUserCommand, RegisterUserResponse}" />
    public class RegisterUserHandler : CommandHandlerBase<RegisterUserCommand, UserVM>
    {
        /// <summary>
        /// The image size.
        /// </summary>
        private const int _imageSize = 800;

        /// <summary>
        /// The bus.
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// The role manager.
        /// </summary>
        private readonly RoleManager<UserRole> _roleManager;

        /// <summary>
        /// The upload base64 image client.
        /// </summary>
        private readonly IRequestClient<IUploadBase64Image> _uploadFileClient;

        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly UserManager<UserAccount> _userManager;

        /// <summary>
        /// The user account.
        /// </summary>
        private UserAccount _userAccount;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterUserHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="roleManager">The role manager.</param>
        /// <param name="bus">The bus.</param>
        public RegisterUserHandler(
            IRepository repository,
            UserManager<UserAccount> userManager,
            RoleManager<UserRole> roleManager,
            IBus bus)
            : base(repository)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._bus = bus;
            this._uploadFileClient = bus.CreateRequestClient<IUploadBase64Image>();
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            if (this.Repository.GetOneOrDefault<UserAccount>(x => x.PhoneNumber == this.Request.PhoneNumber) != null)
            {
                this.Response.AddException(nameof(this.Request.PhoneNumber), $"The phone number ({this.Request.PhoneNumber}) is already in use.");
            }

            this._userAccount = new UserAccount(this.Request.UserName)
            {
                CreatedOn = DateTime.Now,
                Email = this.Request.Email,
                PhoneNumber = this.Request.PhoneNumber,
                Profile = new UserProfile
                {
                    FirstName = this.Request.FirstName,
                    MiddleName = this.Request.MiddleName,
                    LastName = this.Request.LastName,
                    FullName = $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}",

                    NormalizedFirstName = this.Request.FirstName.ToUpperInvariant(),
                    NormalizedMiddleName = this.Request.MiddleName.ToUpperInvariant(),
                    NormalizedLastName = this.Request.LastName.ToUpperInvariant(),
                    NormalizedFullName
                        = $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}".ToUpperInvariant(),

                    Address1 = this.Request.Address1,
                    Address2 = this.Request.Address2,
                    City = this.Request.City,
                    Province = this.Request.Province,
                    Country = this.Request.Country,
                    ZipCode = this.Request.ZipCode,
                }
            };

            var uploadIdentificationCardsSuccessful = await this.UploadIdentificationCards(cancellationToken);
            var uploadSelfieSuccessful = await this.UploadSelfie(cancellationToken);

            if (uploadIdentificationCardsSuccessful && uploadSelfieSuccessful)
            {
                var createResult = await this._userManager.CreateAsync(this._userAccount, this.Request.Password);

                if (createResult.Succeeded)
                {
                    await this.AssignRole(this._userAccount);

                    this.Response.UserName = this._userAccount.UserName;
                    this.Response.FirstName = this._userAccount.Profile.FirstName;
                    this.Response.LastName = this._userAccount.Profile.LastName;
                    this.Response.CreatedOn = this._userAccount.CreatedOn;
                    this.Response.Address1 = this._userAccount.Profile.Address1;
                    this.Response.Address2 = this._userAccount.Profile.Address2;
                    this.Response.City = this._userAccount.Profile.City;
                    this.Response.Country = this._userAccount.Profile.Country;
                    this.Response.Province = this._userAccount.Profile.Province;
                    this.Response.ZipCode = this._userAccount.Profile.ZipCode;
                    this.Response.PhoneNumber = this._userAccount.PhoneNumber;
                    this.Response.Email = this._userAccount.Email;
                }
                else
                {
                    foreach (var error in createResult.Errors)
                    {
                        this.Response.Exceptions.Add(error.Code, new[] { error.Description });
                    }
                }
            }
        }

        /// <summary>
        /// Posts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        protected async override Task PostProcessing(CancellationToken cancellationToken)
        {
            await this._bus.Publish<IUserCreated>(new
            {
                this.Response.FirstName,
                this.Response.LastName,
                Roles = new[] { this.Request.Role },
                this.Response.UserName
            }, cancellationToken);
        }

        /// <summary>
        /// Assigns the role.
        /// </summary>
        /// <returns>A task.</returns>
        private async Task AssignRole(UserAccount user)
        {
            string role;

            switch (this.Request.Role.ToLower())
            {
                case "client":
                    role = "client";
                    break;

                case "driver":
                    role = "driver";
                    break;

                default:
                    throw new AppException($"Invalid application role ({this.Request.Role.ToLower()}).");
            }

            if (await this._roleManager.FindByNameAsync(role) == null)
            {
                await this._roleManager.CreateAsync(new UserRole(role));
            }

            await this._userManager.AddToRoleAsync(user, role);
        }

        /// <summary>
        /// Uploads the identification cards.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        private async Task<bool> UploadIdentificationCards(CancellationToken cancellationToken)
        {
            foreach (var idCard in this.Request.IdentificationCards.Where(x => !string.IsNullOrEmpty(x.Photo)))
            {
                var (request, exception) = await this._uploadFileClient.GetResponse<IFileDTO, IConsumerException>(new
                {
                    Contents = idCard.Photo,
                    FileName = $"{Guid.NewGuid().ToString()}.img",
                    Size = _imageSize
                }, cancellationToken);

                if (request.IsCompletedSuccessfully)
                {
                    var fileDto = (await request).Message;

                    this._userAccount.Profile.IdentificationCards.Add(new IdentificationCard
                    {
                        ImageId = fileDto.Id,
                        Number = idCard.Number,
                        Type = idCard.Type
                    });
                }
                else
                {
                    this.Response.Exceptions.Add(nameof(IUploadBase64Image), new[] { (await exception).Message.ErrorMessage });
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Uploads the selfie.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        private async Task<bool> UploadSelfie(CancellationToken cancellationToken)
        {
            var (request, exception) = await this._uploadFileClient.GetResponse<IFileDTO, IConsumerException>(new
            {
                Contents = this.Request.Photo,
                FileName = $"{Guid.NewGuid().ToString()}.img",
                Size = _imageSize
            }, cancellationToken);

            if (request.IsCompletedSuccessfully)
            {
                var fileDto = (await request).Message;
                this._userAccount.Profile.SelfieId = fileDto.Id;
            }
            else
            {
                this.Response.Exceptions.Add(nameof(IUploadBase64Image), new[] { (await exception).Message.ErrorMessage });
                return false;
            }

            return true;
        }
    }
}