// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright � information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API
{
    using Devkit.WebAPI.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Serilog;

    /// <summary>
    /// The main application runtime class.
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // --- Configuration ---
            builder.Configuration.ReferenceConfigFiles();

            // --- Serilog ---
            builder.Host.UseSerilog((ctx, services, cfg) =>
            {
                cfg.ReadFrom.Configuration(ctx.Configuration)
                   .ReadFrom.Services(services);
            });

            // --- Kestrel / HTTPS ---
            builder.WebHost.ConfigureKestrel((ctx, kestrel) =>
            {
                kestrel.SetupHttps();
            });

            // --- Create Startup ---
            var startup = new Startup(builder.Environment, builder.Configuration);

            // --- Register services ---
            startup.ConfigureServices(builder.Services);

            var app = builder.Build();

            // --- Configure pipeline ---
            startup.Configure(app);

            app.Run();
        }
    }
}