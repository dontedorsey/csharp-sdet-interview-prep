namespace TestFramework.Helpers;

public static class AssertHelper
{
    public static void ContainsExactly<T>(IEnumerable<T> collection, int expectedCount)
    {
        var list = collection.ToList();
        if (list.Count != expectedCount)
            throw new AssertionException($"Expected {expectedCount} item(s) but found {list.Count}");
    }

    public static void IsNotNullOrEmpty(string? value, string fieldName = "value")
    {
        if (string.IsNullOrEmpty(value))
            throw new AssertionException($"Expected '{fieldName}' to be non-empty");
    }

    public static void IsValidGuid(string? value, string fieldName = "id")
    {
        if (!Guid.TryParse(value, out _))
            throw new AssertionException($"Expected '{fieldName}' to be a valid GUID, got: {value}");
    }

    public static void IsWithinLast(DateTime value, TimeSpan window, string fieldName = "timestamp")
    {
        var age = DateTime.UtcNow - value.ToUniversalTime();
        if (age > window)
            throw new AssertionException($"Expected '{fieldName}' to be within the last {window}, but it was {age} ago");
    }
}

public sealed class AssertionException(string message) : Exception(message);
