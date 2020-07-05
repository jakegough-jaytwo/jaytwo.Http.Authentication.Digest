using System;
using System.Collections.Generic;
using System.Text;
using jaytwo.SolutionResolution;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace jaytwo.Http.Authentication.Digest.Tests
{
    public class DigestSampleAppWebApplicationFactory
        : WebApplicationFactory<DigestSampleApp.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var contentRoot = new SlnFileResolver().ResolvePathRelativeToSln("test/DigestSampleApp.Tests");
            builder.UseContentRoot(contentRoot);
        }
    }
}
