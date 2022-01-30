using Microsoft.Extensions.Configuration;

// Build Configuration
var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

var configurationRoot = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables()
    .Build();

var appSettings = configurationRoot.GetSection(nameof(AppSettings)).Get<AppSettings>();

Console.WriteLine($"Current Environment:  {appSettings.Environment}");