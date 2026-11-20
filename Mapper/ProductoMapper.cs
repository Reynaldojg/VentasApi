using System;
using VentasApi.Models;
using VentasApi.Models.DTOS.ProductoDTO;

namespace VentasApi.Mapper;

public static class ProductoMapper
{
    //ajustes en el mapper toDTO faltaba conseguir el nombre de la categoria
    public static ProductoDTO toDTO(this Producto producto)
    {
        return new ProductoDTO
        {
            ID = producto.ID,
            Nombre = producto.Nombre,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CategoriaId = producto.CategoriaId,
            CategoriaNombre = producto.Categoria?.Nombre?? string.Empty
        };
    }

    public static Producto ToEntity(this CrearProductoDTO dTO)
    {
        return new Producto
        {
            Nombre = dTO.Nombre,   
            Precio = dTO.Precio,
            Stock = dTO.Stock,
            CategoriaId =dTO.CategoriaId
        };
    }

    public static void  UpdateEntity(this Producto producto, UpdateProductoDTO dTO)
    {
        producto.Nombre = dTO.Nombre;
        producto.Precio = dTO.Precio;
        producto.Stock = dTO.Stock;
        producto.CategoriaId = dTO.CategoriaId;
    }

    public static List<ProductoDTO> ToDTOListLINQ(this ICollection<Producto> productos)
    {
        return productos.Select(p => p.toDTO()).ToList();
    }


}
