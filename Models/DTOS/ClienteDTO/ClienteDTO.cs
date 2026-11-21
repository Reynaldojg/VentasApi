using System;

namespace VentasApi.Models.DTOS.ClienteDTO;

// DTO de salida: lo que la API devuelve. Sin validaciones porque no recibe datos.
public class ClienteDTO
{
    public int ID {get; set;}
    public string Nombre {get; set;} = string.Empty;
    public string? Apellido {get; set;}
    public string Cedula {get; set;} = string.Empty;
    public string? Telefono {get; set;}
    public string Email {get; set;} = string.Empty;
    public string? Direccion {get; set;}
}
