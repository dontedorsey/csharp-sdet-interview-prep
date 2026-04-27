using Microsoft.Playwright;

namespace E2ETests.Pages;

public abstract class BasePage
{
    protected readonly IPage Page;
    protected readonly string BaseUrl;

    protected BasePage(IPage page, string baseUrl)
    {
        Page = page;
        BaseUrl = baseUrl;
    }

    protected async Task WaitForUrlAsync(string urlFragment)
        => await Page.WaitForURLAsync($"**{urlFragment}**");

    protected async Task<string> GetTextAsync(string selector)
        => await Page.TextContentAsync(selector) ?? string.Empty;
}
