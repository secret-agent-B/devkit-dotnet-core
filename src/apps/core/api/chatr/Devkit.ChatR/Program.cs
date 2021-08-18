// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright � information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR
{
    using Devkit.Metrics.Extensions;
    using Devkit.WebAPI.Extensions;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// The main application runtime class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Creates the host builder.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>An IHostBuilder.</returns>
        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) => config.ReferenceConfigFiles())
                .UseKestrel((context, options) => options.SetupHttps())
                .UseStartup<Startup>();

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
    }
}