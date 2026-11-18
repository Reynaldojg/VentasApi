using System;
using VentasApi.Operations;

namespace VentasApi.Repository.IRepository;

// Interfaz (contrato) del repositorio de auditoría.
// Solo tiene Add, GetAll y GetById: un historial de auditoría no debería
// editarse ni borrarse, por eso no hereda de IRepositoryBase.
public interface IAuditiEntityRepository
{
        Task <OperationResult<AuditiEntity>> Add(AuditiEntity entity);
        Task<OperationResult<List<AuditiEntity>>> GetAll();
        Task <OperationResult<AuditiEntity>> GetById(int id);
}
