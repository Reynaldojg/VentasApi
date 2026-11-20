using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models;

public class Cliente
{
    public int Id {get; set;}

    [MaxLength(100)]   // limita el largo del texto
    public string Nombre {get;set;}= string.Empty;
    public string? Apellido {get; set;}

    [Phone(ErrorMessage = "El teléfono no es válido")]
    public int Cedula {get; set;}
    public int Telefono {get; set;}
    
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es válido")]   // revisa que tenga formato de email
    public string? Email {get; set;}
    public string? Direccion {get; set;}
}
