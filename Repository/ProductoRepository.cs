using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.Base;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

public class ProductoRepository : RepositoryBase, IProductoRepository
{
    // El DBContext (_db) y SaveChanges vienen de RepositoryBase.
    public ProductoRepository(DBContext dB) : base(dB) { }

    public async Task<OperationResult<ICollection<Producto>>> GetAll()
    {
        var producto = await _db.Productos
        .AsNoTracking()
        .Include(c => c.Categoria)
        .OrderBy(c =>c.Nombre)
        .ToListAsync();

        return OperationResult<ICollection<Producto>>.Success(producto);
    }

    public async Task<OperationResult<Producto>> GetById(int id)
    {
        var producto = await _db.Productos
        .Include(c => c.Categoria)
        .FirstOrDefaultAsync(p => p.ID == id);

        if(producto == null)
            return OperationResult<Producto>.Failure("el producto no existe");
        
        return OperationResult<Producto>.Success(producto);
    }
    public async Task<OperationResult<Producto>> Add(Producto producto)
    {
        producto.FechaCreacion = DateTime.UtcNow;
        await _db.Productos.AddAsync(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto);
    }

    public async Task<OperationResult<Producto>> Update(Producto producto)
    {
        producto.FechaModificacion = DateTime.UtcNow;
        _db.Productos.Update(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto);
    }

    public async Task<OperationResult<Producto>> Delete(Producto producto)
    {
        _db.Productos.Remove(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            // Mejora: se devuelve el mensaje real de SaveChanges (antes era un texto fijo).
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto, "Producto eliminado");
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
        return await _db.Productos.AnyAsync(p => p.Nombre == nombre);
    }


}
