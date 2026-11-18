using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Acceso a la tabla de auditoría (historial de cambios).
public class AuditiEntityRepository : IAuditiEntityRepository
{
    private readonly DBContext _bd;

    public AuditiEntityRepository(DBContext dBContext)
    {
      _bd = dBContext;
    }

    public async Task<OperationResult<AuditiEntity>> Add(AuditiEntity entity)
    {
        entity.FechaAccion = DateTime.Now;
        // AddAsync lo deja pendiente; SaveChangesAsync hace el INSERT de verdad.
        await _bd.AddAsync(entity);
        await _bd.SaveChangesAsync();
        return OperationResult<AuditiEntity>.Success(entity);
    }

    public async Task<OperationResult<List<AuditiEntity>>> GetAll()
    {
        var audit = await _bd.AuditiEntities.OrderBy(c => c.Id).ToListAsync();
        return OperationResult<List<AuditiEntity>>.Success(audit);
    }

    public async Task<OperationResult<AuditiEntity>> GetById(int id)
    {
        // FirstOrDefaultAsync: el primer registro que cumpla la condición, o null si no hay.
        var audit = await _bd.AuditiEntities.FirstOrDefaultAsync(c => c.Id == id);
        if (audit == null)
            return OperationResult<AuditiEntity>.Failure($"No se encontró el registro de auditoría con id {id}");

        return OperationResult<AuditiEntity>.Success(audit);
    }
}
