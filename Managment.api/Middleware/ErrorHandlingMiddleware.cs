using Managment.api.Models;
using System.Net;

namespace Managment.api.Middleware
{
    /// <summary>
    /// Clase que representa un middleware para manejar errores en la aplicación.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Inicializa una nueva instancia de la clase ErrorHandlingMiddleware.
        /// </summary>
        /// <param name="next">El siguiente middleware en la cadena de ejecución.</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Método que invoca el middleware para manejar errores.
        /// </summary>
        /// <param name="context">El contexto HTTP actual.</param>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Método privado que maneja una excepción y devuelve una respuesta HTTP con el error.
        /// </summary>
        /// <param name="context">El contexto HTTP actual.</param>
        /// <param name="exception">La excepción que se produjo.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Aquí puedes añadir cualquier lógica personalizada para diferentes tipos de excepciones
            var statusCode = HttpStatusCode.InternalServerError; // 500 si error no manejado

            // Log de la excepción, por ejemplo: usando ILogger

            ApiResponse<string?> result = new()
            {
                Success = false,
                Data = exception.StackTrace,
                Message = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
        }
    }
}
