using System;

namespace VentasApi.Models;

// Entidad que EF Core convierte en la tabla "productos".
public class Producto
{
    // Clave primaria (por convención del nombre "ID").
    public int ID { get; set; }
    public string Nombre { get; set; } = string.Empty;

    // decimal es el tipo recomendado para dinero (double/float pierden precisión).
    // Su precisión (18,2) se configura en DBContext.OnModelCreating.
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    // --- Relación con Categoria (muchos productos pertenecen a una categoría) ---
    // CategoriaId es la CLAVE FORÁNEA (FK): guarda el ID de la categoría en la tabla.
    // EF la reconoce por convención: nombre de la navegación ("Categoria") + "Id".
    public int CategoriaId { get; set; }

    // Propiedad de NAVEGACIÓN: permite acceder al objeto Categoria completo desde el producto
    // (por ejemplo producto.Categoria.Nombre). No crea columna; EF la llena cuando
    // usas .Include(p => p.Categoria) en una consulta. Lleva "?" porque puede no estar cargada.
    public Categoria? Categoria { get; set; }

    public DateTime? FechaCreacion {get; set;}
    public DateTime? FechaModificacion {get; set;}
}
