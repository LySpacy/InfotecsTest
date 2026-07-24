using InfotecsTest.Application.Interfaces.Services;
using InfotecsTest.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InfotecsTest.Application;

/// <summary>
/// Подключение зависимостей
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Регистрация сервисов слоя приложения
    /// </summary>
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ITimescaleService, TimescaleService>();

        return services;
    }
}
