using System;
using System.Text.Json;
using VentasApi.Mapper;
using VentasApi.Models;
using VentasApi.Models.DTOS.ProductoDTO;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;
using VentasApi.Services.IServices;

namespace VentasApi.Services;

public class ProductoServices : IProductoServices
{
    private readonly IProductoRepository _ProducRp;
    private readonly IAuditiEntityRepository _AuditiRp;
    private readonly ICategoriaRepository _CategoriaRp;

    public ProductoServices(ICategoriaRepository categoriaRP, IAuditiEntityRepository AuditRp, IProductoRepository producRp)
    {
        _ProducRp = producRp;
        _CategoriaRp = categoriaRP;
        _AuditiRp = AuditRp;

    }
    public async Task<OperationResult<ProductoDTO>> Add(CrearProductoDTO dto)
    {
        if(await _ProducRp.ExisteNombre(dto.Nombre))
            return OperationResult<ProductoDTO>.Failure("Ya existe un producto con ese nombre");
        
        var categoria = await _CategoriaRp.GetById(dto.CategoriaId);
        if(!categoria.IsSuccess)
            return OperationResult<ProductoDTO>.Failure($"No existe la categoría con id {dto.CategoriaId}");
        
        var producto = dto.ToEntity();
        // Mejora: asignamos la categoría que ya trajimos al validar, así CategoriaNombre
        // no sale vacío en la respuesta ni en la auditoría. No se duplica: EF ya la conoce.
        producto.Categoria = categoria.Data;
        var resultado = await _ProducRp.Add(producto);

        if(!resultado.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(resultado.Message);

        await RegistrarAuditoria(producto.ID, "Agregar", null, JsonSerializer.Serialize(producto.toDTO()));
        
        return OperationResult<ProductoDTO>.Success(resultado.Data!.toDTO());
    }

    public async Task<OperationResult<ProductoDTO>> Delete(int id)
    {
        var existe = await _ProducRp.GetById(id);
        if(!existe.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(existe.Message);
        
        var producto = existe.Data!;
        var valorAnterior = JsonSerializer.Serialize(producto.toDTO());

        var resultado = await _ProducRp.Delete(producto);
        if(!resultado.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(resultado.Message);

        await RegistrarAuditoria(producto.ID, "Eliminar", valorAnterior, null);

        return OperationResult<ProductoDTO>.Success(resultado.Data!.toDTO());
    }

    public async Task<OperationResult<ICollection<ProductoDTO>>> GetAll()
    {
        var resultado = await _ProducRp.GetAll();

        if(!resultado.IsSuccess)
            return OperationResult<ICollection<ProductoDTO>>.Failure(resultado.Message);
        
        return OperationResult<ICollection<ProductoDTO>>.Success(resultado.Data!.ToDTOListLINQ());
    }

    public async Task<OperationResult<ProductoDTO>> GetById(int id)
    {
        var resultado = await _ProducRp.GetById(id);

        if(!resultado.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(resultado.Message);
        
        return OperationResult<ProductoDTO>.Success(resultado.Data!.toDTO());
    }

    public async Task<OperationResult<ProductoDTO>> Update(int id, UpdateProductoDTO update)
    {
        var existe = await _ProducRp.GetById(id);
        if(!existe.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(existe.Message);

        // Mejora: evita renombrar el producto con el nombre de otro que ya existe.
        // Solo se revisa si el nombre cambió (si no, se encontraría a sí mismo).
        if(existe.Data!.Nombre != update.Nombre && await _ProducRp.ExisteNombre(update.Nombre))
            return OperationResult<ProductoDTO>.Failure("Ya existe un producto con ese nombre");

        var categoria = await _CategoriaRp.GetById(update.CategoriaId);
        if(!categoria.IsSuccess)
            return OperationResult<ProductoDTO>.Failure($"No existe la categoría con id {update.CategoriaId}");
        
        var producto = existe.Data!;
        var valorAnterior = JsonSerializer.Serialize(producto.toDTO());

        producto.UpdateEntity(update);
        // Mejora: si cambiaron de categoría, ponemos la nueva para que CategoriaNombre salga actualizado.
        producto.Categoria = categoria.Data;

        var resultado = await _ProducRp.Update(producto);
        if(!resultado.IsSuccess)
            return OperationResult<ProductoDTO>.Failure(resultado.Message);
        
        await RegistrarAuditoria(producto.ID, "Actualizar", valorAnterior, JsonSerializer.Serialize(producto.toDTO()));

        return OperationResult<ProductoDTO>.Success(resultado.Data!.toDTO());
    }

    private async Task RegistrarAuditoria(int entityId, string Operation, string? oldValues, string? newValues)
    {
        await _AuditiRp.Add(new AuditiEntity
        {
           EntityName = "Producto",
           EntityId = entityId,
           OperationType = Operation,
           OldValues = oldValues,
           NewValues = newValues 
        });
    }

    
}
