using System;
using System.Net.Http;
using jaytwo.Http.Authentication.Digest;

namespace jaytwo.Http;

public static class DigestAuthenticationIHttpClientExtensions
{
    public static IHttpClient WithDigestAuthentication(this IHttpClient httpClient, string user, string pass)
        => httpClient.WithAuthentication(new DigestAuthenticationProvider(httpClient, user, pass));
}
