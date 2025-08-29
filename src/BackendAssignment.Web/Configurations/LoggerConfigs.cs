using Serilog;
using Serilog.Core;
using Serilog.Events;
using Microsoft.AspNetCore.Http;

namespace BackendAssignment.Web.Configurations;

public static class LoggerConfigs
{
  public static WebApplicationBuilder AddLoggerConfigs(this WebApplicationBuilder builder)
  {
    builder.Host.UseSerilog((context, config) => 
    {
      config.ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext();
    });

    return builder;
  }
}

public static class CorrelationIdMiddlewareExtensions
{
  public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder app)
  {
    return app.UseMiddleware<CorrelationIdLoggingMiddleware>();
  }
}

public class CorrelationIdLoggingMiddleware
{
  private readonly RequestDelegate _next;

  public CorrelationIdLoggingMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    var correlationId = Guid.NewGuid().ToString();
    
    // Add correlation ID to response headers
    context.Response.OnStarting(() =>
    {
      context.Response.Headers["X-Correlation-Id"] = correlationId;
      return Task.CompletedTask;
    });

    // Add correlation ID to log context
    using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
    {
      await _next(context);
    }
  }
}
