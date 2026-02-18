using Solidex.Microservices.Core.Infrastructure.SignalR;

namespace Solidex.Microservices.Core.Tests.Infrastructure;

public class BroadcastMessageTests
{
    [Fact]
    public void Properties_CanBeSetAndRead()
    {
        var message = new BroadcastMessage
        {
            MessageType = "notification",
            Data = new { Id = 1, Name = "Test" }
        };

        Assert.Equal("notification", message.MessageType);
        Assert.NotNull(message.Data);
    }

    [Fact]
    public void Properties_DefaultToNull()
    {
        var message = new BroadcastMessage();

        Assert.Null(message.MessageType);
        Assert.Null(message.Data);
    }

    [Fact]
    public void Data_CanHoldDifferentTypes()
    {
        var message = new BroadcastMessage();

        message.Data = "string data";
        Assert.Equal("string data", message.Data);

        message.Data = 42;
        Assert.Equal(42, message.Data);

        message.Data = new List<int> { 1, 2, 3 };
        Assert.IsType<List<int>>(message.Data);
    }
}
