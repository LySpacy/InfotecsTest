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

public static class DependencyInjection
{
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

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        db.Database.Migrate();
    }
}