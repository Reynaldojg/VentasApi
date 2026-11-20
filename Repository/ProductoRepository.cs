using System;
using Microsoft.EntityFrameworkCore;
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
    public async Task<OperationResult<ICollection<Producto>>> GetAll()
    {
        var producto = await _DB.productos
        .AsNoTracking()
        .Include(c => c.Categoria)
        .OrderBy(c =>c.Nombre)
        .ToListAsync();

        return OperationResult<ICollection<Producto>>.Success(producto);
    }

    public async Task<OperationResult<Producto>> GetById(int id)
    {
        var producto = await _DB.productos
        .Include(c => c.Categoria)
        .FirstOrDefaultAsync(p => p.ID == id);

        if(producto == null)
            return OperationResult<Producto>.Failure("el producto no existe");
        
        return OperationResult<Producto>.Success(producto);
    }
    public async Task<OperationResult<Producto>> Add(Producto producto)
    {
        producto.FechaCreacion = DateTime.UtcNow;
        await _DB.productos.AddAsync(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto);
    }

    public async Task<OperationResult<Producto>> Update(Producto producto)
    {
        producto.FechaModificacion = DateTime.UtcNow;
        _DB.productos.Update(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto);
    }

    public async Task<OperationResult<Producto>> Delete(Producto producto)
    {
        _DB.productos.Remove(producto);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            // Mejora: se devuelve el mensaje real de SaveChanges (antes era un texto fijo).
            return OperationResult<Producto>.Failure(resultado.Message);
        
        return OperationResult<Producto>.Success(producto, "Producto eliminado");
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
        return await _DB.productos.AnyAsync(p => p.Nombre == nombre);
    }

    public async Task<OperationResult<bool>> SaveChanges()
    {
        try
        {
            var filasAfectadas = await _DB.SaveChangesAsync();
            return OperationResult<bool>.Success(filasAfectadas > 0);
        }
        catch(DbUpdateException ex)
        {
            return OperationResult<bool>.Failure($"Error al guardar cambios: {ex.InnerException?.Message?? ex.Message}");
        }
    }

}
