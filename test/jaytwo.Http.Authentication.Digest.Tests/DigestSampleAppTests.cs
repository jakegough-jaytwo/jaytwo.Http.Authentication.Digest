using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using jaytwo.FluentHttp;
using Xunit;

namespace jaytwo.Http.Authentication.Digest.Tests;

public class DigestSampleAppTests : IClassFixture<DigestSampleAppWebApplicationFactory>
{
    private readonly DigestSampleAppWebApplicationFactory _fixture;

    public DigestSampleAppTests(DigestSampleAppWebApplicationFactory fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetHome_ReturnsOkWithoutDigestAuthenticationProvider()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.SendAsync(request =>
        {
            request
                .WithMethod(HttpMethod.Get)
                .WithUriPath("/home");
        });

        // Assert
        using (response)
        {
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Welcome to the public insecure area.", content);
        }
    }

    [Fact]
    public async Task GetSecure_ReturnsUnauthorizedWithoutDigestAuthenticationProvider()
    {
        // Arrange
        var client = _fixture.CreateClient();

        // Act
        var response = await client.SendAsync(request =>
        {
            request
                .WithMethod(HttpMethod.Get)
                .WithUriPath("/secure");
        });

        // Assert
        using (response)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    [Fact]
    public async Task GetSecure_ReturnsUnauthorizedWithIncorrectCredentials()
    {
        // Arrange
        var client = _fixture.CreateClient().Wrap().WithDigestAuthentication("noUser", "noPassword");

        // Act
        var response = await client.SendAsync(request =>
        {
            request
                .WithMethod(HttpMethod.Get)
                .WithUriPath("/secure");
        });

        // Assert
        using (response)
        {
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }

    [Fact]
    public async Task GetSecure_ReturnsOkWithDigestAuthenticationProvider()
    {
        // Arrange
        var client = _fixture.CreateClient().Wrap();

        // Act
        var response = await client
            .WithDigestAuthentication("eddie", "starwars123")
            .SendAsync(request =>
        {
            request
                .WithMethod(HttpMethod.Get)
                .WithUriPath("/secure");
        });

        // Assert
        using (response)
        {
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.Equal("Welcome to the secured area.", content);
        }
    }
}
