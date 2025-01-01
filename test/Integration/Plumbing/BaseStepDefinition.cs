using Microsoft.Playwright;
using Xunit.Abstractions;

namespace Integration.Plumbing;

public abstract class BaseStepDefinition
{
    protected readonly ITestOutputHelper Output;
    protected readonly IntegrationFixture Fixture;
    protected IPage Page;

    protected BaseStepDefinition(ITestOutputHelper output)
    {
        Fixture = TestRunBinding.Fixture;
        Output = output;
        Page = Fixture.Page!;
    }
}