using System;

namespace VentasApi.Operations;

// Historial de cambios (tabla "AuditiEntities"): qué se hizo, sobre qué registro y cuándo.
public class AuditiEntity
{
    public int Id { get; set; }
    public string? EntityName { get; set; }     // tabla afectada, ej. "Categoria"
    public int EntityId { get; set; }           // ID del registro afectado
    public string? OperationType { get; set; }  // "Agregar", "Actualizar" o "Eliminar"
    public DateTime FechaAccion { get; set; }

    // Cómo estaba antes y cómo quedó después, guardado como JSON.
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }

    public string? UserId { get; set; }   // quién lo hizo (para cuando haya login)
}
