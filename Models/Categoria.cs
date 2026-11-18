using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models;

// Entidad: EF la convierte en la tabla "categorias". Cada propiedad es una columna.
public class Categoria
{
    public int ID {get; set;}   // clave primaria, autoincremental

    // Sin "?" = obligatorio (NOT NULL). string.Empty evita que empiece en null.
    public string Nombre {get; set;} = string.Empty;

    // Con "?" = opcional (acepta NULL).
    public string? Descripcion {get; set;}

    public DateTime? FechaCreacion {get; set;}
    public DateTime? FechaModificacion {get; set;}
}
