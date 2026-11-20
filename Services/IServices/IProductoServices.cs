using System;
using VentasApi.Models.DTOS.ProductoDTO;
using VentasApi.Operations;

namespace VentasApi.Services.IServices;

public interface IProductoServices
{
    Task<OperationResult<ICollection<ProductoDTO>>> GetAll();
    Task<OperationResult<ProductoDTO>> GetById(int id);
    Task<OperationResult<ProductoDTO>> Add(CrearProductoDTO dto);
    Task<OperationResult<ProductoDTO>> Update(int id, UpdateProductoDTO update);
    Task<OperationResult<ProductoDTO>> Delete(int id);
}
