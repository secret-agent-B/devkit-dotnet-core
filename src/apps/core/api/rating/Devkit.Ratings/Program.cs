// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright � information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings
{
    using Devkit.Metrics.Extensions;
    using Devkit.WebAPI.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    /// <summary>
    /// The runtime class for this API.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.ReferenceConfigFiles();
            builder.WebHost.ConfigureKestrel(options => options.SetupHttps());
            
            // Configure Serilog
            builder.Host.ConfigureSerilog();
            
            // Configure services using Startup
            var startup = new Startup(builder.Environment, builder.Configuration);
            startup.ConfigureServices(builder.Services);
            
            var app = builder.Build();
            
            // Configure middleware
            startup.Configure(app);
            
            app.Run();
        }
    }
}