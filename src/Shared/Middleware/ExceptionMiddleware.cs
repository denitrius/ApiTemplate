using FluentValidation;
using static System.Net.HttpStatusCode;

namespace ApiTemplate.Shared.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError("[GlobalErrorHandler]: {@error}", ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception error)
    {
        var result = error switch
        {
            ValidationException ex => HandleValidationException(ex),
            BadHttpRequestException ex => HandleBadRequestException(ex),
            _ => HandleDefaultException()
        };

        static IResult HandleValidationException(ValidationException ex)
        {
            var errors = ex.Errors.GroupBy(p => p.PropertyName).ToDictionary(k => k.Key,
                v => v.Select(a => a.ErrorMessage).ToArray());

            return Results.ValidationProblem(errors: errors, statusCode: (int)BadRequest,
                title: nameof(ValidationException));
        }

        static IResult HandleBadRequestException(BadHttpRequestException ex)
        {
            return Results.Problem(detail: ex.Message, statusCode: ex.StatusCode,
                title: nameof(BadHttpRequestException));
        }

        static IResult HandleDefaultException()
        {
            return Results.Problem(detail: "An unexpected exception occurred while processing the request.",
                statusCode: (int)InternalServerError, title: "UnexpectedException");
        }

        await result.ExecuteAsync(context);
    }
}
