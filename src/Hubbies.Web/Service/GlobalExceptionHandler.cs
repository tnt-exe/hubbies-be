using Microsoft.AspNetCore.Diagnostics;

namespace Hubbies.Web.Service;

public class GlobalExceptionHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    private static readonly string[] _separator = ["\r\n", "\r", "\n"];

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var statusCode = exception switch
        {
            HttpException httpException => (int)httpException.StatusCode,
            UnauthorizedAccessException _ => StatusCodes.Status401Unauthorized,
            NotImplementedException _ => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Type = exception.GetType().Name,
            Title = "An error occurred while processing your request.",
            Status = statusCode,
            Detail = exception.Message,
            Instance = httpContext.Request.Path
        };

        if (exception is IHasErrors errorsException)
        {
            problemDetails.Extensions.Add("errors", errorsException.Errors);
        }

        if (httpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsDevelopment() ||
            httpContext.RequestServices.GetRequiredService<IHostEnvironment>().IsStaging())
        {
            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            problemDetails.Extensions.Add("stackTrace", exception.StackTrace?.Split(_separator, StringSplitOptions.None));
        }

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}
