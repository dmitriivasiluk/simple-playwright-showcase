namespace PlaywrightShowcase.Configuration.DTO;

public class RunConfiguration
{
    public const string ConfigurationKey = "configuration";
    public string TestEnvironment { get; set; } = string.Empty;
    public string BrowserUnderTest { get; set; } = string.Empty;
    public bool IsHeadlessBrowser { get; set; }
}