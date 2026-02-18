# Solidex.Microservices.Core

A comprehensive .NET 10 library providing essential infrastructure components for building microservices with standardized patterns, JWT authentication, and database context management.

## 📦 NuGet Package

```
Solidex.Microservices.Core
```

**Version:** 1.0.11  
**Target Framework:** .NET 10.0

## 🚀 Features

### 🔐 Authentication & Authorization
- **JWT Token Management**: Complete JWT token generation and validation
- **System Token Support**: Built-in system user token generation
- **Role-based Authorization**: Support for role-based access control
- **HTTP Client Integration**: Pre-configured JWT-enabled HTTP clients

### 🗄️ Database Infrastructure
- **Entity Framework Core Integration**: Enhanced DbContext with automatic audit fields
- **Audit Trail**: Automatic creation and modification date tracking
- **Entity Interfaces**: Standardized entity patterns with `IEntity` support

### 🌐 Web API Infrastructure
- **Controller Extensions**: Standardized API response patterns
- **Error Handling Middleware**: Global exception handling with proper HTTP status codes
- **Response Models**: Consistent API response structure
- **Pagination Support**: Built-in pagination utilities

### 🔧 Utilities & Helpers
- **JSON Processing**: Robust JSON parsing and serialization utilities
- **Environment Configuration**: Dynamic connection string management
- **Debug Support**: Verbose logging and debugging utilities
- **SignalR Support**: Real-time messaging infrastructure

## 📁 Project Structure

```
Solidex.Microservices.Core/
├── Db/
│   └── DbContextChanges.cs          # Enhanced DbContext with audit fields
├── Extension/
│   └── ControllerExtension.cs       # Controller response extensions
├── Helper/
│   ├── DebugHelper.cs               # Debug and logging utilities
│   ├── EnvironmentHelper.cs         # Environment configuration helpers
│   └── JsonHelper.cs                # JSON processing utilities
├── Infrastructure/
│   ├── Attributes/                  # Custom attributes
│   ├── Authorization/               # Authorization components
│   ├── SignalR/
│   │   └── BroadcastMessage.cs      # SignalR message models
│   ├── IEntityProperty.cs           # Entity property interface
│   ├── INamedEntity.cs              # Named entity interface
│   ├── JsonPatchEntity.cs           # JSON patch support
│   ├── Paginator.cs                 # Pagination utilities
│   └── ResponseHelper.cs            # Response model helpers
├── JwtAuth/
│   ├── JwtHttpClient.cs             # JWT-enabled HTTP client
│   └── JwtTokenProvider.cs          # JWT token generation
├── ServerMiddleware/
│   ├── ApplicationExtension.cs      # Application builder extensions
│   ├── ErrorHandlingMiddleware.cs   # Global error handling
│   ├── ServiceCollectionExtension.cs # Service registration extensions
│   └── ServiceMiddleware.cs         # Service configuration utilities
```

## 🛠️ Installation

### NuGet Package Manager
```bash
Install-Package Solidex.Microservices.Core
```

### .NET CLI
```bash
dotnet add package Solidex.Microservices.Core
```

## 📖 Usage Examples

### 1. Database Context Setup

```csharp
public class YourDbContext : DbContextWithChanges
{
    public DbSet<YourEntity> YourEntities { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
}
```

### 2. JWT Authentication Configuration

```csharp
// In Startup.cs or Program.cs
services.AddSolidAuthorization();

// Configure AuthOptions (you need to implement this class)
public static class AuthOptions
{
    public static string JwtKey { get; set; } = "your-secret-key-here";
    public static string JwtIssuer { get; set; } = "your-issuer";
}
```

### 3. Controller Extensions

```csharp
[ApiController]
[Route("api/[controller]")]
public class SampleController : ControllerBase, IControllerWithMapper
{
    public IMapper Mapper { get; }
    
    [HttpGet]
    public IActionResult Get()
    {
        // Standardized responses
        return this.CreatedResult(new { message = "Success" });
        return this.BadRequestResult("Invalid request");
        return this.NotFoundResult("Resource not found");
        return this.AccessDeniedResult("Access denied");
    }
}
```

### 4. JWT HTTP Client

```csharp
// System token client
var client = new JwtHttpClient("https://api.example.com");

// User token client
var client = new JwtHttpClient("https://api.example.com", httpContextAccessor);
```

### 5. Error Handling Middleware

```csharp
// In Configure method
app.UseMiddleware<ErrorHandlingMiddleware>();
```

### 6. Service Registration

```csharp
// In Startup.cs or Program.cs
services.AddServerControllers();
```

## 🔧 Configuration

### Environment Variables

The library supports dynamic connection string configuration through environment variables:

- `ConnectionStrnig_InitialCatalog` - Database name
- `ConnectionStrnig_DataSource` - Database server
- `ConnectionStrnig_UserID` - Database username
- `ConnectionStrnig_Password` - Database password

### AuthOptions Configuration

You need to implement the `AuthOptions` class with your JWT configuration:

```csharp
public static class AuthOptions
{
    public static string JwtKey { get; set; } = "your-secret-key-here";
    public static string JwtIssuer { get; set; } = "your-issuer";
}
```

## 📋 Dependencies

- **AutoMapper** (13.0.1) - Object mapping
- **Microsoft.AspNetCore.Authentication.JwtBearer** (10.0.0) - JWT authentication
- **Microsoft.AspNetCore.Authorization** (10.0.0) - Authorization
- **Microsoft.AspNetCore.Mvc.NewtonsoftJson** (10.0.0) - JSON serialization
- **Microsoft.EntityFrameworkCore** (10.0.0) - Database access
- **Newtonsoft.Json** (13.0.3) - JSON processing
- **Solidex.Core.Base** (1.0.20-1.2.0) - Base infrastructure

## 🏗️ Building from Source

```bash
# Clone the repository
git clone <repository-url>

# Navigate to the project directory
cd Solidex.Microservices.Core

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run tests (if available)
dotnet test

# Create NuGet package
dotnet pack
```

## 📝 License

[Add your license information here]

## 🤝 Contributing

[Add contribution guidelines here]

## 📞 Support

[Add support contact information here]

## 🔄 Version History

- **1.0.11** - Current version
- **1.0.10** - Previous version
- [Add more version history as needed]

---

**Note:** This library is designed for .NET 10.0 applications and provides a solid foundation for building microservices with standardized patterns and best practices. 