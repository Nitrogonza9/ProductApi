using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.IO;

namespace ProductApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            await _next(context);
            watch.Stop();
            var responseTime = watch.ElapsedMilliseconds;

            var logMessage = $"Request: {context.Request.Method} {context.Request.Path} responded in {responseTime} ms";
            _logger.LogInformation(logMessage);

            await File.AppendAllTextAsync("request_logs.txt", logMessage + Environment.NewLine);
        }
    }
}
