using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Solidex.Microservices.Core.DocExplorer
{
    /// <summary>
    /// Middleware alternative for serving endpoint documentation as markdown.
    /// Handles requests to /docexplorer/{routeName}.
    /// </summary>
    public class DocExplorerMiddleware
    {
        private readonly RequestDelegate _next;
        private const string BasePath = "/docexplorer/";

        public DocExplorerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IWebHostEnvironment env)
        {
            var path = context.Request.Path.Value;
            if (path == null || !path.StartsWith(BasePath, System.StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            var routeName = path[BasePath.Length..].Trim('/');
            if (string.IsNullOrWhiteSpace(routeName))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("routeName is required. Use the Name from [HttpGet(Name = \"...\")] (e.g. GetCompanyMessages)");
                return;
            }

            var map = DocExplorerRouteMap.RouteNameToPath.Value;
            if (!map.TryGetValue(routeName.Trim(), out var relativePath))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Documentation not found for route name: {routeName}");
                return;
            }

            var docsPath = ResolveDocsPath(env);
            if (string.IsNullOrEmpty(docsPath))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Documentation directory not found");
                return;
            }

            var filePath = Path.GetFullPath(Path.Combine(docsPath, relativePath.Replace('/', Path.DirectorySeparatorChar)));
            if (!filePath.StartsWith(Path.GetFullPath(docsPath)) || !File.Exists(filePath))
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync($"Documentation file not found: {relativePath}");
                return;
            }

            context.Response.ContentType = "text/markdown";
            var content = await File.ReadAllTextAsync(filePath);
            await context.Response.WriteAsync(content);
        }

        private static string ResolveDocsPath(IWebHostEnvironment env)
        {
            var candidates = new[]
            {
                Path.Combine(env.ContentRootPath, "controllerDocs"),
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? ".",
                    "..", "controllerDocs")
            };
            return candidates.FirstOrDefault(Directory.Exists);
        }
    }
}
