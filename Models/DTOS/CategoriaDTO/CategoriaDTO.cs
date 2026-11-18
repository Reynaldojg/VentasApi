using System;

namespace VentasApi.Models.DTOS.CategoriaDTO;

// DTO de salida: lo que la API devuelve. No expone las fechas de la entidad.
public class CategoriaDTO
{
    public int ID {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string? Descripcion {get; set;}
}
