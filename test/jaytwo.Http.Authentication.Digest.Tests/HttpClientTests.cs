using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using jaytwo.FluentHttp;
using Xunit;
using Xunit.Abstractions;

namespace jaytwo.Http.Authentication.Digest.Tests;

public class HttpClientTests
{
    private readonly ITestOutputHelper _output;

    public HttpClientTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData("auth", "MD5")]
    [InlineData("auth-int", "MD5")]
    // not worth supporting, not even postman or mozilla supports RFC 7616
    //[InlineData("auth", "SHA-256")]
    //[InlineData("auth", "SHA-512")]
    //[InlineData("auth-int", "SHA-256")]
    //[InlineData("auth-int", "SHA-512")]
    public async Task DigestAuth_Works(string qop, string algorithm)
    {
        // arrange
        using var httpClient = new HttpClient().Wrap();
        var user = "hello";
        var pass = "world";

        // act
        var response = await httpClient
            .WithDigestAuthentication(user, pass)
            .SendAsync(request =>
            {
                request
                    .WithMethod(HttpMethod.Get)
                    .WithBaseUri("http://httpbin.jaytwo.com") // full url is required in the request
                    .WithUriPath($"/digest-auth/{qop}/{user}/{pass}/{algorithm}");
            });

        // assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
