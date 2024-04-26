using Reqnroll;

[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace Integration.Plumbing;

[Binding]
public class TestRunBinding(ScenarioContext scenarioContext)
{
    [BeforeTestRun]
    public static async Task Before()
    {
        Fixture = new IntegrationFixture();
        await Fixture.InitializeAsync();
    }

    [AfterTestRun]
    public static async Task After()
    {
        await Fixture.DisposeAsync();
    }

    [BeforeScenario]
    public async Task BeforeScenario()
    {
        await Fixture.Context!.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }
    
    [AfterScenario]
    public async Task AfterScenario(ScenarioContext scenarioContext, FeatureContext featureContext)
    {
        var feature = featureContext.FeatureInfo.Title;
        var scenario = scenarioContext.ScenarioInfo.Title;
        var filename = $"{feature} - {scenario}";
        // var filename = string.Join("_", scenarioContext.ScenarioInfo.Title.Split(Path.GetInvalidFileNameChars()))
        //     .Replace("<", "_")
        //     .Replace(">", "_")
        //     .Replace("+", ".");
        var path = $"../../../traces/{filename}.zip";
        await Fixture.Context!.Tracing.StopAsync(new()
        {
            Path = path
        });
    }
    
#pragma warning disable CS8618 // BeforeTestRun will guarantee that this field is not null 
    public static IntegrationFixture Fixture { get; private set; }
#pragma warning restore CS8618
}

[Binding]
public class Bindings(ScenarioContext scenarioContext)
{
    [Given(@"I have entered (.*) into the calculator")]
    public void GivenIHaveEnteredIntoTheCalculator(int number)
    {
        scenarioContext["Number1"] = number;
    }

    [BeforeScenario()]
    public void BeforeScenario()
    {
        Console.WriteLine("Starting " + scenarioContext.ScenarioInfo.Title);
    }
}