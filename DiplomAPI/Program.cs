using DiplomAPI.Context; 
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Регистрация контекста базы данных с использованием строки подключения
builder.Services.AddDbContext<PostgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Регистрация контроллеров и Swagger
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Настройки ошибок для разных сред
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Включение строгой транспортной безопасности
}
else
{
    app.UseDeveloperExceptionPage(); // Показывает страницу с ошибками в режиме разработки
}

// Настройки Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DiplomAPI v1");
    c.RoutePrefix = string.Empty; // Позволяет открыть Swagger UI на корневом URL
});

app.UseHttpsRedirection(); // Перенаправление HTTP на HTTPS
app.UseStaticFiles(); // Использование статических файлов

app.UseRouting(); // Использование маршрутизации

app.UseAuthorization(); // Авторизация

// Конфигурация маршрутизации контроллеров
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Запуск приложения