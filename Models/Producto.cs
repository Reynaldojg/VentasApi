using System;

namespace VentasApi.Models;

// Entidad: EF la convierte en la tabla "productos".
public class Producto
{
    public int ID { get; set; }   // clave primaria, autoincremental
    public string Nombre { get; set; } = string.Empty;

    // decimal para dinero (double pierde precisión). El (18,2) está en DBContext.
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    // Clave foránea: guarda el ID de la categoría a la que pertenece.
    public int CategoriaId { get; set; }

    // Navegación: el objeto Categoria completo. No es columna; llega null si no usas .Include(p => p.Categoria).
    public Categoria? Categoria { get; set; }

    public DateTime? FechaCreacion {get; set;}
    public DateTime? FechaModificacion {get; set;}
}
