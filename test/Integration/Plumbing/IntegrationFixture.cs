using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright;

namespace Integration.Plumbing;

public class IntegrationFixture
{
    private readonly CustomWebApplicationFactory _factory;

    public string ServerAddress => _factory.ServerAddress;
    
    public IPlaywright? Playwright;
    public IBrowser? Browser;
    public IBrowserContext? Context;
    
    public readonly HttpClient HttpClient;

    public IPage? Page { get; set; }

    public IntegrationFixture()
    {
        _factory = new CustomWebApplicationFactory();
                
        HttpClient = _factory.CreateClient(
            new Nwwz.Mvc.Testing.WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
    }
    
    public virtual async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            //SlowMo = 300
        });
        Context = await Browser.NewContextAsync(new BrowserNewContextOptions()
        {   
            IgnoreHTTPSErrors = true
        });
        
        Page = await Context.NewPageAsync();
    }

    public virtual async Task DisposeAsync()
    {
        await Context!.DisposeAsync();
        Playwright!.Dispose();
    }
}