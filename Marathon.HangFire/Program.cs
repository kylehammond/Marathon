using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var environment = Environment.GetEnvironmentVariable("ENVIRONMENT");
var builder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
    .AddJsonFile($"appsettings.{environment}.json", true, true)
    .AddEnvironmentVariables();

var configurationRoot = builder.Build();
var appSettings = configurationRoot.GetSection(nameof(AppSettings)).Get<AppSettings>();

Console.WriteLine(appSettings.Environment);