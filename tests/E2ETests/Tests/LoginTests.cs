using E2ETests.Base;
using E2ETests.Pages;
using NUnit.Framework;

namespace E2ETests.Tests;

/// <summary>
/// E2E login tests using Playwright POM — Section 02 of the guide.
/// Update selectors and BASE_URL in testsettings.local.json to target your app.
/// </summary>
[TestFixture, Category("E2E")]
public class LoginTests : PlaywrightTestBase
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public async Task SetUp()
    {
        _loginPage = new LoginPage(Page, BaseUrl);
        await _loginPage.NavigateAsync();
    }

    [Test]
    public async Task Login_WithValidCredentials_RedirectsToDashboard()
    {
        var dashboard = await _loginPage.LoginAsync("user@test.com", "ValidPass1!");

        var welcome = await dashboard.GetWelcomeTextAsync();
        Assert.That(welcome, Does.Contain("Welcome"));
    }

    [Test]
    public async Task Login_WithWrongPassword_ShowsErrorMessage()
    {
        await _loginPage.LoginExpectingFailureAsync("user@test.com", "wrongpassword");

        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Is.Not.Empty);
    }

    [Test]
    public async Task Login_WithEmptyEmail_ShowsValidationError()
    {
        await _loginPage.LoginExpectingFailureAsync(string.Empty, "ValidPass1!");

        var error = await _loginPage.GetErrorMessageAsync();
        Assert.That(error, Is.Not.Empty);
    }

    [Test]
    public async Task Login_ThenLogout_ReturnsToLoginPage()
    {
        var dashboard = await _loginPage.LoginAsync("user@test.com", "ValidPass1!");
        var loginPage = await dashboard.LogoutAsync();

        Assert.That(Page.Url, Does.Contain("/login"));
    }
}
