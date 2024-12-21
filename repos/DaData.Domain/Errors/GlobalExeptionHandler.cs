using DaData.Domain.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace DaData.Domain.Errors
{
    public class GlobalExeptionHandler(ILogger<GlobalExeptionHandler> _logger) : IExeptionHandler
    {
        public async Task<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
        {
            _logger.LogError($"An error occurred while processing your request: {ex.Message}");

            ErrorResponse errorResponse;

            switch (ex)
            {
                case BadHttpRequestException:
                    errorResponse = new ErrorResponse((int)HttpStatusCode.BadRequest,
                        ex.GetType().Name,
                        ex.Message);
                    break;

                default:
                    errorResponse = new ErrorResponse((int)HttpStatusCode.InternalServerError,
                        "Internal server error",
                        ex.Message);
                    break;
            }

            httpContext.Response.StatusCode = errorResponse.StatusCode;
            httpContext.Response.ContentType = "application/json";

            var jsonResponce = JsonSerializer.Serialize(errorResponse);

            await httpContext.Response.WriteAsync(jsonResponce, cancellationToken);
               
            return true;
        }
    }
}
