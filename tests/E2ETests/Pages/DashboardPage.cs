using Microsoft.Playwright;

namespace E2ETests.Pages;

public class DashboardPage : BasePage
{
    private const string WelcomeHeading = "[data-testid='welcome-heading']";
    private const string UserMenuButton = "[data-testid='user-menu']";
    private const string LogoutButton = "[data-testid='logout-btn']";

    public DashboardPage(IPage page, string baseUrl) : base(page, baseUrl) { }

    public async Task<string> GetWelcomeTextAsync()
        => await GetTextAsync(WelcomeHeading);

    public async Task<LoginPage> LogoutAsync()
    {
        await Page.ClickAsync(UserMenuButton);
        await Page.ClickAsync(LogoutButton);
        await WaitForUrlAsync("/login");
        return new LoginPage(Page, BaseUrl);
    }
}
