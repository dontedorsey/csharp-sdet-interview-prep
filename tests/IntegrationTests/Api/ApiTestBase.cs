using NUnit.Framework;
using RestSharp;
using TestFramework.Configuration;

namespace IntegrationTests.Api;

/// <summary>
/// Base class for RestSharp API tests — Section 05 of the guide.
/// Replace ApiBaseUrl in testsettings.local.json before running.
/// </summary>
[TestFixture]
public abstract class ApiTestBase
{
    protected RestClient Client = null!;

    [OneTimeSetUp]
    public void SkipIfNotConfigured()
    {
        if (TestConfig.Instance.ApiBaseUrl.Contains("localhost"))
            Assert.Ignore("TEST_API_BASE_URL not configured — skipping integration tests (set the secret to run against a real API)");
    }

    [SetUp]
    public virtual void SetUp()
    {
        var options = new RestClientOptions(TestConfig.Instance.ApiBaseUrl)
        {
            ThrowOnAnyError = false,
            Timeout = TimeSpan.FromSeconds(30)
        };
        Client = new RestClient(options);
    }

    [TearDown]
    public virtual void TearDown() => Client?.Dispose();
}
