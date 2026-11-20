using VentasApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Registrar servicios (los métodos están en DependencyInjection.cs).
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepository();
builder.Services.AddServices();

builder.Services.AddControllers();   // habilita los controladores
builder.Services.AddOpenApi();       // genera la documentación de los endpoints

var app = builder.Build();

// 2) Pipeline: pasos por los que pasa cada petición, en este orden.
if (app.Environment.IsDevelopment())
{
    // Documentación y pantalla de pruebas (Scalar), solo en desarrollo.
    app.MapOpenApi();
    app.MapScalarApiReference();

    // Si entras a la raíz (http://localhost:5223/), te manda directo a Scalar.
    app.MapGet("/", () => Results.Redirect("/scalar/v1")).ExcludeFromDescription();
}

app.UseHttpsRedirection();   // http -> https
app.UseAuthorization();      // revisa [Authorize] (cuando haya login)
app.MapControllers();        // conecta las rutas [Route]/[HttpGet] con las URLs

app.Run();
