using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Operations;

namespace VentasApi.Repository.Base;

// Código común de todos los repositorios: el DBContext y SaveChanges.
// abstract = no se puede crear directamente, solo sirve para heredar de ella.
public abstract class RepositoryBase
{
    // protected = solo esta clase y las que heredan pueden usarlo (el service no lo ve).
    protected readonly DBContext _db;

    // Cada repository le pasa el DBContext con ": base(db)" en su constructor.
    protected RepositoryBase(DBContext db)
    {
        _db = db;
    }

    // Envía a SQL todo lo pendiente (Add/Update/Remove) y atrapa los errores de la BD.
    protected async Task<OperationResult<bool>> SaveChanges()
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
}
