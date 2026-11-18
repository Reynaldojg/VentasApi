using System;

namespace VentasApi.Models.DTOS.CategoriaDTO;

// DTO = Data Transfer Object (objeto de transferencia de datos).
// Es lo que la API DEVUELVE al cliente. Sirve para no exponer la entidad completa
// (por ejemplo, aquí ocultamos FechaCreacion y FechaModificacion).
public class CategoriaDTO
{
    public int ID {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string? Descripcion {get; set;}
}
