// -----------------------------------------------------------------------
// <copyright file="AppStartupBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI
{
    using System.Collections.Generic;
    using System.Reflection;
    using Devkit.Data.Extensions;
    using Devkit.Patterns.CQRS.Extensions;
    using Devkit.WebAPI.Extensions;
    using Devkit.WebAPI.Filters;
    using Devkit.WebAPI.ServiceRegistry;
    using MassTransit;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// The application startup base.
    /// </summary>
    public abstract class AppStartupBase
    {
        /// <summary>
        /// The API definition.
        /// </summary>
        private readonly APIDefinition _apiDefinition;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppStartupBase" /> class.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="configuration">The configuration.</param>
        protected AppStartupBase(IWebHostEnvironment env, IConfiguration configuration)
        {
            this.WebHostEnvironment = env;
            this.Configuration = configuration;

            this.MediatorAssemblies = new HashSet<Assembly>();
            this.ValidationAssemblies = new HashSet<Assembly>();

            this._apiDefinition = configuration.GetAPIDefinition();
        }

        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <value>
        /// IConfigurationRoot.
        /// </value>
        protected IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the application mediator assemblies.
        /// </summary>
        /// <value>
        /// MediatorAssemblies.
        /// </value>
        protected ICollection<Assembly> MediatorAssemblies { get; }

        /// <summary>
        /// Gets the application validation assemblies.
        /// </summary>
        /// <value>
        /// ValidationAssemblies.
        /// </value>
        protected ICollection<Assembly> ValidationAssemblies { get; }

        /// <summary>
        /// Gets the web host environment.
        /// </summary>
        /// <value>
        /// The web host environment.
        /// </value>
        protected IWebHostEnvironment WebHostEnvironment { get; }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configure(WebApplication app)
        {
            if (WebHostEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHealthChecks("/health");
            app.UseSwagger(_apiDefinition);

            CustomConfigure(app);

            app.MapControllers();
            CustomEndpoints(app);
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(PipelineFilterAttribute));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.Converters = new JsonConverter[]
                    {
                        new StringEnumConverter(new CamelCaseNamingStrategy())
                    };
                });

            services.AddHealthChecks();
            services.AddRepository();
            services.AddMediatRAssemblies(this.MediatorAssemblies);
            services.AddValidationAssemblies(this.ValidationAssemblies);
            services.AddServiceRegistry();
            services.AddSwagger(this._apiDefinition);

            this.CustomConfigureServices(services);
        }

        /// <summary>
        /// Adds the consumers.
        /// </summary>
        /// <param name="configurator">The configurator.</param>
        protected virtual void AddConsumers(IBusRegistrationConfigurator configurator)
        {
            // do nothing...
        }

        /// <summary>
        /// Setup middleware.
        /// </summary>
        /// <param name="app">The application.</param>
        protected abstract void CustomConfigure(IApplicationBuilder app);

        /// <summary>
        /// Configure middlewares.
        /// </summary>
        /// <param name="services">The services.</param>
        protected abstract void CustomConfigureServices(IServiceCollection services);

        /// <summary>
        /// Add custom endpoints.
        /// </summary>
        /// <param name="endpointBuilder">The endpoint builder.</param>
        protected virtual void CustomEndpoints(IEndpointRouteBuilder endpoints)
        {
            // do nothing...
        }
    }
}