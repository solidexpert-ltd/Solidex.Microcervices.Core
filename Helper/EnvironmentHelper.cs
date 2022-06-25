using System;
using Microsoft.Data.SqlClient;

namespace Solidex.Microservices.Core.Helper
{
    public static class EnvironmentHelper
    {
        public static string UpdateConnectionString(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var initialCatalog = Environment.GetEnvironmentVariable("ConnectionStrnig_InitialCatalog");
            if (!string.IsNullOrEmpty(initialCatalog))
                builder.InitialCatalog = initialCatalog;
            var dataSource = Environment.GetEnvironmentVariable("ConnectionStrnig_DataSource");
            if (!string.IsNullOrEmpty(dataSource))
                builder.DataSource = dataSource;
            var userId = Environment.GetEnvironmentVariable("ConnectionStrnig_UserID");
            if (!string.IsNullOrEmpty(userId))
                builder.UserID = userId;
            var password = Environment.GetEnvironmentVariable("ConnectionStrnig_Password");
            if (!string.IsNullOrEmpty(password))
                builder.Password = password;

            return builder.ToString();
        }
    }
}