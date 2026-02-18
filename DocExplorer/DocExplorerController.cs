using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Solidex.Microservices.Core.DocExplorer
{
    /// <summary>
    /// Returns markdown documentation for API endpoints by route name.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [Produces("text/markdown")]
    public class DocExplorerController : ControllerBase
    {
        private readonly string _docsPath;

        public DocExplorerController(IWebHostEnvironment env)
        {
            var candidates = new[]
            {
                Path.Combine(env.ContentRootPath, "controllerDocs"),
                Path.Combine(
                    Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? ".",
                    "..", "controllerDocs")
            };
            _docsPath = candidates.FirstOrDefault(Directory.Exists);
        }

        /// <summary>
        /// Returns the markdown documentation for a specific endpoint.
        /// </summary>
        /// <param name="routeName">The Name from route attribute (e.g. GetCompanyMessages, GetAllDeliveryMethods)</param>
        /// <returns>Markdown content or 404 if not found</returns>
        [HttpGet("{*routeName}")]
        [ProducesResponseType(typeof(string), 200, "text/markdown")]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(string routeName)
        {
            if (string.IsNullOrWhiteSpace(routeName))
                return BadRequest("routeName is required. Use the Name from [HttpGet(Name = \"...\")] (e.g. GetCompanyMessages)");

            var map = DocExplorerRouteMap.RouteNameToPath.Value;
            if (!map.TryGetValue(routeName.Trim(), out var relativePath))
                return NotFound($"Documentation not found for route name: {routeName}");

            if (string.IsNullOrEmpty(_docsPath))
                return NotFound("Documentation directory not found");

            var filePath = Path.GetFullPath(Path.Combine(_docsPath, relativePath.Replace('/', Path.DirectorySeparatorChar)));

            if (!filePath.StartsWith(Path.GetFullPath(_docsPath)))
                return BadRequest("Invalid route name");

            if (!System.IO.File.Exists(filePath))
                return NotFound($"Documentation file not found: {relativePath}");

            var content = await System.IO.File.ReadAllTextAsync(filePath);
            return Content(content, "text/markdown");
        }
    }
}
