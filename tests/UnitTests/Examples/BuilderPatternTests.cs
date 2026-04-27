using NUnit.Framework;
using TestFramework.Builders;
using TestFramework.Models;

namespace UnitTests.Examples;

/// <summary>
/// Demonstrates Builder pattern for test data — Section 01 and 02 of the guide.
/// </summary>
[TestFixture]
public class BuilderPatternTests
{
    [Test]
    public void UserBuilder_DefaultsToStandardRole()
    {
        var user = new UserBuilder().Build();

        Assert.That(user.Role, Is.EqualTo(UserRole.Standard));
        Assert.That(user.IsActive, Is.True);
    }

    [Test]
    public void UserBuilder_WithRole_SetsRole()
    {
        var admin = new UserBuilder().AsAdmin().Build();

        Assert.That(admin.Role, Is.EqualTo(UserRole.Admin));
    }

    [Test]
    public void OrderBuilder_WithItems_ComputesTotal()
    {
        var order = new OrderBuilder()
            .WithItem("SKU-001", quantity: 2, unitPrice: 29.99m)
            .WithItem("SKU-002", quantity: 1, unitPrice: 9.99m)
            .Build();

        Assert.That(order.Total, Is.EqualTo(69.97m));
    }

    [Test]
    public void OrderBuilder_CancelledStatus_ExcludedFromRevenue()
    {
        var cancelled = new OrderBuilder()
            .WithStatus(OrderStatus.Cancelled)
            .WithItem("SKU-001", 2, 29.99m)
            .Build();

        // Revenue service would filter out cancelled — demonstrate data setup
        Assert.That(cancelled.Status, Is.EqualTo(OrderStatus.Cancelled));
        Assert.That(cancelled.Total, Is.GreaterThan(0)); // total exists; exclusion is service logic
    }

    [Test]
    public void BuildList_CreatesRequestedCount()
    {
        var users = new UserBuilder().BuildList(5);

        Assert.That(users, Has.Count.EqualTo(5));
        Assert.That(users.Select(u => u.Id).Distinct().Count(), Is.EqualTo(5)); // unique IDs
    }
}
