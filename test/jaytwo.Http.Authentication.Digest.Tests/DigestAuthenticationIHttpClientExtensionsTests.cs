using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace jaytwo.Http.Authentication.Digest.Tests;

public class DigestAuthenticationIHttpClientExtensionsTests
{
    [Fact]
    public void WithAuthentication_Authentication_Provider()
    {
        // Arrange
        var user = "hello";
        var pass = "world";
        var mock = new Mock<IHttpClient>();

        // Act
        var wrapped = mock.Object.WithDigestAuthentication(user, pass);

        // Assert
        var typed = Assert.IsType<AuthenticationWrapper>(wrapped);
        var auth = Assert.IsType<DigestAuthenticationProvider>(typed.AuthenticationProvider);
        Assert.Same(mock.Object, auth.HttpClient);
        Assert.Equal(user, auth.Username);
        Assert.Equal(pass, auth.Password);
    }
}
