using System;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

public class ProductoRepository : IProductoRepository
{
    private readonly DBContext _DB;
    public ProductoRepository(DBContext dB)
    {
        _DB = dB;
    }
    public Task<OperationResult<ICollection<Producto>>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Producto>> GetById(int id)
    {
        throw new NotImplementedException();
    }
    public Task<OperationResult<Producto>> Add(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Producto>> Update(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<Producto>> Delete(Producto entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExisteNombre(string nombre)
    {
        throw new NotImplementedException();
    }

    public Task<OperationResult<bool>> SaveChanges()
    {
        throw new NotImplementedException();
    }

}
