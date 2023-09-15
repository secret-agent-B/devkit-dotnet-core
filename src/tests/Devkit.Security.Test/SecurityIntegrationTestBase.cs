// -----------------------------------------------------------------------
// <copyright file="SecurityTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test
{
    using System;
    using AspNetCore.Identity.Mongo;
    using AspNetCore.Identity.Mongo.Mongo;
    using AspNetCore.Identity.Mongo.Stores;
    using Devkit.Security;
    using Devkit.Security.Data.Models;
    using Devkit.Security.Test.ServiceBus;
    using Devkit.ServiceBus.Interfaces;
    using Devkit.Test;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Bson;

    /// <summary>
    /// Security API test base.
    /// </summary>
    /// <typeparam name="T">The type of test input.</typeparam>
    /// <seealso cref="IntegrationTestBase{T, Startup}" />
    public abstract class SecurityIntegrationTestBase<T> : IntegrationTestBase<T, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityIntegrationTestBase{T}"/> class.
        /// </summary>
        protected SecurityIntegrationTestBase()
            : base()
        {
            Environment.SetEnvironmentVariable("GOOGLE_CLIENT_ID", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("GOOGLE_SECRET", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("FACEBOOK_CLIENT_ID", this.Faker.Random.AlphaNumeric(10));
            Environment.SetEnvironmentVariable("FACEBOOK_SECRET", this.Faker.Random.AlphaNumeric(10));

            this.WebApplicationFactory?.ConfigureTestServices(services =>
            {
                // Setup user manager and role manager for integration test overriding the default implementation.
                var databaseOptions = new MongoIdentityOptions
                {
                    ConnectionString = this.WebApplicationFactory.RepositoryConfiguration.ConnectionString
                };

                var options = new MongoIdentityOptions
                {
                    ConnectionString = databaseOptions.ConnectionString
                };

                var userCollection = MongoUtil.FromConnectionString<UserAccount>(options, databaseOptions.UsersCollection);
                var roleCollection = MongoUtil.FromConnectionString<UserRole>(options, databaseOptions.RolesCollection);

                services.AddSingleton(x => userCollection);
                services.AddSingleton(x => roleCollection);

                services.AddTransient<IUserStore<UserAccount>>(x =>
                    new UserStore<UserAccount, UserRole, ObjectId>(
                        userCollection,
                        roleCollection,
                        new IdentityErrorDescriber()));

                services.AddSingleton<IBusRegistry, TestSecurityBusRegistry>();
            });
        }
    }
}