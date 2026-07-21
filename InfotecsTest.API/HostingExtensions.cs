using InfotecsTest.Application;

namespace InfotecsTest.API
{
    internal static class HostingExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Добавляем контроллеры
            builder.Services.AddControllers();

            // Подключаем Swagger
            builder.Services.AddCustomSwagger();

            //Подключение слоев
            builder.Services.RegisterApplicationLayer();
            builder.Services.RegisterInfrustractionLayer(builder.Configuration);

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseRouting();

            // Подключаем Swagger UI
            app.UseCustomSwagger();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            // Маршрутизация контроллеров
            app.MapControllers();

            //Миграции бд
            app.ApplyMigrations();

            app.MapGet("/", () =>
             Results.Redirect("/swagger"));

            return app;
        }
    }
}
