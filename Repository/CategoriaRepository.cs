using Microsoft.EntityFrameworkCore;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Operations;
using VentasApi.Repository.Base;
using VentasApi.Repository.IRepository;

namespace VentasApi.Repository;

// Acceso a la tabla "categorias". El service usa esto en vez del DBContext directamente.
public class CategoriaRepository : RepositoryBase, ICategoriaRepository
{
    // ": base(dBContext)" le pasa el DBContext a RepositoryBase, que lo guarda en _db.
    public CategoriaRepository(DBContext dBContext) : base(dBContext) { }

    public async Task<OperationResult<ICollection<Categoria>>> GetAll()
    {
        // AsNoTracking: EF no vigila los cambios de estos objetos. Más rápido; úsalo cuando solo vas a leer.
        var categoria = await _db.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
        return OperationResult<ICollection<Categoria>>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> GetById(int id)
    {
        // Sin AsNoTracking a propósito: Update y Delete del service usan este mismo objeto.
        var categoria = await _db.Categorias.FirstOrDefaultAsync(c => c.ID == id);
        if(categoria == null)
            return OperationResult<Categoria>.Failure("La categoria no existe");
        return OperationResult<Categoria>.Success(categoria);
    }

    public async Task<OperationResult<Categoria>> Add(Categoria categoria)
    {
        // UtcNow = hora universal, no depende de la zona horaria del servidor.
        categoria.FechaCreacion = DateTime.UtcNow;
        // AddAsync solo la deja "pendiente"; el INSERT real ocurre en SaveChanges.
        await _db.Categorias.AddAsync(categoria);
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
        _db.Categorias.Update(categoria);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
        {
            return OperationResult<Categoria>.Failure("Error al actualizar categoria");
        }

        return OperationResult<Categoria>.Success(categoria,"La categoria fue actualizada con existo");

    }

    public async Task<OperationResult<Categoria>> Delete(Categoria categoria)
    {
        // Marca la entidad para borrar -> DELETE al guardar.
        _db.Categorias.Remove(categoria);
        var resultado = await SaveChanges();

        if (!resultado.IsSuccess)
        {
            return OperationResult<Categoria>.Failure("Error al eliminar categoria");
        }

        return OperationResult<Categoria>.Success(categoria, "Categoria eliminada");
    }


    public async Task<bool> ExisteNombre(string nombre)
    {
       // AnyAsync: true si hay al menos una categoría con ese nombre.
       return await _db.Categorias.AnyAsync(c => c.Nombre == nombre);
    }
}
