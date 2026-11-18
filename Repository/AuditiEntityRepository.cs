using System;
using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Implementación del repositorio de auditoría.
// Un REPOSITORIO encapsula el acceso a datos: el resto de la app no habla
// directamente con el DBContext, sino con estos métodos.
public class AuditiEntityRepository : IAuditiEntityRepository
{
    // "readonly" = solo se puede asignar en el constructor.
    private readonly DBContext _bd;

    // Inyección de dependencias por constructor: ASP.NET crea el DBContext
    // y nos lo pasa automáticamente al crear este repositorio.
    public AuditiEntityRepository(DBContext dBContext)
    {
      _bd = dBContext;
    }

    // async/await: la operación con la base de datos no bloquea el hilo
    // mientras espera la respuesta de SQL Server.
    public async Task<OperationResult<AuditiEntity>> Add(AuditiEntity entity)
    {
        entity.FechaAccion = DateTime.Now;
        // AddAsync solo marca la entidad como "pendiente de insertar" en memoria...
        await _bd.AddAsync(entity);
        // ...y SaveChangesAsync es lo que realmente ejecuta el INSERT en la base de datos.
        await _bd.SaveChangesAsync();
        return OperationResult<AuditiEntity>.Success(entity);
    }

    public async Task<OperationResult<List<AuditiEntity>>> GetAll()
    {
        // LINQ: OrderBy ordena por Id, ToListAsync ejecuta la consulta (SELECT ... ORDER BY Id).
        var audit = await _bd.AuditiEntities.OrderBy(c => c.Id).ToListAsync();
        return OperationResult<List<AuditiEntity>>.Success(audit);
    }

    public async Task<OperationResult<AuditiEntity>> GetById(int id)
    {
        // FirstOrDefaultAsync devuelve el primer registro que cumpla la condición, o null si no hay.
        var audit = await _bd.AuditiEntities.FirstOrDefaultAsync(c => c.Id == id);
        if (audit == null)
            // $"..." es interpolación de cadenas: inserta el valor de {id} en el texto.
            return OperationResult<AuditiEntity>.Failure($"No se encontró el registro de auditoría con id {id}");

        return OperationResult<AuditiEntity>.Success(audit);
    }
}
