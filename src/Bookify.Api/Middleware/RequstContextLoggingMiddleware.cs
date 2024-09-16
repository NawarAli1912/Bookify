using Serilog.Context;

namespace Bookify.Api.Middleware;

public class RequstContextLoggingMiddleware
{
    private const string CorrelationIdHeaderName = "X-Correlation-Id";

    private readonly RequestDelegate _next;

    public RequstContextLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return _next(context);
        }
    }

    private string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CorrelationIdHeaderName, out var correlationId);


        return correlationId.FirstOrDefault() ?? context.TraceIdentifier;
    }
}
