using Solidex.Microservices.Core.Infrastructure;

namespace Solidex.Microservices.Core.Tests.Infrastructure;

public class JsonPatchEntityTests
{
    [Fact]
    public void Properties_CanBeSetAndRead()
    {
        var entity = new JsonPatchEntity
        {
            Act = "replace",
            Path = "/name",
            Value = "test"
        };

        Assert.Equal("replace", entity.Act);
        Assert.Equal("/name", entity.Path);
        Assert.Equal("test", entity.Value);
    }

    [Fact]
    public void Properties_DefaultToNull()
    {
        var entity = new JsonPatchEntity();

        Assert.Null(entity.Act);
        Assert.Null(entity.Path);
        Assert.Null(entity.Value);
    }

    [Fact]
    public void Properties_CanBeUpdated()
    {
        var entity = new JsonPatchEntity
        {
            Act = "add",
            Path = "/description",
            Value = "initial"
        };

        entity.Value = "updated";

        Assert.Equal("updated", entity.Value);
    }
}
