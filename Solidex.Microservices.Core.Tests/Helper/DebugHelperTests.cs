using Solidex.Microservices.Core.Helper;

namespace Solidex.Microservices.Core.Tests.Helper;

public class DebugHelperTests
{
    [Fact]
    public void VerboseMode_CanBeSet()
    {
        // Setting VerboseMode should not throw
        DebugHelper.VerboseMode = true;
        DebugHelper.VerboseMode = false;
    }

    [Fact]
    public void Out_WhenVerboseModeDisabled_DoesNotThrow()
    {
        DebugHelper.VerboseMode = false;

        var exception = Record.Exception(() => DebugHelper.Out("test message", DebugHelper.Type.Info));

        Assert.Null(exception);
    }

    [Fact]
    public void Out_WhenVerboseModeEnabled_DoesNotThrow()
    {
        DebugHelper.VerboseMode = true;

        try
        {
            var exception = Record.Exception(() => DebugHelper.Out("test message", DebugHelper.Type.Error));

            Assert.Null(exception);
        }
        finally
        {
            DebugHelper.VerboseMode = false;
        }
    }

    [Fact]
    public void Out_WithNullType_DoesNotThrow()
    {
        DebugHelper.VerboseMode = true;

        try
        {
            var exception = Record.Exception(() => DebugHelper.Out("test message"));

            Assert.Null(exception);
        }
        finally
        {
            DebugHelper.VerboseMode = false;
        }
    }

    [Fact]
    public void JsonFieldParseError_DoesNotThrow()
    {
        DebugHelper.VerboseMode = false;

        var exception = Record.Exception(() => DebugHelper.JsonFieldParseError("testField", new { data = "test" }));

        Assert.Null(exception);
    }
}
