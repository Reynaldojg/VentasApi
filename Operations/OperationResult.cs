using System;

namespace VentasApi.Operations;

// Envoltorio para devolver resultados siempre igual: ¿salió bien?, un mensaje y los datos.
// Así no se lanzan excepciones para errores esperados (ej. "no existe").
// <T> = el tipo de dato que devuelve (Categoria, List<Producto>, bool...).
public class OperationResult<T>
{
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }   // null si hubo error

        // Uso: OperationResult<Categoria>.Success(categoria)
        public static OperationResult<T> Success(T data, string message = "Operacion existosa")
        {
            return new OperationResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        // Uso: OperationResult<Categoria>.Failure("La categoria no existe")
        public static OperationResult<T> Failure(string message)
        {
            return new OperationResult<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default(T) // valor vacío del tipo: null, 0 o false
            };
        }
}
