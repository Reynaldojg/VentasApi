using VentasApi;
using Scalar.AspNetCore;

// El "builder" sirve para configurar la aplicación ANTES de arrancarla:
// aquí registramos servicios, leemos appsettings.json, etc.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Registramos la base de datos, los repositorios y los services.
// Los métodos están definidos en DependencyInjection.cs
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepository();
builder.Services.AddServices();

// Habilita el uso de controladores (clases que heredan de ControllerBase).
builder.Services.AddControllers();

// Genera la documentación OpenAPI (la descripción de todos tus endpoints).
builder.Services.AddOpenApi();

// A partir de aquí la configuración de servicios queda cerrada y se construye la app.
var app = builder.Build();

// Configure the HTTP request pipeline.
// El "pipeline" es la cadena de middlewares por la que pasa cada petición, EN ORDEN.
if (app.Environment.IsDevelopment())
{
    // Solo en desarrollo exponemos el documento OpenAPI (normalmente en /openapi/v1.json).
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// Redirige automáticamente las peticiones http:// a https://
app.UseHttpsRedirection();

// Middleware de autorización (revisa atributos como [Authorize] en los controladores).
app.UseAuthorization();

// Conecta las rutas de los controladores ([Route], [HttpGet], etc.) con las URLs.
app.MapControllers();

// Arranca el servidor y se queda escuchando peticiones.
app.Run();
