using System;
using VentasApi.Data;
using VentasApi.Models;
using VentasApi.Repository.IRepository.IRepositoryBase;

namespace VentasApi.Repository.IRepository;

// Interfaz específica para Categoria. Hereda todos los métodos CRUD de
// IRepositoryBase<Categoria>, así que no hace falta volver a escribirlos.
// Si algún día necesitas algo propio de categorías (ej. ExisteNombre(string nombre)),
// lo agregas dentro de estas llaves.
public interface ICategoriaRepository : IRepositoryBase<Categoria>
{
    Task<bool> ExisteNombre(string nombre);
}
