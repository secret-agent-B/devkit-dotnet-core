// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright � information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Gateway
{
    using System;
    using System.IO;
    using Devkit.Gateway.Extensions;
    using Devkit.Metrics.Extensions;
    using Devkit.WebAPI.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// The program runtime class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureSerilog()
                .Build()
                .Run();
        }

        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>An IHostBuilder.</returns>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var ocelotConfigPath = Environment.GetEnvironmentVariable("OCELOT_CONFIG_PATH");
                    var gatewayType = Environment.GetEnvironmentVariable("DEVKIT_GATEWAY_TYPE");
                    var ocelotConfigs = Path.Combine(ocelotConfigPath, gatewayType);

                    config
                        .ReferenceConfigFiles()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddOcelot(ocelotConfigs, gatewayType, context.HostingEnvironment.EnvironmentName);
                })
                .UseKestrel((context, options) => options.SetupHttps())
                .UseStartup<Startup>();
    }
}