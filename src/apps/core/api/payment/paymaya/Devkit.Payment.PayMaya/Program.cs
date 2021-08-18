// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.PayMaya
{
    using Devkit.Payment.PayMaya.ServiceBus;
    using Devkit.ServiceBus.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// The application runtime class.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddServiceBus<PayMayaBusRegistry>();
                    services.AddHostedService<PayMayaService>();
                });
        }
    }
}