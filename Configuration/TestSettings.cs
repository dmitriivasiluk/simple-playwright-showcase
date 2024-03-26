using Microsoft.Extensions.Configuration;
using RunConfiguration = PlaywrightShowcase.Configuration.DTO.RunConfiguration;

namespace PlaywrightShowcase.Configuration;

public class TestSettings
{
    private static readonly IConfiguration _config = new ConfigurationBuilder()
           .AddJsonFile(Path.Combine("Configuration", "appsettings.json"))
           .AddEnvironmentVariables()
           .Build();

    public RunConfiguration RunConfiguration;

    public TestSettings()
    {
        RunConfiguration = new RunConfiguration();
        _config.GetSection(RunConfiguration.ConfigurationKey).Bind(RunConfiguration);
    }
}