using Managment.api.Models;
using System.Net;

namespace Managment.api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

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

            //var result = new { error = exception.Message, stackTrace = exception.StackTrace }; // Personaliza esto según tus necesidades
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(result));
        }
    }
}
