using Integration.Plumbing;
using Microsoft.Playwright;
using Reqnroll;
using Xunit.Abstractions;

namespace Integration;

[Binding]
public class WeatherStepDefinition(ITestOutputHelper output) : BaseStepDefinition(output)
{
    [Given(@"the weather page is visible")]
    public async Task GivenTheCounterPageIsVisibleX()
    {
        await Page.GotoAsync("https://localhost:7048/");
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Weather" }).ClickAsync();
    }
    
    [Then(@"{int} locations are shown")]
    public async Task ThenCounterIs(int counter)
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Assert.Equal(5,await Page.Locator("tr").CountAsync()-1);
    }
}