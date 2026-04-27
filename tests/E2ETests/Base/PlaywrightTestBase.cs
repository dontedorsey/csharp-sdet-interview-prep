using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using TestFramework.Configuration;

namespace E2ETests.Base;

/// <summary>
/// Base class for all Playwright E2E tests — Section 02 and 04 of the guide.
/// Headless in CI (CI=true env var), headed locally.
/// Captures trace on test failure for debugging.
/// </summary>
[TestFixture]
public abstract class PlaywrightTestBase : PageTest
{
    protected string BaseUrl => TestConfig.Instance.BaseUrl;

    [OneTimeSetUp]
    public void SkipIfNotConfigured()
    {
        if (TestConfig.Instance.BaseUrl.Contains("localhost"))
            Assert.Ignore("TEST_BASE_URL not configured — skipping E2E tests (set STAGING_BASE_URL secret to run against a real app)");
    }

    public override BrowserNewContextOptions ContextOptions() => new()
    {
        ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
        RecordVideoDir = TestContext.CurrentContext.Result.Outcome.Status
            == NUnit.Framework.Interfaces.TestStatus.Failed ? "videos/" : null
    };

    [SetUp]
    public async Task StartTracing()
    {
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    [TearDown]
    public async Task StopTracing()
    {
        var failed = TestContext.CurrentContext.Result.Outcome.Status
            == NUnit.Framework.Interfaces.TestStatus.Failed;

        var tracePath = failed
            ? $"playwright-traces/{TestContext.CurrentContext.Test.Name}.zip"
            : null;

        await Context.Tracing.StopAsync(new TracingStopOptions { Path = tracePath });
    }
}
