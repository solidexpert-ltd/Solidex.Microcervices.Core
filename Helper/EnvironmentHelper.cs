using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Solidex.Microservices.Core.Helper
{
    public static class EnvironmentHelper
    {
        public static string UpdateConnectionString(string connectionString)
        {
            var dict = connectionString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(part => part.Split('='))
                .ToDictionary(split => split[0], split => split[1]);
            var connectionParameters = new Dictionary<string, string>(dict, StringComparer.OrdinalIgnoreCase);


            var initialCatalog = Environment.GetEnvironmentVariable("ConnectionStrnig_InitialCatalog");
            if (!string.IsNullOrEmpty(initialCatalog))
                connectionParameters["Initial Catalog"] = initialCatalog;
            var dataSource = Environment.GetEnvironmentVariable("ConnectionStrnig_DataSource");
            if (!string.IsNullOrEmpty(dataSource))
                connectionParameters["Data Source"] = dataSource;
            var userId = Environment.GetEnvironmentVariable("ConnectionStrnig_UserID");
            if (!string.IsNullOrEmpty(userId))
                connectionParameters["UserID"] = userId;
            var password = Environment.GetEnvironmentVariable("ConnectionStrnig_Password");
            if (!string.IsNullOrEmpty(password))
                connectionParameters["Password"] = password;

            return String.Join(";", connectionParameters.Select(x => $"{x.Key}={x.Value}"));

            //  return builder.ToString();
        }
    }
}