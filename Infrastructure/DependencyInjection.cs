using Application.Common.Interfaces.Authentication;
using Infrastructure.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Persistence;
using Application.Common.Interfaces.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure;

public static class DependencyIjection{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration){
        services
            .AddAuth(configuration)
            .AddPersistence();
        
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddDbContext<FakeStoreAPIDbContext>(options => options.UseSqlServer(
            "Data Source=(localdb)\\MSSQLLocalDB;" +
            "Initial Catalog=FakeStoreAPI;" +
            "Integrated Security=True;" +
            "Connect Timeout=30;" +
            "Encrypt=False;" +
            "Trust Server Certificate=False;" +
            "Application Intent=ReadWrite;" +
            "Multi Subnet Failover=False"
        ));
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
    {
        JWTSettings jwtSettings = new JWTSettings();
        configuration.Bind(JWTSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJWTTokenGenerator, JWTTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            { 
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }
}