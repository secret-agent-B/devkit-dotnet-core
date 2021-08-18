// -----------------------------------------------------------------------
// <copyright file="UpdateUserCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.UpdateUser
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Security.Business.ViewModels;

    /// <summary>
    /// UpdateUserCommand class is the request to update a user account and its profile.
    /// </summary>
    public class UpdateUserCommand : CommandRequestBase<UserVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommand"/> class.
        /// </summary>
        public UpdateUserCommand()
        {
            this.AddIdentificationCards = new List<IdentificationCardEditorVM>();
            this.RemoveIdentificationCardImageIds = new List<string>();
        }

        /// <summary>
        /// Gets or sets the new identification cards.
        /// </summary>
        /// <value>
        /// The add identification cards.
        /// </value>
        public List<IdentificationCardEditorVM> AddIdentificationCards { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>
        /// The name of the middle.
        /// </value>
        public string MiddleName { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the remove identification card image ids.
        /// </summary>
        /// <value>
        /// The remove identification card image ids.
        /// </value>
        public List<string> RemoveIdentificationCardImageIds { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }
    }
}