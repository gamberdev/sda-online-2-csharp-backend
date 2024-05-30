using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Middleware;

//class to handle the object of response
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public class NotFoundException : Exception
{
    public NotFoundException(string message)
        : base(message) { }
}

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(string message)
        : base(message) { }
}

public class ConflictException : Exception
{
    public ConflictException(string message)
        : base(message) { }
}

public class BadRequestException : Exception
{
    public BadRequestException(string message)
        : base(message) { }
}

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger,
        RequestDelegate next
    )
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
         $"---- Handling Request: {context.Request.Method} {context.Request.Path} ----"
     );
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Unhandled error occurred: {ex}");

            var response = new { StatusCode = StatusCodes.Status500InternalServerError, Message = "An unexpected error occurred." };

            if (ex is Npgsql.PostgresException postgresException)
            {
                if (postgresException.SqlState == "23505")
                {
                    response = new { StatusCode = StatusCodes.Status409Conflict, Message = "Duplicate Data. DataInfo already exists, try again!" };
                }
                else if (postgresException.SqlState == "XX000")
                {
                    response = new { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
                }
            }
            else if (ex is ArgumentException argEx)
            {
                response = new { StatusCode = StatusCodes.Status400BadRequest, Message = argEx.Message };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = response.StatusCode;

            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
        finally
        {
            _logger.LogInformation(
                $"---- End Handling Request: {context.Request.Method} {context.Request.Path} ----"
            );
        }
    }

    private ErrorResponse GetException(Exception ex)
    {
        return ex switch
        {
            NotFoundException notFoundException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Message = notFoundException.Message
                },
            ValidationException validationException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = validationException.Message
                },
            UnauthorizedAccessException unauthorizedAccessException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = unauthorizedAccessException.Message
                },
            ForbiddenAccessException forbiddenAccessException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Message = forbiddenAccessException.Message
                },
            ConflictException conflictException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status409Conflict,
                    Message = conflictException.Message
                },
            BadRequestException badRequestException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = badRequestException.Message
                },
            DbUpdateException dbUpdateException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = dbUpdateException.Message
                },
            InvalidOperationException invalidOperationException
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = invalidOperationException.Message
                },
            _
                => new ErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                },
        };
    }
}
