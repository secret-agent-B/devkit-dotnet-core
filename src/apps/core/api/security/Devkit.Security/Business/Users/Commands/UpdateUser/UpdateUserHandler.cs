// -----------------------------------------------------------------------
// <copyright file="UpdateUserHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.UpdateUser
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Security.Data.Models;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// UpdateUserHandler class is handler for UpdateUserCommand.
    /// </summary>
    public class UpdateUserHandler : CommandHandlerBase<UpdateUserCommand, UserVM>
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
        /// The upload file client.
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
        /// Initializes a new instance of the <see cref="UpdateUserHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="userManager">The user store.</param>
        /// <param name="bus">The bus.</param>
        public UpdateUserHandler(IRepository repository, UserManager<UserAccount> userManager, IBus bus)
            : base(repository)
        {
            this._userManager = userManager;
            this._uploadFileClient = bus.CreateRequestClient<IUploadBase64Image>();
            this._bus = bus;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            this._userAccount = await this._userManager.FindByNameAsync(this.Request.UserName);

            this._userAccount.Profile.FirstName = this.Request.FirstName;
            this._userAccount.Profile.MiddleName = this.Request.MiddleName;
            this._userAccount.Profile.LastName = this.Request.LastName;
            this._userAccount.Profile.FullName =
                $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}";
            this._userAccount.Profile.NormalizedFirstName = this.Request.FirstName.ToUpperInvariant();
            this._userAccount.Profile.NormalizedMiddleName = this.Request.MiddleName.ToUpperInvariant();
            this._userAccount.Profile.NormalizedLastName = this.Request.LastName.ToUpperInvariant();
            this._userAccount.Profile.NormalizedFullName =
                $"{this.Request.FirstName} {this.Request.MiddleName} {this.Request.LastName}".ToUpperInvariant();
            this._userAccount.Profile.Address1 = this.Request.Address1;
            this._userAccount.Profile.Address2 = this.Request.Address2;
            this._userAccount.Profile.City = this.Request.City;
            this._userAccount.Profile.Province = this.Request.Province;
            this._userAccount.Profile.Country = this.Request.Country;
            this._userAccount.Profile.ZipCode = this.Request.ZipCode;

            await this.UpdateSelfie(cancellationToken);
            await this.UploadIdentificationCards(cancellationToken);
            await this.DeleteIdentificationCards(cancellationToken);

            var updateResult = await this._userManager.UpdateAsync(this._userAccount);

            if (updateResult.Succeeded)
            {
                this.Response = new UserVM(this._userAccount)
                {
                    Address1 = this._userAccount.Profile.Address1,
                    Address2 = this._userAccount.Profile.Address2,
                    City = this._userAccount.Profile.City,
                    Province = this._userAccount.Profile.Province,
                    Country = this._userAccount.Profile.Country,
                    ZipCode = this._userAccount.Profile.ZipCode,
                    IdentificationCards = this._userAccount.Profile.IdentificationCards
                        .Select(x =>
                            new IdentificationCardVM
                            {
                                ImageId = x.ImageId,
                                Number = x.Number,
                                Type = x.Type
                            })
                        .ToList(),
                    SelfieId = this._userAccount.Profile.SelfieId
                };
            }
        }

        /// <summary>
        /// Deletes the identification cards.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task DeleteIdentificationCards(CancellationToken cancellationToken)
        {
            var removableIds = this._userAccount.Profile.IdentificationCards
                                   .Where(x => this.Request.RemoveIdentificationCardImageIds.Contains(x.ImageId)).ToArray();

            for (var i = removableIds.Count(); i > 0; i--)
            {
                var currentId = removableIds[i - 1];
                await this._bus.Publish<IDeleteFile>(
                   new
                   {
                       Id = currentId.ImageId
                   }, cancellationToken);

                this._userAccount.Profile.IdentificationCards.Remove(currentId);
            }
        }

        /// <summary>
        /// Updates the selfie.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        private async Task UpdateSelfie(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(this.Request.Photo))
            {
                return;
            }

            var (request, exception) = await this._uploadFileClient.GetResponse<IFileDTO, IConsumerException>(new
            {
                Contents = this.Request.Photo,
                FileName = $"{Guid.NewGuid().ToString()}.img",
                Size = _imageSize
            }, cancellationToken);

            if (request.IsCompletedSuccessfully)
            {
                var fileDto = (await request).Message;

                // Delete the selfie from the database
                await this._bus.Publish<IDeleteFile>(
                    new
                    {
                        Id = this._userAccount.Profile.SelfieId
                    }, cancellationToken);

                this._userAccount.Profile.SelfieId = fileDto.Id;
            }
            else
            {
                this.Response.Exceptions.Add(nameof(IUploadBase64Image), new[] { (await exception).Message.ErrorMessage });
            }
        }

        /// <summary>
        /// Uploads the identification cards.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        private async Task UploadIdentificationCards(CancellationToken cancellationToken)
        {
            foreach (var idCard in this.Request.AddIdentificationCards.Where(x => !string.IsNullOrEmpty(x.Photo)))
            {
                if (string.IsNullOrEmpty(idCard.Photo))
                {
                    continue;
                }

                var (request, exception) = await this._uploadFileClient.GetResponse<IFileDTO, IConsumerException>(
                    new
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
                }
            }
        }
    }
}