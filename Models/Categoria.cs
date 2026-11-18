using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models;

// Entidad (modelo) que EF Core convierte en la tabla "categorias".
// Cada propiedad pública con get/set se convierte en una columna.
public class Categoria
{

    // Por convención, una propiedad llamada "ID" o "Id" es la clave primaria (PK)
    // y en SQL Server se genera como IDENTITY (autoincremental).
    public int ID {get; set;}

    // "string" sin "?" => columna NOT NULL.
    // "= string.Empty" evita que el valor inicial sea null (y quita la advertencia del compilador).
    public string Nombre {get; set;} = string.Empty;

    // El "?" indica que puede ser null => columna que acepta NULL.
    public string? Descripcion {get; set;}

    // Fechas de auditoría simple: cuándo se creó y cuándo se modificó por última vez.
    public DateTime? FechaCreacion {get; set;}
    public DateTime? FechaModificacion {get; set;}
}
