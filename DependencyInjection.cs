using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Repository;
using VentasApi.Repository.IRepository;
using VentasApi.Services;
using VentasApi.Services.IServices;

namespace VentasApi;

// Clase que agrupa el registro de dependencias para mantener Program.cs limpio.
// Los métodos son de extensión ("this IServiceCollection"), por eso en Program.cs
// se pueden llamar como builder.Services.AddRepository().
public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        // "Cuando alguien pida un ICategoriaRepository, entrégale un CategoriaRepository".
        // AddScoped = se crea UNA instancia por cada petición HTTP (igual que el DBContext).
        // Otras opciones: AddTransient (una nueva cada vez que se pide)
        // y AddSingleton (una sola para toda la vida de la app).
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IAuditiEntityRepository, AuditiEntityRepository>();
        // Devolvemos "services" para poder encadenar: builder.Services.AddRepository().AddServices();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Scoped también, porque los services usan los repositorios (que son Scoped).
        services.AddScoped<ICategoriaService, CategoriaService>();
        return services;
    }

    // Recibe la configuración como segundo parámetro porque, a diferencia de los otros
    // métodos, necesita leer appsettings.json. Se llama con builder.Services.AddDatabase(builder.Configuration).
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // Registramos el DBContext en el contenedor de inyección de dependencias (DI).
        // "ConnectionSQL" es el nombre de la cadena de conexión dentro de appsettings.json
        // (sección "ConnectionStrings"). UseSqlServer le dice a EF Core que use SQL Server.
        services.AddDbContext<DBContext>(opcion =>
            opcion.UseSqlServer(configuration.GetConnectionString("ConnectionSQL")));
        return services;
    }
}
