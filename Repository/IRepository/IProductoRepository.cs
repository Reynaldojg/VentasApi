using System;
using VentasApi.Models;
using VentasApi.Repository.IRepository.IRepositoryBase;

namespace VentasApi.Repository.IRepository;

public interface IProductoRepository : IRepositoryBase<Producto>
{
    Task<bool> ExisteNombre(string nombre);
}
