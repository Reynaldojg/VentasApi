using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models.DTOS.ClienteDTO;

// DTO de entrada para el POST. Sin ID (lo genera la BD) ni fechas (las pone el servidor).
// Si una validación falla, [ApiController] responde 400 automáticamente.
public class CrearClienteDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [MaxLength(100)]
    public string Nombre {get; set;} = string.Empty;

    [MaxLength(100)]
    public string? Apellido {get; set;}

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [MaxLength(20)]
    public string Cedula {get; set;} = string.Empty;

    // [Phone] solo funciona con string: revisa que tenga formato de teléfono.
    [Phone(ErrorMessage = "El teléfono no es válido")]
    [MaxLength(20)]
    public string? Telefono {get; set;}

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El email no es válido")]
    [MaxLength(150)]
    public string Email {get; set;} = string.Empty;

    [MaxLength(200)]
    public string? Direccion {get; set;}
}
