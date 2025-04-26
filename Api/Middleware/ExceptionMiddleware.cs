using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationException(context, ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static async Task HandleValidationException(HttpContext context, ValidationException validationException)
    {
        var validationErrors = new Dictionary<string, string[]>();

        if (validationException.Errors is not null)
            foreach (var error in validationException.Errors)
                if (validationErrors.TryGetValue(error.PropertyName, out var existingErrors))
                    validationErrors[error.PropertyName] = [.. existingErrors, error.ErrorMessage];
                else
                    validationErrors[error.PropertyName] = [error.ErrorMessage];

        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var validationProblemDetails = new ValidationProblemDetails(validationErrors)
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "ValidationFailure",
            Title = "Validation Failure",
            Detail = "One or more validation errors occurred."
        };

        await context.Response.WriteAsJsonAsync(validationProblemDetails);
    }
}