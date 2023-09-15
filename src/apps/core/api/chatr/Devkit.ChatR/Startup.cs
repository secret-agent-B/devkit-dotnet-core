// -----------------------------------------------------------------------
// <copyright file="Startup.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright � information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR
{
    using Microsoft.AspNetCore.Routing;
    using Devkit.ChatR.Configurations;
    using Devkit.ChatR.Hubs;
    using Devkit.ChatR.ServiceBus;
    using Devkit.ServiceBus.Extensions;
    using Devkit.WebAPI;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using StackExchange.Redis.Extensions.Core;
    using StackExchange.Redis.Extensions.Core.Abstractions;
    using StackExchange.Redis.Extensions.Core.Configuration;
    using StackExchange.Redis.Extensions.Core.Implementations;
    using StackExchange.Redis.Extensions.System.Text.Json;

    /// <summary>
    /// The Startup class for the FileStore API that sets up the services and DI.
    /// </summary>
    /// <seealso cref="AppStartupBase" />
    public class Startup : AppStartupBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">The configuration.</param>
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
            : base(env, configuration)
        {
            this.MediatorAssemblies.Add(typeof(Startup).Assembly);
            this.ValidationAssemblies.Add(typeof(Startup).Assembly);
        }

        /// <summary>
        /// Setup middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        protected override void CustomConfigure(IApplicationBuilder app)
        {
        }

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected override void CustomConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus<ChatRRegistry>();

            // Add configuration(s).
            services.Configure<ChatRConfiguration>(this.Configuration.GetSection(ChatRConfiguration.Section));

            this.SetupRedis(services);

            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = this.WebHostEnvironment.IsDevelopment();
            });
        }

        /// <summary>
        /// Add custom endpoints.
        /// </summary>
        /// <param name="endpointBuilder">The endpoint builder.</param>
        protected override void CustomEndpoints(IEndpointRouteBuilder endpointBuilder)
        {
            endpointBuilder.MapHub<ChatRHub>("/hubs/chatr");
        }

        /// <summary>
        /// Setups the redis.
        /// </summary>
        /// <param name="services">The services.</param>
        private void SetupRedis(IServiceCollection services)
        {
            var redisConfiguration = this.Configuration.GetSection("RedisChatR").Get<RedisConfiguration>();

            services.AddSingleton<IRedisClient, RedisClient>();
            services.AddSingleton<IRedisConnectionPoolManager, RedisConnectionPoolManager>();
            services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
            services.AddSingleton(provider => provider.GetRequiredService<IRedisClient>().GetDefaultDatabase());
            services.AddSingleton(redisConfiguration);
        }
    }
}