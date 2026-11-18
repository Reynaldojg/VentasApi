using System;
using VentasApi.Operations;

namespace VentasApi.Repository.IRepository.IRepositoryBase;

// CRUD común para todos los repositorios.
public interface IRepositoryBase<T> where T : class
{
    Task<OperationResult<ICollection<T>>> GetAll();
    Task<OperationResult<T>> GetById(int id);
    Task<OperationResult<T>> Add(T entity);
    Task<OperationResult<T>> Update(T entity);
    Task<OperationResult<T>> Delete(T entity);
    Task<OperationResult<bool>> SaveChanges();
}
