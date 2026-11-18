using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Repository;
using VentasApi.Repository.IRepository;
using VentasApi.Services;
using VentasApi.Services.IServices;

namespace VentasApi;

// Aquí se registran las clases que ASP.NET inyecta en los constructores.
// Se llaman desde Program.cs (ej. builder.Services.AddRepository()).
public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        // "Cuando alguien pida ICategoriaRepository, dale un CategoriaRepository".
        // AddScoped = una instancia nueva por cada petición HTTP.
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProductoRepository, ProductoRepository>();
        services.AddScoped<IAuditiEntityRepository, AuditiEntityRepository>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoriaService, CategoriaService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // Conecta EF Core con SQL Server usando la cadena "ConnectionSQL" de appsettings.json.
        services.AddDbContext<DBContext>(opcion =>
            opcion.UseSqlServer(configuration.GetConnectionString("ConnectionSQL")));
        return services;
    }
}
