using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models.DTOS.CategoriaDTO;

public class UpdateCategoriaDTO
{
    [Required]
    public string Nombre {get; set;} = string.Empty;
    public string? Descripcion {get; set;}
}
