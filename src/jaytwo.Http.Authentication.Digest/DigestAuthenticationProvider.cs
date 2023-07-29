using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using jaytwo.FluentHttp;

namespace jaytwo.Http.Authentication.Digest;

public class DigestAuthenticationProvider : AuthenticationProviderBase, IAuthenticationProvider
{
    public DigestAuthenticationProvider(IHttpClient httpClient, string username, string pass)
    {
        HttpClient = httpClient;
        Username = username;
        Password = pass;
    }

    internal Func<string> NonceFactory { get; set; } = () => Guid.NewGuid().ToString();

    protected internal IHttpClient HttpClient { get; private set; }

    protected internal string Username { get; private set; }

    protected internal string Password { get; private set; }

    public override async Task AuthenticateAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var digestServerParams = await GetDigestServerParams(request.RequestUri, cancellationToken);

        var clientNonce = NonceFactory.Invoke();
        var nonceCount = 1;
        var authorizationHeaderValue = await GetDigesAuthorizationtHeaderAsync(digestServerParams, request, clientNonce, nonceCount);

        SetRequestAuthenticationHeader(request, authorizationHeaderValue);
    }

    internal async Task<DigestServerParams> GetDigestServerParams(Uri uri, CancellationToken cancellationToken)
    {
        using var unauthenticatedRequest = new HttpRequestMessage(HttpMethod.Get, uri);
        using var unauthenticatedResponse = await HttpClient.SendAsync(unauthenticatedRequest, cancellationToken);

        var wwwAuthenticateHeader = unauthenticatedResponse
            .EnsureExpectedStatusCode(HttpStatusCode.Unauthorized)
            .GetHeaderValue("www-authenticate");

        return DigestServerParams.Parse(wwwAuthenticateHeader);
    }

    internal async Task<string> GetDigesAuthorizationtHeaderAsync(DigestServerParams digestServerParams, HttpRequestMessage request, string clientNonce, int nonceCount)
    {
        var uri = request.RequestUri.IsAbsoluteUri ? request.RequestUri.PathAndQuery : request.RequestUri.OriginalString;

        var nonceCountAsString = $"{nonceCount}".PadLeft(8, '0'); // padleft not strictly necessary, but it makes the documented example work
        var response = await DigestCalculator.GetResponseAsync(digestServerParams, request, uri, Username, Password, clientNonce, nonceCountAsString);

        var data = new Dictionary<string, string>()
        {
            { "username",  Username },
            { "realm",  digestServerParams.Realm },
            { "nonce", digestServerParams.Nonce },
            { "uri", uri },
            { "response", response },
            { "qop", digestServerParams.Qop },
            { "nc", nonceCountAsString },
            { "cnonce", clientNonce },
            { "opaque", digestServerParams.Opaque },
        };

        var result = DigestServerParams.SerializeDictionary(data);
        return result;
    }
}
