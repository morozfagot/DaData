using Microsoft.AspNetCore.Http;

namespace DaData.Domain.Abstractions
{
    public interface IExeptionHandler
    {
        Task<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);
    }
}
