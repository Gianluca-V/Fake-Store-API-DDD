using Api;
using Api.Common.Middleware;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddEndpointsApiExplorer();
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Cookie,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });
    }
}
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    if (builder.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
}
