using Microsoft.Extensions.DependencyInjection;
using Application.Services.AuthenticationService;
using Application.Services.ProductService;
using Application.Services.CategoryService;

namespace Application;

public static class DependencyIjection{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}