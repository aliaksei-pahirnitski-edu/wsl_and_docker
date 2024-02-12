using NBomber.CSharp;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("http://localhost:5049/");

var scenarioOs = Scenario.Create("get_os_name", async context =>
{
    var response = await httpClient.GetAsync("/os/");

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
        .WithoutWarmUp()
        .WithLoadSimulations(
            Simulation.RampingInject(rate: 5,
                                interval: TimeSpan.FromSeconds(1),
                                during: TimeSpan.FromSeconds(30))
        );



var scenarioCpu = Scenario.Create("eating CPU", async context =>
{
    var response = await httpClient.GetAsync("/cpu");

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 2,
                                  interval: TimeSpan.FromSeconds(1),
                                  during: TimeSpan.FromSeconds(30))
            );
/*
var scenarioWeather = Scenario.Create("get_weatherforecast", async context =>
{
    var response = await httpClient.GetAsync("/weatherforecast/");

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.Inject(rate: 2,
                                  interval: TimeSpan.FromSeconds(1),
                                  during: TimeSpan.FromSeconds(30))
            );
*/

NBomberRunner
    .RegisterScenarios(scenarioOs, scenarioCpu)
    .Run();

Console.WriteLine("Press any key ...");
Console.ReadKey();