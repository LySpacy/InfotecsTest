using InfotecsTest.API;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.ConfigureServices();

var app = builder.Build();

// Конфигурация пайплайна
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Подключаем Pipeline
app.ConfigurePipeline();

// Запуск приложения
app.Run();