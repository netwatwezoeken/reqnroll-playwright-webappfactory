using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Integration.Plumbing;

internal class CustomWebApplicationFactory : Nwwz.Mvc.Testing.WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseStaticWebAssets() // this makes sure the assest from the Presentation project are loaded
            .ConfigureTestServices(services =>
            {
                // Override service for testing here
            });
    }
}