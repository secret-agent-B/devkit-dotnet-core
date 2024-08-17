namespace Devkit.Auth;

using Devkit.Metrics.Extensions;
using Devkit.WebAPI.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

public class Program
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
