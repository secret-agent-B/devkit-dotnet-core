namespace Devkit.Auth;

using Devkit.Auth.Configurations;
using Devkit.Auth.ServiceBus;
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
        services.AddServiceBus<AuthRegistry>();

        // Add configuration(s).
        services.Configure<AuthConfiguration>(this.Configuration.GetSection(AuthConfiguration.Section));

        this.SetupRedis(services);

        services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = this.WebHostEnvironment.IsDevelopment();
        });
    }

    /// <summary>
    /// Setups the redis.
    /// </summary>
    /// <param name="services">The services.</param>
    private void SetupRedis(IServiceCollection services)
    {
        var redisConfiguration = this.Configuration.GetSection("RedisAuth").Get<RedisConfiguration>();

        services.AddSingleton<IRedisClient, RedisClient>();
        services.AddSingleton<IRedisConnectionPoolManager, RedisConnectionPoolManager>();
        services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
        services.AddSingleton(provider => provider.GetRequiredService<IRedisClient>().GetDefaultDatabase());
        services.AddSingleton(redisConfiguration);
    }
}