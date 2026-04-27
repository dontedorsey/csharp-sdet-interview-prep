using NUnit.Framework;

namespace TestFramework.Helpers;

public static class AssertHelper
{
    public static void ContainsExactly<T>(IEnumerable<T> collection, int expectedCount)
    {
        var list = collection.ToList();
        Assert.That(list, Has.Count.EqualTo(expectedCount),
            $"Expected {expectedCount} item(s) but found {list.Count}");
    }

    public static void IsNotNullOrEmpty(string? value, string fieldName = "value")
    {
        Assert.That(value, Is.Not.Null.And.Not.Empty,
            $"Expected '{fieldName}' to be non-empty");
    }

    public static void IsValidGuid(string? value, string fieldName = "id")
    {
        Assert.That(Guid.TryParse(value, out _), Is.True,
            $"Expected '{fieldName}' to be a valid GUID, got: {value}");
    }

    public static void IsWithinLast(DateTime value, TimeSpan window, string fieldName = "timestamp")
    {
        var age = DateTime.UtcNow - value.ToUniversalTime();
        Assert.That(age, Is.LessThanOrEqualTo(window),
            $"Expected '{fieldName}' to be within the last {window}, but it was {age} ago");
    }
}
