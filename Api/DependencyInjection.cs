using Microsoft.Extensions.DependencyInjection;
using Application.Services.AuthenticationService;
using Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Api.Common.Middleware;

namespace Api;

public static class DependencyIjection{
    public static IServiceCollection AddPresentation(this IServiceCollection services){
        services.AddControllers();
        services.AddTransient<ErrorHandlingMiddleware>();
        services.AddMappings();
        return services;
    }
}