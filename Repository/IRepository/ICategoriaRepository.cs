using System;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Repository.IRepository.IRepositoryBase;

namespace VentasApi.Repository.IRepository;

// Hereda el CRUD de IRepositoryBase y agrega lo propio de categorías.
public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    Task<bool> ExisteNombre(string nombre);
}
