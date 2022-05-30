namespace OperationResult.AspNetCode.Middlewares
{
    using System.Text.Json.Serialization;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using System.Text.Json;
    using System;
    
    using Core.Models;
    using Helpers;
    using Core;
    
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            try
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;

                var model = OperationResultFactory.CreateResult<Empty>(ExceptionHelpers.GetMessages(exception));
                
                var result = JsonSerializer.Serialize(model, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    Converters = { new JsonStringEnumConverter() }
                });

                return context.Response.WriteAsync(result.Length > 4000
                    ? "Error message to long. Please use DEBUG in method HandleExceptionAsync to handle a whole of text of the exception"
                    : result);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Critical error {MiddlewareName}", nameof(ErrorHandlingMiddleware));
                return context.Response.WriteAsync(
                    $"{exception.Message} For more information please use DEBUG in method HandleExceptionAsync to handle a whole of text of the exception");
            }
            finally
            {
                _logger.LogError(exception, "Global exception handler");
            }
        }
    }
}