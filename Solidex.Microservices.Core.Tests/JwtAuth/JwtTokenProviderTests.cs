using Solidex.Microservices.Core.JwtAuth;

namespace Solidex.Microservices.Core.Tests.JwtAuth;

public class JwtTokenProviderTests
{
    private readonly AuthOptions _authOptions = new AuthOptions
    {
        JwtKey = "ThisIsATestSecretKeyThatIsLongEnoughForHmacSha256",
        JwtIssuer = "TestIssuer"
    };

    [Fact]
    public void GenerateJwtToken_ReturnsNonEmptyString()
    {
        var token = JwtTokenProvider.GenerateJwtToken(
            "testuser",
            "user-id-123",
            Guid.NewGuid(),
            new List<string> { "admin" },
            _authOptions);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateJwtToken_ReturnsDifferentTokensForDifferentUsers()
    {
        var token1 = JwtTokenProvider.GenerateJwtToken(
            "user1",
            "id-1",
            Guid.NewGuid(),
            new List<string> { "admin" },
            _authOptions);

        var token2 = JwtTokenProvider.GenerateJwtToken(
            "user2",
            "id-2",
            Guid.NewGuid(),
            new List<string> { "user" },
            _authOptions);

        Assert.NotEqual(token1, token2);
    }

    [Fact]
    public void GenerateJwtToken_TokenHasThreeParts()
    {
        var token = JwtTokenProvider.GenerateJwtToken(
            "testuser",
            "user-id",
            Guid.NewGuid(),
            new List<string> { "admin" },
            _authOptions);

        var parts = token.Split('.');
        Assert.Equal(3, parts.Length);
    }

    [Fact]
    public void GenerateJwtToken_WithMultipleRoles_ReturnsValidToken()
    {
        var roles = new List<string> { "admin", "manager", "user" };

        var token = JwtTokenProvider.GenerateJwtToken(
            "testuser",
            "user-id",
            Guid.NewGuid(),
            roles,
            _authOptions);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateJwtToken_WithEmptyRoles_ReturnsValidToken()
    {
        var token = JwtTokenProvider.GenerateJwtToken(
            "testuser",
            "user-id",
            Guid.NewGuid(),
            new List<string>(),
            _authOptions);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void GenerateSystemToken_ReturnsNonEmptyToken()
    {
        var token = JwtTokenProvider.GenerateSystemToken(
            Solidex.Core.Base.ComplexTypes.SystemUsersEnumeration.SystemMessage,
            _authOptions);

        Assert.False(string.IsNullOrEmpty(token));
    }

    [Fact]
    public void AuthOptions_PropertiesCanBeSetAndRead()
    {
        var options = new AuthOptions
        {
            JwtKey = "mykey",
            JwtIssuer = "myissuer"
        };

        Assert.Equal("mykey", options.JwtKey);
        Assert.Equal("myissuer", options.JwtIssuer);
    }
}
