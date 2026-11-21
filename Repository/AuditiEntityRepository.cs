using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Operations;
using VentasApi.Repository.Base;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Acceso a la tabla de auditoría (historial de cambios).
public class AuditiEntityRepository : RepositoryBase, IAuditiEntityRepository
{
    // El DBContext (_db) y SaveChanges vienen de RepositoryBase.
    public AuditiEntityRepository(DBContext dBContext) : base(dBContext) { }

    public async Task<OperationResult<AuditiEntity>> Add(AuditiEntity entity)
    {
        entity.FechaAccion = DateTime.Now;
        // AddAsync lo deja pendiente; SaveChanges (de RepositoryBase) hace el INSERT y atrapa errores.
        await _db.AddAsync(entity);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
            return OperationResult<AuditiEntity>.Failure(resultado.Message);

        return OperationResult<AuditiEntity>.Success(entity);
    }

    public async Task<OperationResult<List<AuditiEntity>>> GetAll()
    {
        var audit = await _db.AuditiEntities.OrderBy(c => c.Id).ToListAsync();
        return OperationResult<List<AuditiEntity>>.Success(audit);
    }

    public async Task<OperationResult<AuditiEntity>> GetById(int id)
    {
        // FirstOrDefaultAsync: el primer registro que cumpla la condición, o null si no hay.
        var audit = await _db.AuditiEntities.FirstOrDefaultAsync(c => c.Id == id);
        if (audit == null)
            return OperationResult<AuditiEntity>.Failure($"No se encontró el registro de auditoría con id {id}");

        return OperationResult<AuditiEntity>.Success(audit);
    }
}
