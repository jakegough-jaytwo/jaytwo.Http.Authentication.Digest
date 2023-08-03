# jaytwo.Http.Authentication.Digest

<p align="center">
  <a href="https://jenkins.jaytwo.com/job/github-jakegough-jaytwo/job/jaytwo.Http.Authentication.Digest/job/master/" alt="Build Status (master)">
    <img src="https://jenkins.jaytwo.com/buildStatus/icon?job=github-jakegough-jaytwo%2Fjaytwo.Http.Authentication.Digest%2Fmaster&subject=build%20(master)" /></a>
  <a href="https://jenkins.jaytwo.com/job/github-jakegough-jaytwo/job/jaytwo.Http.Authentication.Digest/job/develop/" alt="Build Status (develop)">
    <img src="https://jenkins.jaytwo.com/buildStatus/icon?job=github-jakegough-jaytwo%2Fjaytwo.Http.Authentication.Digest%2Fdevelop&subject=build%20(develop)" /></a>
</p>

<p align="center">
  <a href="https://www.nuget.org/packages/jaytwo.Http.Authentication.Digest/" alt="NuGet Package jaytwo.Http.Authentication.Digest">
    <img src="https://img.shields.io/nuget/v/jaytwo.Http.Authentication.Digest.svg?logo=nuget&label=jaytwo.Http.Authentication.Digest" /></a>
  <a href="https://www.nuget.org/packages/jaytwo.Http.Authentication.Digest/" alt="NuGet Package jaytwo.Http.Authentication.Digest (beta)">
    <img src="https://img.shields.io/nuget/vpre/jaytwo.Http.Authentication.Digest.svg?logo=nuget&label=jaytwo.Http.Authentication.Digest" /></a>
</p>

## Installation

Add the NuGet package

```
PM> Install-Package jaytwo.Http.Authentication.Digest
```

## Usage

This builds on the `IHttpClient` and `IAuthenticationProvider` abstractions from the `jaytwo.Http` and `jaytwo.Http.Authentication` packages.   This is meant for use with the `jaytwo.FluentHttp` package.

It's worth noting that digest auth is pretty much dead these days, but occasionally you find an integration that needs it.

```csharp
// digest auth
var httpClient = new HttpClient().Wrap().WithDigestAuthentication("user", "pass");
```

---

Made with &hearts; by Jake
