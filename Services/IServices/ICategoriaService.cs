using System;
using VentasApi.Models.DTOS.CategoriaDTO;
using VentasApi.Operations;

namespace VentasApi.Services.IServices;

public interface ICategoriaService
{
    Task<OperationResult<ICollection<CategoriaDTO>>> GetAll();
    Task<OperationResult<CategoriaDTO>> GetById(int id);
    Task<OperationResult<CategoriaDTO>> Add(CrearCategoriaDTO dto);
    Task<OperationResult<CategoriaDTO>> Update(int id, UpdateCategoriaDTO update);
    Task<OperationResult<CategoriaDTO>> Delete(int id);
}
