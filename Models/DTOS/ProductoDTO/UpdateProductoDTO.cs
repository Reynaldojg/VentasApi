using System;
using System.ComponentModel.DataAnnotations;

namespace VentasApi.Models.DTOS.ProductoDTO;

public class UpdateProductoDTO
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Debes indicar una categoría válida")]
    public int CategoriaId { get; set; }   // solo el ID, no el objeto
}
