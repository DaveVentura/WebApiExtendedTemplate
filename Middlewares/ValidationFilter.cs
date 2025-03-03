using DaveVentura.WebApiExtendedTemplate.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DaveVentura.WebApiExtendedTemplate.Middlewares {
    public class ValidationFilter : IAsyncActionFilter {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
            if (!context.ModelState.IsValid) {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage)).ToArray();
                var errorResponse = new ErrorResponse();
                var errors = new List<string>();
                {
                    foreach (var error in errorsInModelState) {

                        foreach (string subError in error.Value ?? []) {
                            string errorMessage = $"Error in Field:{error.Key}: {subError}";
                            errors.Add(errorMessage);
                        }
                    }
                    errorResponse.Errors = errors;
                    context.Result = new BadRequestObjectResult(errorResponse);
                    return;
                }
            }
            await next();
        }
    }
}
