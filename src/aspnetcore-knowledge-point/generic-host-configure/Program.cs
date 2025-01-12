public class Program
{
    public static async Task Main(string[] args)
    {
        await new HostBuilder()
            .ConfigureHostConfiguration(configHost =>
            {
                // Add environment variables
                configHost.AddEnvironmentVariables();

                // Add command-line arguments
                configHost.AddCommandLine(args);

                // Add in-memory configuration for host-level
                configHost.AddInMemoryCollection(new Dictionary<string, string>
                {
                     { "Application.Name", "App-01" },
                     { "Application.Version", "1.0.0" }
                });
            })
            .ConfigureAppConfiguration((hostContext, configApp) =>
            {
                // Add JSON file configuration
                configApp.AddJsonFile("appsettings.json", optional: true);

                // Add environment variables for app-level
                configApp.AddEnvironmentVariables();

                // Add in-memory configuration for app-level
                configApp.AddInMemoryCollection(new Dictionary<string, string>
                {
                     { "Application.Name", "App-01" },
                     { "Application.Version", "1.0.0" }
                });
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Register the hosted service
                services.AddHostedService<SampleHostedService>();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
                logging.AddFilter((provider, category, logLevel) => !category.Contains("Microsoft"));
            })
            .Build()
            .RunAsync();
    }
}
public class SampleHostedService : IHostedService
{
    private readonly ILogger<SampleHostedService> _logger;
    private readonly IConfiguration _config;

    public SampleHostedService(IConfiguration config, ILogger<SampleHostedService> logger)
    {
        _config = config;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("=== Testing Configuration Priority ===");

        // Enumerate all configuration keys and their values
        foreach (var kvp in _config.AsEnumerable().OrderBy(k => k.Key))
        {
            _logger.LogDebug($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // If IConfigurationRoot is available, list all providers
        if (_config is IConfigurationRoot configRoot)
        {
            _logger.LogDebug("=== Configuration Providers ===");
            foreach (var provider in configRoot.Providers)
            {
                _logger.LogDebug($"Provider: {provider}");
            }
        }

        _logger.LogDebug("=== End of Configuration Test ===");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogDebug("End of Host");
        return Task.CompletedTask;
    }
}
