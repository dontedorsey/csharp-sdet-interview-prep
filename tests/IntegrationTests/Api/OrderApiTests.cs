using NUnit.Framework;
using RestSharp;

namespace IntegrationTests.Api;

/// <summary>
/// Example API test structure — adapt endpoints to match your target API.
/// Demonstrates GET, POST, and validation error patterns from Section 05.
/// </summary>
[TestFixture, Category("Integration")]
public class OrderApiTests : ApiTestBase
{
    // ── Replace these with real response models from your API ─────────────
    private record OrderResponse(string Id, string Status, string CustomerId);
    private record CreateOrderRequest(string CustomerId, string[] Skus);
    private record ValidationErrorResponse(Dictionary<string, string[]> Errors);
    // ─────────────────────────────────────────────────────────────────────

    [Test]
    public async Task GetOrder_WithValidId_Returns200()
    {
        var request = new RestRequest("/api/orders/{id}")
            .AddUrlSegment("id", "ORD-001");

        var response = await Client.ExecuteGetAsync<OrderResponse>(request);

        Assert.That((int)response.StatusCode, Is.EqualTo(200));
        Assert.That(response.Data, Is.Not.Null);
        Assert.That(response.Data!.Id, Is.EqualTo("ORD-001"));
    }

    [Test]
    public async Task GetOrder_WithNonExistentId_Returns404()
    {
        var request = new RestRequest("/api/orders/{id}")
            .AddUrlSegment("id", "DOES-NOT-EXIST");

        var response = await Client.ExecuteGetAsync(request);

        Assert.That((int)response.StatusCode, Is.EqualTo(404));
    }

    [Test]
    public async Task CreateOrder_WithValidPayload_Returns201()
    {
        var request = new RestRequest("/api/orders", Method.Post)
            .AddJsonBody(new CreateOrderRequest("CUST-42", new[] { "SKU-001" }));

        var response = await Client.ExecutePostAsync<OrderResponse>(request);

        Assert.That((int)response.StatusCode, Is.EqualTo(201));
        Assert.That(response.Data!.CustomerId, Is.EqualTo("CUST-42"));
    }

    [Test]
    public async Task CreateOrder_WithMissingCustomerId_Returns400WithErrors()
    {
        var request = new RestRequest("/api/orders", Method.Post)
            .AddJsonBody(new { Skus = new[] { "SKU-001" } }); // missing CustomerId

        var response = await Client.ExecutePostAsync<ValidationErrorResponse>(request);

        Assert.That((int)response.StatusCode, Is.EqualTo(400));
        Assert.That(response.Data!.Errors, Contains.Key("customerId"));
    }
}
