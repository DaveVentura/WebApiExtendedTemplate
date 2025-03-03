using DaveVentura.WebApiExtendedTemplate.Contracts.Responses;
using DaveVentura.WebApiExtendedTemplate.Exceptions;

namespace Test.Middlewares {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = DetermineStatusCode(exception);

            List<string> errors = [];
            errors.Add($"{(exception is ApiException ? "" : $"{exception.GetType()}:")}{exception.Message}");
            if (exception.InnerException != null) {
                errors.Add($"{exception.InnerException.GetType()}:{exception.InnerException.Message}");
            }

            var errorResponse = new ErrorResponse {
                Errors = errors
            };

            return context.Response.WriteAsJsonAsync(errorResponse);
        }

        private static int DetermineStatusCode(Exception ex) => ex switch {
            ApiException aex => aex.HttpStatusCode,
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            FormatException => StatusCodes.Status412PreconditionFailed,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            TimeoutException => StatusCodes.Status504GatewayTimeout,
            _ => StatusCodes.Status500InternalServerError
        };
    }

    public static class ErrorHandlingMiddlewareExtensions {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

