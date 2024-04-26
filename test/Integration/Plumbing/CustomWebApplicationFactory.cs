using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace Integration.Plumbing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        var dummyHost = builder.Build();
        builder.ConfigureWebHost(webHostBuilder => webHostBuilder.UseKestrel());
        var host = builder.Build();
        host.Start();
        return dummyHost;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseUrls("https://localhost:7048");
        builder.UseEnvironment("Integration")
            .UseStaticWebAssets() // this makes sure the assest from the Presentation project are loaded
            .ConfigureTestServices(services =>
            {
                Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Integration");
            });
    }
}