using Integration.Plumbing;
using Microsoft.Playwright;
using Reqnroll;
using Xunit.Abstractions;

namespace Integration;

[Binding]
public class WeatherStepDefinition(ITestOutputHelper output) : BaseStepDefinition(output)
{
    [Given(@"the weather page is visible")]
    public async Task GivenTheWeatherPageIsVisible()
    {
        await Page.GotoAsync(Fixture.ServerAddress);
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Weather" }).ClickAsync();
    }
    
    [Then(@"(.*) locations are shown")]
    public async Task ThenLocationsAreShown(int counter)
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        Assert.Equal(5,await Page.Locator("tr").CountAsync()-1);
    }
}