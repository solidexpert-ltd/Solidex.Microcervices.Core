using Solidex.Microservices.Core.Helper;

namespace Solidex.Microservices.Core.Tests.Helper;

public class EnvironmentHelperTests
{
    [Fact]
    public void UpdateConnectionString_NoEnvVars_ReturnsOriginalString()
    {
        // Clear any env vars that might interfere
        Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_UserID", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_Password", null);

        var connectionString = "Data Source=localhost;Initial Catalog=TestDb;UserID=sa;Password=pass123";

        var result = EnvironmentHelper.UpdateConnectionString(connectionString);

        Assert.Contains("Data Source=localhost", result);
        Assert.Contains("Initial Catalog=TestDb", result);
        Assert.Contains("UserID=sa", result);
        Assert.Contains("Password=pass123", result);
    }

    [Fact]
    public void UpdateConnectionString_WithInitialCatalogEnvVar_OverridesValue()
    {
        Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", "OverriddenDb");
        Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_UserID", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_Password", null);

        try
        {
            var connectionString = "Data Source=localhost;Initial Catalog=TestDb;UserID=sa;Password=pass123";

            var result = EnvironmentHelper.UpdateConnectionString(connectionString);

            Assert.Contains("Initial Catalog=OverriddenDb", result);
        }
        finally
        {
            Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", null);
        }
    }

    [Fact]
    public void UpdateConnectionString_WithDataSourceEnvVar_OverridesValue()
    {
        Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", "newserver");
        Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_UserID", null);
        Environment.SetEnvironmentVariable("ConnectionStrnig_Password", null);

        try
        {
            var connectionString = "Data Source=localhost;Initial Catalog=TestDb;UserID=sa;Password=pass123";

            var result = EnvironmentHelper.UpdateConnectionString(connectionString);

            Assert.Contains("Data Source=newserver", result);
        }
        finally
        {
            Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", null);
        }
    }

    [Fact]
    public void UpdateConnectionString_WithMultipleEnvVars_OverridesAllValues()
    {
        Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", "NewDb");
        Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", "newserver");
        Environment.SetEnvironmentVariable("ConnectionStrnig_UserID", "newuser");
        Environment.SetEnvironmentVariable("ConnectionStrnig_Password", "newpass");

        try
        {
            var connectionString = "Data Source=localhost;Initial Catalog=TestDb;UserID=sa;Password=pass123";

            var result = EnvironmentHelper.UpdateConnectionString(connectionString);

            Assert.Contains("Initial Catalog=NewDb", result);
            Assert.Contains("Data Source=newserver", result);
            Assert.Contains("UserID=newuser", result);
            Assert.Contains("Password=newpass", result);
        }
        finally
        {
            Environment.SetEnvironmentVariable("ConnectionStrnig_InitialCatalog", null);
            Environment.SetEnvironmentVariable("ConnectionStrnig_DataSource", null);
            Environment.SetEnvironmentVariable("ConnectionStrnig_UserID", null);
            Environment.SetEnvironmentVariable("ConnectionStrnig_Password", null);
        }
    }
}
