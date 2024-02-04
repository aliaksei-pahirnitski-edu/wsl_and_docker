using wsl_and_docker.Env;
using wsl_and_docker.Files;
using wsl_and_docker.MemCpu;
using wsl_and_docker.Weather;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/cpu", CpuEndpoint.DoCalc);


app.Run();


