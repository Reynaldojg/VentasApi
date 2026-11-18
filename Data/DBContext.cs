using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Models;
using VentasApi.Operations;

namespace VentasApi.Data;

// Puente entre el código y la base de datos: por aquí se consulta, inserta, edita y borra.
public class DBContext : DbContext
{
    // La configuración (cadena de conexión, SQL Server) llega desde AddDatabase en DependencyInjection.cs.
    public DBContext(DbContextOptions<DBContext> options) : base(options){}

    // Cada DbSet es una tabla de la base de datos.
    public DbSet<Categoria> categorias {get; set;}
    public DbSet<Producto> productos {get; set;}
    public DbSet<AuditiEntity> AuditiEntities {get; set;}

    // Configuración extra de tablas/columnas (Fluent API). Si cambias algo aquí, haz una migración.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Precio será decimal(18,2): hasta 18 dígitos, 2 de ellos decimales.
        modelBuilder.Entity<Producto>()
        .Property(p => p.Precio)
        .HasPrecision(18, 2);
    }
}
