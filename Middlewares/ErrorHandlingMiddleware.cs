//#if(useAzureTable)
using Azure;
//#endif
using System.Text.Json;
using WebApiExtendedTemplate.Contracts.Responses;
using WebApiExtendedTemplate.Exceptions;

namespace WebApiExtendedTemplate.Middlewares {
    public class ErrorHandlingMiddleware {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger, IWebHostEnvironment env) {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            int statusCode = DetermineStatusCode(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            string traceId = context.TraceIdentifier;
            var errors = ExtractErrorMessages(exception);

            var errorResponse = new ErrorResponse {
                StatusCode = statusCode,
                Message = exception.Message,
                Errors = errors,
                TraceId = traceId,
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            _logger.LogError(exception, "Error: {Message} | TraceId: {TraceId}", exception.Message, traceId);

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
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
            //#if(useAzureTable)
            RequestFailedException rfex => rfex.Status,
            //#endif
            _ => StatusCodes.Status500InternalServerError
        };

        private static List<string> ExtractErrorMessages(Exception exception) {
            var errors = new List<string> { $"{exception.GetType().Name}: {exception.Message}" };

            if (exception.InnerException != null) {
                errors.Add($"{exception.InnerException.GetType().Name}: {exception.InnerException.Message}");
            }

            return errors;
        }
    }
}
