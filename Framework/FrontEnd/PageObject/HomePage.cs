using Microsoft.Playwright;

namespace PlaywrightShowcase.Framework.FrontEnd.PageObject;

public class HomePage(IPage page) : BasePage(page)
{
    private ILocator GetStartedButton => Page.Locator("//a[contains(text(), 'Get started')]");

    public async Task<GetStartedPage> OpenGetStartedPageAsync()
    {
        await GetStartedButton.ClickAsync();

        return new GetStartedPage(Page);
    }
}
