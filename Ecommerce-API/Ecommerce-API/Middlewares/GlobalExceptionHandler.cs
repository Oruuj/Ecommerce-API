using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_API.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            switch (exception)
            {
                case ArgumentNullException argNullEx:
                    problemDetails.Title = "Missing Argument";
                    problemDetails.Status = StatusCodes.Status400BadRequest;
                    problemDetails.Detail = argNullEx.Message;
                    break;

                case UnauthorizedAccessException unauthorizedEx:
                    problemDetails.Title = "Unauthorized";
                    problemDetails.Status = StatusCodes.Status401Unauthorized;
                    problemDetails.Detail = unauthorizedEx.Message;
                    break;

                case InvalidOperationException invalidOpEx:
                    problemDetails.Title = "Invalid Operation";
                    problemDetails.Status = StatusCodes.Status409Conflict;
                    problemDetails.Detail = invalidOpEx.Message;
                    break;

                case KeyNotFoundException notFoundEx:
                    problemDetails.Title = "Resource Not Found";
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Detail = notFoundEx.Message;
                    break;


                default:
                    problemDetails.Title = "An unexpected error occurred";
                    problemDetails.Status = StatusCodes.Status500InternalServerError;
                    problemDetails.Detail = exception.Message;
                    break;
            }
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
