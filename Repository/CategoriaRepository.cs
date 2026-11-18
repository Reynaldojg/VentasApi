using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Implementación concreta de ICategoriaRepository.
// Aquí está la lógica real de acceso a la tabla "categorias".
public class CategoriaRepository : ICategoriaRepository
{
    private readonly DBContext _db;

    // El DBContext llega por inyección de dependencias (registrado en Program.cs).
    public CategoriaRepository(DBContext dBContext)
    {
        _db = dBContext;
    }

    public async Task<OperationResult<ICollection<Categoria>>> GetAll()
    {
        // AsNoTracking: EF no "vigila" estas entidades para detectar cambios.
        // Es más rápido y se recomienda en consultas de solo lectura.
        var categoria = await _db.categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
        return OperationResult<ICollection<Categoria>>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> GetById(int id)
    {
        // Aquí SÍ se hace tracking (sin AsNoTracking), útil si luego vas a
        // actualizar o eliminar la categoría que obtuviste.
        var categoria = await _db.categorias.FirstOrDefaultAsync(c => c.ID == id);
        if(categoria == null)
            return OperationResult<Categoria>.Failure("La categoria no existe");
        return OperationResult<Categoria>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> Add(Categoria categoria)
    {
        // Usamos UtcNow (hora universal) para no depender de la zona horaria del servidor.
        categoria.FechaCreacion = DateTime.UtcNow;
        // Marca la entidad como "Added" (pendiente de insertar).
        await _db.categorias.AddAsync(categoria);
        // Reutilizamos nuestro propio SaveChanges, que ya maneja los errores.
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Categoria>.Failure("error al agregar la categoria");

        // Tras guardar, EF rellena automáticamente categoria.ID con el valor generado por la BD.
        return OperationResult<Categoria>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> Update(Categoria categoria)
    {
        categoria.FechaModificacion = DateTime.UtcNow;
        // Update marca la entidad (y todas sus propiedades) como "Modified" => genera un UPDATE.
        _db.categorias.Update(categoria);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
        {
            return OperationResult<Categoria>.Failure("Error al actualizar categoria");
        }

        return OperationResult<Categoria>.Success(categoria);

    }

    public async Task<OperationResult<Categoria>> Delete(Categoria categoria)
    {
        // Remove marca la entidad como "Deleted" => genera un DELETE al guardar.
        _db.categorias.Remove(categoria);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
        {
            return OperationResult<Categoria>.Failure("Error al eliminar categoria");
        }

        return OperationResult<Categoria>.Success(categoria, "Categoria eliminada");
    }

    // Método central para guardar: todos los cambios pendientes (Add/Update/Remove)
    // se envían a la base de datos en una sola transacción.
    public async Task<OperationResult<bool>> SaveChanges()
    {
        try
        {
            // SaveChangesAsync devuelve cuántas filas se afectaron en la BD.
            var filasAfectadas = await _db.SaveChangesAsync();
            // Ojo: si no se afectó ninguna fila, IsSuccess sigue siendo true y Data es false.
            return OperationResult<bool>.Success(filasAfectadas > 0);
        }
        catch (DbUpdateException ex)
        {
            // DbUpdateException ocurre cuando SQL Server rechaza el cambio
            // (ej. clave foránea inválida, valor duplicado, columna NOT NULL vacía).
            // InnerException suele traer el mensaje real de SQL; "??" usa ex.Message si es null.
            return OperationResult<bool>.Failure($"Error al guardar cambios: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
       return await _db.categorias.AnyAsync(c => c.Nombre == nombre);
    }
}
