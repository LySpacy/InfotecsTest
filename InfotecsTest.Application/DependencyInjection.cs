using InfotecsTest.Application.Interfaces.Services;
using InfotecsTest.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfotecsTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ITimescaleService, TimescaleService>();

        return services;
    }
}