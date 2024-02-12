using Microsoft.AspNetCore.Mvc;
using wsl_and_docker.DI;
using wsl_and_docker.Env;
using wsl_and_docker.Files;
using wsl_and_docker.MemCpu;
using wsl_and_docker.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISampleService, SampleService>();
builder.Services.AddScoped<CpuMemEndpoint>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.MapGet("/weatherforecast", WeatherEndpoint.GetForecast)
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/os", EnvEndpoints.OSName);
app.MapGet("/env", EnvEndpoints.GetEnv);

app.MapGet("/exists", FilesEndpoint.Exists);
app.MapGet("/createDir", FilesEndpoint.CreateDir);
app.MapPost("/log", FilesEndpoint.AppendLog);

// app.MapGet("/cpu", (int? n, [FromServices]CpuMemEndpoint service) => service.DoCalc(n));


app.Run();


