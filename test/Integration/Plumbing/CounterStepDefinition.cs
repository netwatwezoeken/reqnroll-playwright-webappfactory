using Microsoft.Playwright;
using Reqnroll;
using Xunit.Abstractions;

namespace Integration.Plumbing;

[Binding]
public class CounterStepDefinition(ITestOutputHelper output) : BaseStepDefinition(output)
{
    [Given(@"the counter page is visible and counter is {int}")]
    public async Task GivenTheCounterPageIsVisibleX(int counter)
    {
        await Page.GotoAsync("https://localhost:7048/");
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "Counter" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        var counterText = await Page.Locator("p").TextContentAsync();
        Assert.Equal($"Current count: {counter}", counterText);
    }

    [When(@"button is clicked")]
    public async Task WhenButtonIsClicked()
    {
        await Page.GetByRole(AriaRole.Button, 
            new PageGetByRoleOptions { Name = "Click me" }).ClickAsync();
    }

    [Then(@"counter is {int}")]
    public async Task ThenCounterIs(int counter)
    {
        var counterText = await Page.Locator("p").TextContentAsync();
        Assert.Equal($"Current count: {counter}", counterText);
    }
}