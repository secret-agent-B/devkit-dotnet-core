using Devkit.Auth.Configurations;
using Devkit.Auth.Data;
using Devkit.Auth.Data.Models;
using Devkit.Auth.ServiceBus;
using Devkit.ServiceBus.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Devkit.Auth;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services
            .AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);

        builder.Services.AddAuthorizationBuilder();

        // TODO: Configure this to connect to Mongo OR SQL.
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("DataSource=appdata.db"));

        builder.Services
            .AddIdentityCore<AppUser>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddApiEndpoints();

        builder.Services.AddServiceBus<AuthRegistry>();

        // Add configuration(s).
        var provider = builder.Services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>();
        builder.Services.Configure<AuthConfiguration>(configuration.GetSection(AuthConfiguration.Section));

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapIdentityApi<AppUser>();
        app.MapControllers();

        app.Run();
    }
}
