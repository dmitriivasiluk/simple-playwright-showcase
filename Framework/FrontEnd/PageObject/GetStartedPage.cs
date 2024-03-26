using Microsoft.Playwright;
namespace PlaywrightShowcase.Framework.FrontEnd.PageObject;

public class GetStartedPage(IPage page) : BasePage(page)
{
    private ILocator InstallingPlayrightSectionTitle => Page.Locator("#installing-playwright");
    public async Task<string> GetInstallingPlaywrightSectionTitleAsync()
    {
        return (await InstallingPlayrightSectionTitle.TextContentAsync())!;
    }
}
