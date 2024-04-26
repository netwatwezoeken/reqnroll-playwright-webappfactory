using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Playwright;

namespace Integration.Plumbing;

public class IntegrationFixture
{
    private readonly CustomWebApplicationFactory _factory;

    public IPlaywright? Playwright;
    public IBrowser? Browser;
    public IBrowserContext? Context;
    
    public readonly HttpClient HttpClient;

    public IPage? Page { get; set; }

    public IntegrationFixture()
    {
        _factory = new CustomWebApplicationFactory();
                
        HttpClient = _factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
    }
    
    public virtual async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {   
            // See integration.json to permanently configure Headless on your local machine
            // run git update-index --skip-worktree src/Main/integration.json so that
            // a change to that file will not be automatically added to your commits
            Headless = false, //_factory.Headless,
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