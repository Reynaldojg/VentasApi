using System;
using VentasApi.Operations;

namespace VentasApi.Repository.IRepository;

// Sin Update ni Delete: el historial no se modifica.
public interface IAuditiEntityRepository
{
        Task <OperationResult<AuditiEntity>> Add(AuditiEntity entity);
        Task<OperationResult<List<AuditiEntity>>> GetAll();
        Task <OperationResult<AuditiEntity>> GetById(int id);
}
