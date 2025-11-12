
using System.Diagnostics;

namespace Restaurants.API.CustomMiddlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var watch = Stopwatch.StartNew();
        await next.Invoke(context);
        watch.Stop();

        if (watch.ElapsedMilliseconds / 1000 > 4 )
        {
            logger.LogWarning("Request [{Method}] at [{Path}] took [{ElapsedMilliseconds}] ms",
                context.Request.Method,
                context.Request.Path,
                watch.ElapsedMilliseconds);
        }

    }
}
