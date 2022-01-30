using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Build Configuration
var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

using IHost host = 

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var appSettings = configurationRoot.GetSection(nameof(AppSettings)).Get<AppSettings>();

Console.WriteLine($"Current Environment:  {appSettings.Environment}");