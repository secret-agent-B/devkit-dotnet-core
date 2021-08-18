// -----------------------------------------------------------------------
// <copyright file="WebHostExtensions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.Extensions
{
    using System;
    using System.IO;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Web host setup extension methods.
    /// </summary>
    public static class WebHostExtensions
    {
        /// <summary>
        /// Reference the configs for settings and cache.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns>The configuration builder.</returns>
        public static IConfigurationBuilder ReferenceConfigFiles(this IConfigurationBuilder builder)
        {
            builder
                // APPLICATION SETTINGS
                .AddJsonFile("configs/appsettings.json", true, true)
                .AddJsonFile($"configs/appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                // DISTRIBUTED CACHE
                .AddJsonFile("configs/redis-cache.json", true, true)
                .AddJsonFile($"configs/redis-cache.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true)
                .AddEnvironmentVariables();

            return builder;
        }

        /// <summary>
        /// Setups the HTTPS.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <returns>The Kestrel server options instance.</returns>
        public static KestrelServerOptions SetupHttps(this KestrelServerOptions options)
        {
            var sslCert = Environment.GetEnvironmentVariable("SSL_CERT");

            if (!string.IsNullOrEmpty(sslCert) && File.Exists(sslCert))
            {
                options.ConfigureHttpsDefaults(httpOptions =>
                {
                    httpOptions.ServerCertificate = new X509Certificate2(sslCert, Environment.GetEnvironmentVariable("SSL_PASSWORD"));
                    options.Listen(IPAddress.Loopback, 443);
                });
            }

            return options;
        }
    }
}