using Solidex.Microservices.Core.Infrastructure;

namespace Solidex.Microservices.Core.Tests.Infrastructure;

public class PaginatorTests
{
    [Theory]
    [InlineData(1, 10, 0)]
    [InlineData(2, 10, 10)]
    [InlineData(3, 10, 20)]
    [InlineData(1, 25, 0)]
    [InlineData(5, 20, 80)]
    public void Skip_ValidPage_ReturnsCorrectOffset(int page, int count, int expected)
    {
        var result = Paginator.Skip(page, count);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(0, 10, 0)]
    [InlineData(-1, 10, 0)]
    [InlineData(-5, 10, 0)]
    public void Skip_ZeroOrNegativePage_ReturnsZero(int page, int count, int expected)
    {
        var result = Paginator.Skip(page, count);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Skip_PageOneCountZero_ReturnsZero()
    {
        var result = Paginator.Skip(1, 0);

        Assert.Equal(0, result);
    }

    [Fact]
    public void Skip_LargePage_ReturnsCorrectOffset()
    {
        var result = Paginator.Skip(100, 50);

        Assert.Equal(4950, result);
    }
}
