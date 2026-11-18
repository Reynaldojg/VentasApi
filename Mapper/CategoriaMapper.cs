using System;
using VentasApi.Models;
using VentasApi.Models.DTOS.CategoriaDTO;

namespace VentasApi.Mapper;

public static class CategoriaMapper
{
    public static CategoriaDTO ToDTO(this Categoria categoria)
    {
        return new CategoriaDTO
        {
            ID = categoria.ID,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion
        };
    }

    public static Categoria ToEntity(this CrearCategoriaDTO crearCategoriaDTO)
    {
         return new Categoria
         {
             Nombre = crearCategoriaDTO.Nombre,
             Descripcion = crearCategoriaDTO.Descripcion
         };
    }

    public static void UpdateEntity(this Categoria categoria,UpdateCategoriaDTO updateCategoriaDTO)
    {
        categoria.Nombre = updateCategoriaDTO.Nombre;
        categoria.Descripcion = updateCategoriaDTO.Descripcion;
    }

    // public static List<CategoriaDTO> ToDTOList(this ICollection<Categoria> categorias)
    // {
    //     var lista = new List<CategoriaDTO>();

    //     foreach(var categoria in categorias)
    //     {
    //         lista.Add(categoria.ToDTO());
    //     }

    //     return lista;
    // }
    public static List<CategoriaDTO> ToDTOListLINQ(this ICollection<Categoria> categorias)
    {
        return categorias.Select(c => c.ToDTO()).ToList();
    }
}
