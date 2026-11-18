using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models.DTOS.CategoriaDTO;

// DTO de entrada para el POST. Sin ID (lo genera la BD) ni fechas (las pone el servidor).
public class CrearCategoriaDTO
{
    // Si no llega, [ApiController] responde 400 con este mensaje, sin escribir ningún if.
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre {get; set;} = string.Empty;
    public string? Descripcion {get; set;}
}
