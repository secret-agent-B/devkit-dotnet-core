// -----------------------------------------------------------------------
// <copyright file="UserVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.ViewModels
{
    using System;
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS;
    using Devkit.Security.Data.Models;

    /// <summary>
    /// Register user response.
    /// </summary>
    /// <seealso cref="ResponseBase" />
    public class UserVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVM" /> class.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        public UserVM(UserAccount userAccount)
        {
            this.UserName = userAccount.UserName;
            this.FirstName = userAccount.Profile.FirstName;
            this.MiddleName = userAccount.Profile.MiddleName;
            this.LastName = userAccount.Profile.LastName;
            this.Email = userAccount.Email;
            this.PhoneNumber = userAccount.PhoneNumber;
            this.CreatedOn = userAccount.CreatedOn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserVM"/> class.
        /// </summary>
        public UserVM()
        {
            this.IdentificationCards = new List<IdentificationCardVM>();
        }

        /// <summary>
        /// Gets or sets the address line 1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2.
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
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the identification cards.
        /// </summary>
        /// <value>
        /// The identification cards.
        /// </value>
        public IList<IdentificationCardVM> IdentificationCards { get; set; }

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
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the province.
        /// </summary>
        /// <value>
        /// The province.
        /// </value>
        public string Province { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the selfie identifier.
        /// </summary>
        /// <value>
        /// The selfie identifier.
        /// </value>
        public string SelfieId { get; set; }
    }
}