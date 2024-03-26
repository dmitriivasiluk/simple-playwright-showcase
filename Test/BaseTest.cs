using Bogus;
using Microsoft.Playwright;
using NUnit.Framework.Interfaces;
using PlaywrightShowcase.Configuration;

namespace PlaywrightShowcase.Test;

public class BaseTest
{
    public Faker Faker;
    public TestSettings TestSettings;
    public IBrowser Browser;
    public IBrowserContext BrowserContext;
    public IPage Page;
    private readonly string LogsFolder = "logs";
    private int DefaultTimeout = 5000;

    public BaseTest()
    {
        TestSettings = new TestSettings();
        Faker = new Faker();
    }

    [SetUp]
    public async Task BeforeTest()
    {
        var playwright = await Playwright.CreateAsync();

        Browser = await GetBrowser(
            TestSettings.RunConfiguration.BrowserUnderTest, 
            playwright, 
            TestSettings.RunConfiguration.IsHeadlessBrowser);
        BrowserContext = await Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                BaseURL = "https://playwright.dev/"
            });
        BrowserContext.SetDefaultTimeout(DefaultTimeout);
        await BrowserContext.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        Page = await BrowserContext.NewPageAsync();
        await Page.GotoAsync("https://playwright.dev/");
    }

    [TearDown]
    public async Task AfterTest()
    {
        await ManageArtifacts();

        await Browser!.CloseAsync();
    }

    private async Task ManageArtifacts()
    {
        await BrowserContext.Tracing.StopAsync(new()
        {
            Path = $"{LogsFolder}/{TestContext.CurrentContext.Test.Name}-trace.zip"
        });

        if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
        {
            if (!Directory.Exists(LogsFolder))
            {
                Directory.CreateDirectory(LogsFolder);
            }

            await Page.ScreenshotAsync(new()
            {
                Path = Path.Combine(LogsFolder, $"{TestContext.CurrentContext.Test.Name}.png"),
                FullPage = true,
            });
        }
    }

    private async Task<IBrowser> GetBrowser(string browserName, IPlaywright playwright, bool isHeadless = true)
    {
        var options = new BrowserTypeLaunchOptions()
        {
            Headless = isHeadless,
            Args = ["--start-maximized"],
            TracesDir = LogsFolder
        };
        switch (browserName)
        {
            case "Chrome":
                options.Channel = "chrome";
                return await playwright.Chromium.LaunchAsync(options);

            case "Edge":
                options.Channel = "msedge";
                return await playwright.Chromium.LaunchAsync(options);

            default:
                throw new Exception("Currently we support only Chrome or Edge browsers!");
        }
    }
}