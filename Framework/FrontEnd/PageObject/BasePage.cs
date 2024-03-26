using Microsoft.Playwright;

namespace PlaywrightShowcase.Framework.FrontEnd.PageObject;

public abstract class BasePage(IPage page)
{
    public IPage Page = page;
    public IPageAssertions Expect(IPage page) => Assertions.Expect(page);
    public ILocatorAssertions Expect(ILocator locator) => Assertions.Expect(locator);
}