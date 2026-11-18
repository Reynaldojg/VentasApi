using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Models;
using VentasApi.Operations;

namespace VentasApi.Data;

//aqui configuramos la conexion y la creacion de la Bdd
// DbContext es la clase de Entity Framework Core que representa una "sesión" con la base de datos.
// A través de ella consultamos, insertamos, actualizamos y eliminamos datos.
public class DBContext : DbContext
{
    // Las opciones (cadena de conexión, proveedor SQL Server, etc.) llegan desde Program.cs
    // gracias a AddDbContext, y se las pasamos a la clase base con ": base(options)".
    public DBContext(DbContextOptions<DBContext> options) : base(options){}

    // Cada DbSet<T> representa una TABLA en la base de datos.
    // Cada objeto T dentro del DbSet representa una FILA de esa tabla.
    public DbSet<Categoria> categorias {get; set;}
    public DbSet<Producto> productos {get; set;}
    public DbSet<AuditiEntity> AuditiEntities {get; set;}

    // Aquí se usa la "Fluent API": configuración de tablas/columnas escrita en código,
    // como alternativa (o complemento) a los atributos tipo [Required], [MaxLength], etc.
    protected override void OnModelCreating(ModelBuilder modelBuilder)  // método donde configuras las tablas
    {
        modelBuilder.Entity<Producto>()   // "configura la tabla Producto"
        .Property(p => p.Precio)      // "específicamente la columna Precio"
        .HasPrecision(18, 2);         // "que sea decimal(18,2)"
        // decimal(18,2) = hasta 18 dígitos en total, de los cuales 2 son decimales.
        // Sin esto EF muestra una advertencia porque no sabe qué precisión usar.
    }
}
