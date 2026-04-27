using NUnit.Framework;
using TestFramework.Builders;
using TestFramework.Models;

namespace UnitTests.Examples;

/// <summary>
/// Demonstrates LINQ patterns in assertions — Section 01 of the guide.
/// </summary>
[TestFixture]
public class LinqAssertionTests
{
    private List<Order> _orders = null!;

    [SetUp]
    public void SetUp()
    {
        _orders = new List<Order>
        {
            new OrderBuilder().WithStatus(OrderStatus.Delivered).WithItem("A", 1, 10m).Build(),
            new OrderBuilder().WithStatus(OrderStatus.Delivered).WithItem("B", 2, 25m).Build(),
            new OrderBuilder().WithStatus(OrderStatus.Cancelled).WithItem("C", 1, 15m).Build(),
            new OrderBuilder().WithStatus(OrderStatus.Pending).WithItem("D", 1, 5m).Build(),
        };
    }

    [Test]
    public void AllDeliveredOrders_HavePositiveTotal()
    {
        var delivered = _orders.Where(o => o.Status == OrderStatus.Delivered).ToList();

        Assert.That(delivered.All(o => o.Total > 0), Is.True);
    }

    [Test]
    public void Orders_ExcludeCancelled_WhenFilteredForRevenue()
    {
        var revenueOrders = _orders
            .Where(o => o.Status != OrderStatus.Cancelled)
            .ToList();

        Assert.That(revenueOrders, Has.Count.EqualTo(3));
        Assert.That(revenueOrders.Any(o => o.Status == OrderStatus.Cancelled), Is.False);
    }

    [Test]
    public void MaterializeBeforeMultipleAssertions_AvoidDoubleIteration()
    {
        // GOOD: materialize once
        var results = _orders.Where(o => o.Total > 10m).ToList();

        Assert.That(results, Is.Not.Empty);
        Assert.That(results.All(o => o.Total > 10m), Is.True);
    }
}
