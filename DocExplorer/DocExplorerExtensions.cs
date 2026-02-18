using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Solidex.Microservices.Core.DocExplorer
{
    public static class DocExplorerExtensions
    {
        /// <summary>
        /// Registers the <see cref="DocExplorerController"/> from the core package
        /// so it is discovered by the MVC framework as an application part.
        /// </summary>
        public static IMvcBuilder AddDocExplorer(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(typeof(DocExplorerController).Assembly);
            return builder;
        }

        /// <summary>
        /// Adds the <see cref="DocExplorerMiddleware"/> to the request pipeline.
        /// Use this as an alternative to the controller when you prefer middleware-based routing.
        /// </summary>
        public static IApplicationBuilder UseDocExplorer(this IApplicationBuilder app)
        {
            app.UseMiddleware<DocExplorerMiddleware>();
            return app;
        }
    }
}
