using DalilAcademy.Core.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DalilAcademy.API.Middleware
{
    public class GlobalExceptionHandler(
        IProblemDetailsService problemDetails,
        ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext context, Exception exception, CancellationToken ct)
        {
            var (status, title, detail) = exception switch
            {
                ValidationException => (400, "Validation failed", (string?)null),
                PaymentFailedException => (400, "Payment failed", exception.Message),
                NotFoundException => (404, "Resource not found", exception.Message),
                ConflictException => (409, "Conflict", exception.Message),
                HttpRequestException => (503, "Payment service unavailable",
                    "The payment service is unreachable. Please try again later."),
                _ => (500, "An unexpected error occurred", null)
            };

            if (status == 500) logger.LogError(exception, "Unhandled exception");
            if (status == 503) logger.LogError(exception, "Payment gateway unreachable");

            context.Response.StatusCode = status;

            var problem = new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = detail
            };

            if (exception is ValidationException ve)
                problem.Extensions["errors"] = ve.Errors.GroupBy(e => e.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

            return await problemDetails.TryWriteAsync(new() { HttpContext = context, ProblemDetails = problem });
        }
    }
}