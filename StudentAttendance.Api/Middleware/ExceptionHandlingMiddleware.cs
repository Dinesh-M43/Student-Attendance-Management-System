using StudentAttendance.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace StudentAttendance.Api.Middleware
{
    /// <summary>
    /// Middleware that catches exceptions thrown by downstream components,
    /// logs them and returns a JSON error response with an appropriate status code.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;


        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance for this middleware.</param>

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions for the provided HTTP context.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task that represents the completion of middleware execution.</returns>

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unhandled exception occurred.");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;

            if (exception is BusinessException)
            {
                statusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                statusCode = statusCode,
                message = exception is BusinessException
                    ? exception.Message
                    : "An unexpected error occurred."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
