using NBomber.CSharp;

// https://nbomber.com/
// https://github.com/PragmaticFlow/NBomber
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
                                during: TimeSpan.FromSeconds(40))
        );



var scenarioMem = Scenario.Create("eating Memory", async context =>
{
    var response = await httpClient.GetAsync("/mem");

    await Task.Delay(500);

    return response.IsSuccessStatusCode
        ? Response.Ok()
        : Response.Fail();
})
            .WithoutWarmUp()
            .WithLoadSimulations(
                Simulation.KeepConstant(copies:1, during: TimeSpan.FromSeconds(20))
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
                Simulation.Inject(rate: 3,
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
    .RegisterScenarios(scenarioOs, scenarioCpu, scenarioMem)
    .Run();

Console.WriteLine("Press any key ...");
Console.ReadKey();