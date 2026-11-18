using System;
using VentasApi.Operations;

namespace VentasApi.Repository.IRepository.IRepositoryBase;

// Interfaz genérica con las operaciones CRUD comunes a todos los repositorios.
// "where T : class" obliga a que T sea un tipo por referencia (como una entidad).
public interface IRepositoryBase<T> where T : class
{
    Task<OperationResult<ICollection<T>>> GetAll();
    Task<OperationResult<T>> GetById(int id);
    Task<OperationResult<T>> Add(T entity);
    Task<OperationResult<T>> Update(T entity);
    Task<OperationResult<T>> Delete(T entity);
    Task<OperationResult<bool>> SaveChanges();
}
