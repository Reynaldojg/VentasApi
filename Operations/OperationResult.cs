using System;

namespace VentasApi.Operations;

// Clase "envoltorio" (patrón Result) para devolver el resultado de una operación
// de forma uniforme: si salió bien o no, un mensaje y los datos.
// Así evitamos lanzar excepciones para errores esperados (ej. "no existe").
// <T> es un GENÉRICO: T puede ser Categoria, List<Producto>, bool, etc.
public class OperationResult<T>
{
        // true si la operación salió bien, false si falló.
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        // Los datos devueltos (pueden ser null si hubo error).
        public T? Data { get; set; }

        // Métodos "fábrica" estáticos: se llaman sin crear un objeto antes,
        // por ejemplo: OperationResult<Categoria>.Success(categoria)
        // "message" es un parámetro opcional: si no lo pasas, usa el valor por defecto.
        public static OperationResult<T> Success(T data, string message = "Operacion existosa")
        {
            // Inicializador de objeto: crea la instancia y asigna propiedades en un solo paso.
            return new OperationResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = message,
                // default(T) = valor por defecto del tipo: null para clases, 0 para int, false para bool.
                Data = default(T)
            };
        }
}
