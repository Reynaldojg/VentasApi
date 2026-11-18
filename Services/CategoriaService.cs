using System;
using System.Text.Json;
using VentasApi.Mapper;
using VentasApi.Models.DTOS.CategoriaDTO;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;
using VentasApi.Services.IServices;

namespace VentasApi.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _CategoriaRp;
    private readonly IAuditiEntityRepository _AudiiRp;

    public CategoriaService(ICategoriaRepository categoriaRp, IAuditiEntityRepository AuditRp)
    {
        _CategoriaRp = categoriaRp;
        _AudiiRp = AuditRp;
    }
    public async Task<OperationResult<CategoriaDTO>> Add(CrearCategoriaDTO dto)
    {
       if(await _CategoriaRp.ExisteNombre(dto.Nombre))
            return OperationResult<CategoriaDTO>.Failure("Ya existe una categoria con ese nombre");
       
        var categoria = dto.ToEntity();
        var resultado = await _CategoriaRp.Add(categoria);

        if(!resultado.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(resultado.Message);
        
        await RegistrarAuditoria(categoria.ID, "Agregar", null, JsonSerializer.Serialize(categoria.ToDTO()));

        return OperationResult<CategoriaDTO>.Success(resultado.Data!.ToDTO());
    }

    public async Task<OperationResult<CategoriaDTO>> Delete(int id)
    {
       var existe = await _CategoriaRp.GetById(id);
        if(!existe.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(existe.Message);
        
        // tomamos la categoría que ya existe y los borramos
        var categoria = existe.Data!;
        var valorAnterior = JsonSerializer.Serialize(categoria.ToDTO());//guarda la informacion vieja

        var resultado = await _CategoriaRp.Delete(categoria);
        if(!resultado.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(resultado.Message);
        
        await RegistrarAuditoria(categoria.ID, "Eliminar", valorAnterior, null);

        return OperationResult<CategoriaDTO>.Success(resultado.Data!.ToDTO());

    }

    public async Task<OperationResult<ICollection<CategoriaDTO>>> GetAll()
    {
        var resultado =  await _CategoriaRp.GetAll();

        if(!resultado.IsSuccess)
            return OperationResult<ICollection<CategoriaDTO>>.Failure(resultado.Message);

        return OperationResult<ICollection<CategoriaDTO>>.Success(resultado.Data!.ToDTOListLINQ());

    }

    public async Task<OperationResult<CategoriaDTO>> GetById(int id)
    {
        //1. Pedirle la entidad al repository
        var resultado = await _CategoriaRp.GetById(id);

        //2. Si fallo, propagar el error(mismo mensaje)
        if(!resultado.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(resultado.Message);
        
        //3. Convertir entidad => DTO y devolverlo
        return OperationResult<CategoriaDTO>.Success(resultado.Data!.ToDTO());
    }

    public async Task<OperationResult<CategoriaDTO>> Update(int id, UpdateCategoriaDTO update)
    {   
        var existe = await _CategoriaRp.GetById(id);
        if(!existe.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(existe.Message);

        // tomamos la categoría que ya existe y le aplicamos los datos nuevos
        var categoria = existe.Data!;
        var valorAnterior = JsonSerializer.Serialize(categoria.ToDTO());//guarda la informacion vieja

        categoria.UpdateEntity(update);

        var resultado = await _CategoriaRp.Update(categoria);
        if(!resultado.IsSuccess)
            return OperationResult<CategoriaDTO>.Failure(resultado.Message);
        
        await RegistrarAuditoria(categoria.ID, "Actualizar", valorAnterior, JsonSerializer.Serialize(categoria.ToDTO()));

        return OperationResult<CategoriaDTO>.Success(resultado.Data!.ToDTO());
    }

    private async Task RegistrarAuditoria(int entityId, string operacion, string? oldValues, string? newValues)
{
    await _AudiiRp.Add(new AuditiEntity
    {
        EntityName = "Categoria",
        EntityId = entityId,
        OperationType = operacion,
        OldValues = oldValues,
        NewValues = newValues
    });
}

}
