using FluentAssertions;
using PlaywrightShowcase.Framework.FrontEnd.PageObject;

namespace PlaywrightShowcase.Test;

public class SampleTest : BaseTest
{
    [Test]
    public async Task Test1()
    {
        var homePage = new HomePage(Page);
        var getStartedPage = await homePage.OpenGetStartedPageAsync();

        var titleSummary = await getStartedPage.GetInstallingPlaywrightSectionTitleAsync();
        titleSummary.Should().Contain("Installing Playwright");
    }
}
