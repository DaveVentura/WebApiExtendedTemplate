using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApiExtendedTemplate.Contracts.Responses;

namespace WebApiExtendedTemplate.Middlewares {
    public class ValidationMiddleware {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            var endpoint = context.GetEndpoint();
            if (endpoint == null) {
                await _next(context);
                return;
            }

            if (context.RequestServices.GetService(typeof(ModelStateDictionary)) is not ModelStateDictionary modelState || modelState.IsValid) {
                await _next(context);
                return;
            }

            var errorsInModelState = modelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage));

            var errorResponse = new ErrorResponse {
                Errors = []
            };

            foreach (var error in errorsInModelState) {
                foreach (string? subError in error.Value ?? []) {
                    string errorMessage = $"Error in Field:{error.Key}: {subError}";
                    errorResponse.Errors.Add(errorMessage);
                }
            }

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }

    public static class ValidationMiddlewareExtensions {
        public static IApplicationBuilder UseValidationMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<ValidationMiddleware>();
        }
    }
}
