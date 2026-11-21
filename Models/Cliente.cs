using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models;

// Entidad: EF la convierte en la tabla "clientes".
// [MaxLength] aquí define el tamaño de la columna en SQL (nvarchar(100), etc.).
public class Cliente
{
    public int ID {get; set;}   // clave primaria, autoincremental

    [MaxLength(100)]
    public string Nombre {get; set;} = string.Empty;

    [MaxLength(100)]
    public string? Apellido {get; set;}

    // string y no int: conserva ceros iniciales y no se pasa del límite de int.
    [MaxLength(20)]
    public string Cedula {get; set;} = string.Empty;

    [MaxLength(20)]
    public string? Telefono {get; set;}

    [MaxLength(150)]
    public string Email {get; set;} = string.Empty;

    [MaxLength(200)]
    public string? Direccion {get; set;}

    public DateTime? FechaCreacion {get; set;}
    public DateTime? FechaModificacion {get; set;}
}
