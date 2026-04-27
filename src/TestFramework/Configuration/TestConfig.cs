using Microsoft.Extensions.Configuration;

namespace TestFramework.Configuration;

public class TestConfig
{
    public string BaseUrl { get; init; } = string.Empty;
    public string ApiBaseUrl { get; init; } = string.Empty;
    public string TestEmail { get; init; } = string.Empty;
    public string TestPassword { get; init; } = string.Empty;
    public string AuthMode { get; init; } = "password";
    public string TestToken { get; init; } = string.Empty;
    public bool Headless { get; init; } = true;

    private static TestConfig? _instance;

    public static TestConfig Instance => _instance ??= Load();

    private static TestConfig Load()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", optional: true)
            .AddJsonFile("testsettings.local.json", optional: true)
            .AddEnvironmentVariables("TEST_")
            .Build();

        return new TestConfig
        {
            BaseUrl = config["BASE_URL"] ?? "http://localhost:3000",
            ApiBaseUrl = config["API_BASE_URL"] ?? "http://localhost:5000",
            TestEmail = config["EMAIL"] ?? "test@example.com",
            TestPassword = config["PASSWORD"] ?? "TestPass1!",
            AuthMode = config["AUTH_MODE"] ?? "password",
            TestToken = config["TOKEN"] ?? string.Empty,
            Headless = Environment.GetEnvironmentVariable("CI") == "true"
        };
    }
}
