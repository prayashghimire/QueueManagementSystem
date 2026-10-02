using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QueueMS.Appilication.Interfaces;
using QueueMS.Appilication.JwtSetting;
using QueueMS.Appilication.Service;
using QueueMS.Domain.Models.ServiceModel;
using QueueMS.Domain.Models.UserModels;
using QueueMS.Infrastructure.DatabaseContext;
using QueueMS.Infrastructure.Repositories;
using System.Text;

namespace QueueMS.Infrastructure;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICounterRepository, CounterRepository>();
        services.AddScoped<IQueueRepository, QueueRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IServiceManager, ServiceManager>();
        services.AddScoped<IRolesManager,  RolesManager>();
        services.AddScoped<ICounterStaffRepository, CounterStaffRepository>();

        services.AddDbContext<QueueMSDatabaseContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging());

       

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        services.AddIdentity<User, IdentityRole<int>>()
            .AddEntityFrameworkStores<QueueMSDatabaseContext>()
            .AddDefaultTokenProviders();

        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]!))
                };
            });

        services.AddAuthorization();

        return services;
    }
}
