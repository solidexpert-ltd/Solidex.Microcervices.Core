using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Solidex.Microservices.Core.ServerMiddleware;

namespace Solidex.Microservices.Core.Tests.ServerMiddleware;

public class ErrorHandlingMiddlewareTests
{
    [Fact]
    public async Task Invoke_NoException_CallsNextDelegate()
    {
        var nextCalled = false;
        RequestDelegate next = (context) =>
        {
            nextCalled = true;
            return Task.CompletedTask;
        };
        var middleware = new ErrorHandlingMiddleware(next);
        var context = new DefaultHttpContext();

        await middleware.Invoke(context);

        Assert.True(nextCalled);
    }

    [Fact]
    public async Task Invoke_GeneralException_Returns500()
    {
        RequestDelegate next = (context) => throw new Exception("Test error");
        var middleware = new ErrorHandlingMiddleware(next);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.Invoke(context);

        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/json", context.Response.ContentType);
    }

    [Fact]
    public async Task Invoke_UnauthorizedAccessException_Returns401()
    {
        RequestDelegate next = (context) => throw new UnauthorizedAccessException("Unauthorized");
        var middleware = new ErrorHandlingMiddleware(next);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.Invoke(context);

        Assert.Equal((int)HttpStatusCode.Unauthorized, context.Response.StatusCode);
    }

    [Fact]
    public async Task Invoke_Exception_ResponseContainsErrorMessage()
    {
        var errorMessage = "Something went wrong";
        RequestDelegate next = (context) => throw new Exception(errorMessage);
        var middleware = new ErrorHandlingMiddleware(next);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.Invoke(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        Assert.Contains(errorMessage, responseBody);
    }

    [Fact]
    public async Task Invoke_Exception_ResponseIsValidJson()
    {
        RequestDelegate next = (context) => throw new Exception("Test error");
        var middleware = new ErrorHandlingMiddleware(next);
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();

        await middleware.Invoke(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        var jsonObj = JsonConvert.DeserializeObject<dynamic>(responseBody);
        Assert.NotNull(jsonObj);
    }
}
