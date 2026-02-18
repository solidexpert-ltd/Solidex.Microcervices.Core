using Solidex.Microservices.Core.Helper;
using Newtonsoft.Json.Linq;

namespace Solidex.Microservices.Core.Tests.Helper;

public class JsonHelperTests
{
    [Fact]
    public void AsString_ReturnsFormattedJsonString()
    {
        var obj = new { Name = "Test", Value = 42 };

        var result = JsonHelper.AsString(obj);

        Assert.Contains("\"Name\": \"Test\"", result);
        Assert.Contains("\"Value\": 42", result);
    }

    [Fact]
    public void ExtractStr_FirstLevel_ReturnsValue()
    {
        dynamic json = JObject.Parse("{\"name\": \"hello\"}");

        var result = JsonHelper.ExtractStr(json, "name");

        Assert.Equal("hello", result);
    }

    [Fact]
    public void ExtractStr_TwoLevels_ReturnsNestedValue()
    {
        dynamic json = JObject.Parse("{\"level1\": {\"level2\": \"nested\"}}");

        var result = JsonHelper.ExtractStr(json, "level1", "level2");

        Assert.Equal("nested", result);
    }

    [Fact]
    public void ExtractStr_MissingKey_ReturnsNull()
    {
        dynamic json = JObject.Parse("{\"name\": \"hello\"}");

        var result = JsonHelper.ExtractStr(json, "missing", silent: true);

        Assert.Null(result);
    }

    [Fact]
    public void ExtractStr_NullValue_ReturnsNull()
    {
        dynamic json = JObject.Parse("{\"name\": null}");

        var result = JsonHelper.ExtractStr(json, "name", silent: true);

        // JObject null values return empty string via ToString()
        Assert.True(result == null || result == "");
    }

    [Fact]
    public void ExtractInt_ValidInteger_ReturnsValue()
    {
        dynamic json = JObject.Parse("{\"count\": 42}");

        var result = JsonHelper.ExtractInt(json, "count");

        Assert.Equal(42, result);
    }

    [Fact]
    public void ExtractInt_InvalidValue_ReturnsNull()
    {
        dynamic json = JObject.Parse("{\"count\": \"notanumber\"}");

        var result = JsonHelper.ExtractInt(json, "count", silent: true);

        Assert.Null(result);
    }

    [Fact]
    public void ExtractInt_MissingKey_ReturnsNull()
    {
        dynamic json = JObject.Parse("{\"name\": \"hello\"}");

        var result = JsonHelper.ExtractInt(json, "missing", silent: true);

        Assert.Null(result);
    }

    [Fact]
    public void ExtractDouble_ValidDouble_ReturnsValue()
    {
        dynamic json = JObject.Parse("{\"price\": 19.99}");

        double? result = JsonHelper.ExtractDouble(json, "price");

        Assert.NotNull(result);
        Assert.Equal(19.99, result.Value, 2);
    }

    [Fact]
    public void ExtractDouble_IntegerValue_ReturnsAsDouble()
    {
        dynamic json = JObject.Parse("{\"price\": 42}");

        double? result = JsonHelper.ExtractDouble(json, "price");

        Assert.NotNull(result);
        Assert.Equal(42.0, result.Value, 2);
    }

    [Fact]
    public void ExtractStr_SecondLevelMissing_ReturnsNull()
    {
        dynamic json = JObject.Parse("{\"level1\": {\"other\": \"value\"}}");

        var result = JsonHelper.ExtractStr(json, "level1", "missing", silent: true);

        Assert.Null(result);
    }
}
