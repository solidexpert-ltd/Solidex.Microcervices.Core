using Microsoft.AspNetCore.Builder;

namespace Solidex.Microservices.Core.DocExplorer
{
    public static class DocExplorerExtensions
    {
        /// <summary>
        /// Adds the <see cref="DocExplorerMiddleware"/> to the request pipeline.
        /// Handles requests to /docexplorer/{routeName} and returns markdown documentation.
        /// </summary>
        public static IApplicationBuilder UseDocExplorer(this IApplicationBuilder app)
        {
            app.UseMiddleware<DocExplorerMiddleware>();
            return app;
        }
    }
}
