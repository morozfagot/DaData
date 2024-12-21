using DaData.Domain.Errors;

namespace DaData
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExeptionHandler(this IApplicationBuilder builder)
        {
            return builder.Use(async(context, next) =>
            {
                try
                {
                    await next(context);
                }
                catch (Exception ex)
                {
                    var exeptionHandler = context.RequestServices.GetRequiredService<GlobalExeptionHandler>();
                    
                    var errorResult= await exeptionHandler.TryHandleAsync(context, ex, CancellationToken.None);

                    if (errorResult == false)
                    {
                        throw ex;
                    }
                }
            });
        }
    }
}
