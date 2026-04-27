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

    [SetUp]
    public virtual void SetUp()
    {
        var options = new RestClientOptions(TestConfig.Instance.ApiBaseUrl)
        {
            ThrowOnAnyError = false,
            MaxTimeout = 30_000
        };
        Client = new RestClient(options);
    }

    [TearDown]
    public virtual void TearDown() => Client?.Dispose();
}
