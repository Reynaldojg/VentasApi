using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.Base;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

public class ClienteRepository : RepositoryBase, IClienteRepository
{
    // El DBContext (_db) y SaveChanges vienen de RepositoryBase.
    public ClienteRepository(DBContext db) : base(db) { }

    public async Task<OperationResult<Cliente>> Add(Cliente cliente)
    {
        cliente.FechaCreacion = DateTime.UtcNow;

        await _db.Clientes.AddAsync(cliente);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Cliente>.Failure("error al agregar al cliente");
        
        return OperationResult<Cliente>.Success(cliente);
    }

    public async Task<OperationResult<Cliente>> Delete(Cliente cliente)
    {
        _db.Clientes.Remove(cliente);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Cliente>.Failure("Error al eliminar al cliente");
        
        return OperationResult<Cliente>.Success(cliente,"El cliente fue eliminado con existo");
    }

    public async Task<OperationResult<ICollection<Cliente>>> GetAll()
    {
        var cliente = await _db.Clientes.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();

        return OperationResult<ICollection<Cliente>>.Success(cliente);
    }
    public async Task<OperationResult<Cliente>> GetById(int id)
    {
       var cliente = await _db.Clientes.FirstOrDefaultAsync(c => c.ID == id);

       if(cliente == null)
            return OperationResult<Cliente>.Failure("el cliente no existe");
        
        return OperationResult<Cliente>.Success(cliente);
    }

    public async Task<OperationResult<Cliente>> Update(Cliente cliente)
    {
       cliente.FechaModificacion = DateTime.UtcNow;

       _db.Clientes.Update(cliente);
       var resultado = await SaveChanges();

       if(!resultado.IsSuccess)
            return OperationResult<Cliente>.Failure("Error al actualizar al cliente");
        
        return OperationResult<Cliente>.Success(cliente,"El cliente fue Actualizado con exito");
    }
}
