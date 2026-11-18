using System;

namespace VentasApi.Operations;

// Entidad de AUDITORÍA: guarda un historial de qué se hizo, sobre qué registro,
// cuándo y quién lo hizo. Se convierte en la tabla "AuditiEntities".
public class AuditiEntity
{
    public int Id { get; set; }

    // Nombre de la entidad afectada, por ejemplo "Categoria" o "Producto".
    public string? EntityName { get; set; }

    // ID del registro afectado dentro de esa entidad.
    public int EntityId { get; set; }

    // Tipo de operación: por ejemplo "Crear", "Actualizar" o "Eliminar".
    public string? OperationType { get; set; }

    // Momento en que ocurrió la acción.
    public DateTime FechaAccion { get; set; }

    // Valores antes y después del cambio (normalmente guardados como texto JSON).
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    // Usuario que realizó la acción (útil cuando haya autenticación).
    public string? UserId { get; set; }
}
