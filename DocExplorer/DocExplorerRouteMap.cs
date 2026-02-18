using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Solidex.Microservices.Core.DocExplorer
{
    internal static class DocExplorerRouteMap
    {
        internal static readonly Lazy<Dictionary<string, string>> RouteNameToPath = new(BuildRouteNameMap);

        private static Dictionary<string, string> BuildRouteNameMap()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            var assembly = Assembly.GetEntryAssembly();
            if (assembly == null) return map;

            foreach (var type in assembly.GetTypes()
                         .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var controllerName = type.Name;
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    var routeName = GetRouteName(method);
                    if (!string.IsNullOrEmpty(routeName))
                        map[routeName] = $"{controllerName}{Path.DirectorySeparatorChar}{routeName}.md";
                }
            }

            return map;
        }

        private static string GetRouteName(MethodInfo method)
        {
            return method.GetCustomAttribute<HttpGetAttribute>()?.Name
                   ?? method.GetCustomAttribute<HttpPostAttribute>()?.Name
                   ?? method.GetCustomAttribute<HttpPutAttribute>()?.Name
                   ?? method.GetCustomAttribute<HttpDeleteAttribute>()?.Name;
        }
    }
}
