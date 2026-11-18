using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models.DTOS.CategoriaDTO;

// DTO que la API RECIBE cuando el cliente quiere crear una categoría (POST).
// No lleva ID porque lo genera la base de datos, ni fechas porque las pone el servidor.
public class CrearCategoriaDTO
{
    // Data Annotation de validación: si el cliente no envía "Nombre",
    // un controlador con [ApiController] responde automáticamente 400 Bad Request
    // con este mensaje de error, sin que tengas que escribir el if a mano.
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre {get; set;} = string.Empty;
    public string? Descripcion {get; set;}
}
