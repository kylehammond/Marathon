using Marathon.HangFire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

// Configure config builder
var builder = new ConfigurationBuilder();
BuildConfiguration(builder);

// Configure Serilog logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build()) // lets serilog read from appsettings
    .Enrich.FromLogContext() // add more serilog things to log
    .WriteTo.Console() // write to console for logs
    .CreateLogger(); // create the logger

// Configure host + services + DI
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IGreetingService, GreetingService>();
        services.AddSingleton(Log.Logger);
    })
    .UseSerilog()
    .Build();

var service = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
service.Run();

Console.ReadLine();

static void BuildConfiguration(IConfigurationBuilder builder)
{
    builder
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();
}