using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Acceso a la tabla "categorias". El service usa esto en vez del DBContext directamente.
public class CategoriaRepository : ICategoriaRepository
{
    private readonly DBContext _db;

    // El DBContext lo inyecta ASP.NET (registrado en DependencyInjection.cs).
    public CategoriaRepository(DBContext dBContext)
    {
        _db = dBContext;
    }

    public async Task<OperationResult<ICollection<Categoria>>> GetAll()
    {
        // AsNoTracking: EF no vigila los cambios de estos objetos. Más rápido; úsalo cuando solo vas a leer.
        var categoria = await _db.categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
        return OperationResult<ICollection<Categoria>>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> GetById(int id)
    {
        // Sin AsNoTracking a propósito: Update y Delete del service usan este mismo objeto.
        var categoria = await _db.categorias.FirstOrDefaultAsync(c => c.ID == id);
        if(categoria == null)
            return OperationResult<Categoria>.Failure("La categoria no existe");
        return OperationResult<Categoria>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> Add(Categoria categoria)
    {
        // UtcNow = hora universal, no depende de la zona horaria del servidor.
        categoria.FechaCreacion = DateTime.UtcNow;
        // AddAsync solo la deja "pendiente"; el INSERT real ocurre en SaveChanges.
        await _db.categorias.AddAsync(categoria);
        var resultado = await SaveChanges();

        if(!resultado.IsSuccess)
            return OperationResult<Categoria>.Failure("error al agregar la categoria");

        // Después de guardar, EF ya llenó categoria.ID con el que generó la BD.
        return OperationResult<Categoria>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> Update(Categoria categoria)
    {
        categoria.FechaModificacion = DateTime.UtcNow;
        // Marca la entidad como modificada -> UPDATE al guardar.
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
        // Marca la entidad para borrar -> DELETE al guardar.
        _db.categorias.Remove(categoria);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
        {
            return OperationResult<Categoria>.Failure("Error al eliminar categoria");
        }

        return OperationResult<Categoria>.Success(categoria, "Categoria eliminada");
    }

    // Envía a SQL todo lo pendiente (Add/Update/Remove) y atrapa los errores de la BD.
    public async Task<OperationResult<bool>> SaveChanges()
    {
        try
        {
            // Devuelve cuántas filas cambiaron. Si es 0, Data = false (pero IsSuccess sigue en true).
            var filasAfectadas = await _db.SaveChangesAsync();
            return OperationResult<bool>.Success(filasAfectadas > 0);
        }
        catch (DbUpdateException ex)
        {
            // SQL rechazó el cambio (FK que no existe, valor duplicado, campo obligatorio vacío...).
            // InnerException trae el mensaje real de SQL; si no hay, se usa ex.Message.
            return OperationResult<bool>.Failure($"Error al guardar cambios: {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
       // AnyAsync: true si hay al menos una categoría con ese nombre.
       return await _db.categorias.AnyAsync(c => c.Nombre == nombre);
    }
}
