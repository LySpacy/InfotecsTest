using InfotecsTest.Application.Interfaces.Helpers;
using InfotecsTest.Application.Interfaces.Repositories;
using InfotecsTest.Infrustraction.DataBase.DbContexts;
using InfotecsTest.Infrustraction.DataBase.Repositories;
using InfotecsTest.Infrustraction.Parser;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InfotecsTest.Application;

/// <summary>
/// Подключение зависимостей
/// </summary>
public static class DependencyInjection
{

    /// <summary>
    /// Подключение сервисов (классов логики) слоя инфраструктуры, а так же конфигурация для различный сервисов работающих с внешними ресурсами.
    /// </summary>
    public static IServiceCollection RegisterInfrustractionLayer(
        this IServiceCollection services, 
        IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new NullReferenceException("ConnectionString to database is null");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString, o =>
            {
                o.EnableRetryOnFailure();
            });
        });

        services.AddScoped<IFileRepository, FileRepository>();
        services.AddScoped<IValueRepository, FileValuesRepository>();
        services.AddScoped<IResultRepository, FileResultRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICsvParser, CsvParser>();


        return services;
    }
}

/// <summary>
/// Подключение миграции
/// </summary>
public static class MigrationExtensions
{

    /// <summary>
    /// Автоматическое проведление миграций, при сборке проекта
    /// </summary>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Database.Migrate();
    }
}
