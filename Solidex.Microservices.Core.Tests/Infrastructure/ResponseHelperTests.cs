using Solidex.Core.Base.Infrastructure;
using Solidex.Microservices.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Solidex.Microservices.Core.Tests.Infrastructure;

public class ResponseHelperTests
{
    [Fact]
    public void AsActionResult_ResponseViewModel_ReturnsObjectResultWithCorrectStatusCode()
    {
        var model = new ResponseViewModel("Not Found", 404, false);

        var result = model.AsActionResult();

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, objectResult.StatusCode);
    }

    [Fact]
    public void AsActionResult_ResponseViewModel_200StatusCode()
    {
        var model = new ResponseViewModel("OK", 200, true);

        var result = model.AsActionResult();

        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(200, objectResult.StatusCode);
    }

    [Fact]
    public void ToPageView_ReturnsCorrectPageView()
    {
        var list = new List<string> { "a", "b", "c" };

        var pageView = list.ToPageView(2, 10);

        Assert.Equal(3, pageView.Count);
        Assert.Equal(2, pageView.Page);
        Assert.Equal(10, pageView.Total);
        Assert.Equal(list, pageView.Elements);
    }

    [Fact]
    public void ToPageView_EmptyList_ReturnsZeroCount()
    {
        var list = new List<string>();

        var pageView = list.ToPageView();

        Assert.Equal(0, pageView.Count);
        Assert.Equal(1, pageView.Page);
        Assert.Equal(0, pageView.Total);
        Assert.Empty(pageView.Elements);
    }

    [Fact]
    public void ToPageView_DefaultParameters_UsesPageOneAndZeroTotal()
    {
        var list = new List<string> { "item1" };

        var pageView = list.ToPageView();

        Assert.Equal(1, pageView.Page);
        Assert.Equal(0, pageView.Total);
    }
}
