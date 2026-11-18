using System;

namespace VentasApi.Models.DTOS.ProductoDTO;

public class ProductoDTO
{
    public int ID { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Stock { get; set; }
    public int CategoriaId { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty; // viene del Include
}
