using Microsoft.Playwright;

namespace E2ETests.Pages;

/// <summary>
/// Page Object for the login screen — Section 02 of the guide.
/// Two login methods encode expected navigation in the return type.
/// </summary>
public class LoginPage : BasePage
{
    private const string EmailInput    = "[data-testid='email-input']";
    private const string PasswordInput = "[data-testid='password-input']";
    private const string SubmitButton  = "[data-testid='login-submit']";
    private const string ErrorBanner   = "[data-testid='login-error']";

    public LoginPage(IPage page, string baseUrl) : base(page, baseUrl) { }

    public async Task NavigateAsync()
        => await Page.GotoAsync($"{BaseUrl}/login");

    /// <summary>Happy path — returns DashboardPage after successful login.</summary>
    public async Task<DashboardPage> LoginAsync(string email, string password)
    {
        await Page.FillAsync(EmailInput, email);
        await Page.FillAsync(PasswordInput, password);
        await Page.ClickAsync(SubmitButton);
        await WaitForUrlAsync("/dashboard");
        return new DashboardPage(Page, BaseUrl);
    }

    /// <summary>Failure path — stays on LoginPage; caller asserts error state.</summary>
    public async Task<LoginPage> LoginExpectingFailureAsync(string email, string password)
    {
        await Page.FillAsync(EmailInput, email);
        await Page.FillAsync(PasswordInput, password);
        await Page.ClickAsync(SubmitButton);
        return this;
    }

    public async Task<string> GetErrorMessageAsync()
        => await GetTextAsync(ErrorBanner);
}
