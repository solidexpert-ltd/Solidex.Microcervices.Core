using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using Solidex.Microservices.Core.DocExplorer;

namespace Solidex.Microservices.Core.Tests.DocExplorer;

public class DocExplorerMiddlewareTests
{
    [Fact]
    public void ResolveDocsPath_FindsCaseInsensitiveFolder()
    {
        // Create a temporary test directory structure
        var tempDir = Path.Combine(Path.GetTempPath(), $"DocExplorerTest_{Guid.NewGuid()}");
        var upperCaseDocsDir = Path.Combine(tempDir, "ControllerDocs");
        
        try
        {
            Directory.CreateDirectory(upperCaseDocsDir);
            
            // Create a test file
            var testFile = Path.Combine(upperCaseDocsDir, "test.md");
            File.WriteAllText(testFile, "# Test Documentation");

            // Mock the IWebHostEnvironment to return our temp directory
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(e => e.ContentRootPath).Returns(tempDir);

            // Use reflection to call the private ResolveDocsPath method
            var middlewareType = typeof(DocExplorerMiddleware);
            var resolveMethod = middlewareType.GetMethod("ResolveDocsPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            Assert.NotNull(resolveMethod);
            
            var result = resolveMethod.Invoke(null, new object[] { mockEnv.Object }) as string;

            // Should find the folder regardless of case
            Assert.NotNull(result);
            Assert.True(Directory.Exists(result));
            Assert.Equal(upperCaseDocsDir, result, ignoreCase: true);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void ResolveDocsPath_FindsExactCaseFolder()
    {
        // Create a temporary test directory structure
        var tempDir = Path.Combine(Path.GetTempPath(), $"DocExplorerTest_{Guid.NewGuid()}");
        var docsDir = Path.Combine(tempDir, "controllerDocs");
        
        try
        {
            Directory.CreateDirectory(docsDir);

            // Mock the IWebHostEnvironment
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(e => e.ContentRootPath).Returns(tempDir);

            // Use reflection to call the private ResolveDocsPath method
            var middlewareType = typeof(DocExplorerMiddleware);
            var resolveMethod = middlewareType.GetMethod("ResolveDocsPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            Assert.NotNull(resolveMethod);
            
            var result = resolveMethod.Invoke(null, new object[] { mockEnv.Object }) as string;

            // Should find the folder
            Assert.NotNull(result);
            Assert.True(Directory.Exists(result));
            Assert.Equal(docsDir, result);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }

    [Fact]
    public void ResolveDocsPath_ReturnsNull_WhenFolderNotFound()
    {
        // Create a temporary test directory structure without controllerDocs
        var tempDir = Path.Combine(Path.GetTempPath(), $"DocExplorerTest_{Guid.NewGuid()}");
        
        try
        {
            Directory.CreateDirectory(tempDir);

            // Mock the IWebHostEnvironment
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv.Setup(e => e.ContentRootPath).Returns(tempDir);

            // Use reflection to call the private ResolveDocsPath method
            var middlewareType = typeof(DocExplorerMiddleware);
            var resolveMethod = middlewareType.GetMethod("ResolveDocsPath", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            Assert.NotNull(resolveMethod);
            
            var result = resolveMethod.Invoke(null, new object[] { mockEnv.Object }) as string;

            // Should return null when folder doesn't exist
            Assert.Null(result);
        }
        finally
        {
            // Cleanup
            if (Directory.Exists(tempDir))
            {
                Directory.Delete(tempDir, true);
            }
        }
    }
}
