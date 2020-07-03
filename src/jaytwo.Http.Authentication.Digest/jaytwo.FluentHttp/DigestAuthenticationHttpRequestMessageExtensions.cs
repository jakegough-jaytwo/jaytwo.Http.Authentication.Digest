using System;
using System.Net.Http;
using jaytwo.Http.Authentication.Digest;

namespace jaytwo.FluentHttp
{
    public static class DigestAuthenticationHttpRequestMessageExtensions
    {
        public static HttpRequestMessage WithDigestAuthentication(this HttpRequestMessage httpRequestMessage, HttpClient httpClient, string user, string pass)
        {
            return httpRequestMessage.WithAuthentication(new DigestAuthenticationProvider(httpClient, user, pass));
        }
    }
}
