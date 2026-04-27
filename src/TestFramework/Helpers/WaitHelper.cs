namespace TestFramework.Helpers;

public static class WaitHelper
{
    /// <summary>
    /// Polls condition until it returns a non-null value or timeout elapses.
    /// </summary>
    public static async Task<T> WaitUntilAsync<T>(
        Func<Task<T?>> condition,
        TimeSpan timeout,
        TimeSpan? interval = null,
        string? description = null) where T : class
    {
        var poll     = interval ?? TimeSpan.FromMilliseconds(500);
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            var result = await condition();
            if (result is not null) return result;
            await Task.Delay(poll);
        }

        var desc = description ?? typeof(T).Name;
        throw new TimeoutException(
            $"Condition '{desc}' not met within {timeout.TotalSeconds}s");
    }

    /// <summary>
    /// Polls a boolean condition until true or timeout elapses.
    /// </summary>
    public static async Task WaitForAsync(
        Func<Task<bool>> condition,
        TimeSpan timeout,
        TimeSpan? interval = null,
        string? description = null)
    {
        var poll     = interval ?? TimeSpan.FromMilliseconds(500);
        var deadline = DateTime.UtcNow + timeout;

        while (DateTime.UtcNow < deadline)
        {
            if (await condition()) return;
            await Task.Delay(poll);
        }

        var desc = description ?? "condition";
        throw new TimeoutException(
            $"'{desc}' not met within {timeout.TotalSeconds}s");
    }
}
