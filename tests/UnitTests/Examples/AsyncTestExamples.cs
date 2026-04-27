using NUnit.Framework;

namespace UnitTests.Examples;

/// <summary>
/// Demonstrates async test patterns — Section 01 of the guide.
/// async Task not async void. SetUp and TearDown can be async.
/// </summary>
[TestFixture]
public class AsyncTestExamples
{
    [Test]
    public async Task AsyncMethod_ReturnsExpectedResult()
    {
        var result = await SimulateAsyncOperationAsync(input: 42);

        Assert.That(result, Is.EqualTo(84));
    }

    [Test]
    public async Task AsyncMethod_ThrowsOnInvalidInput_CaughtCorrectly()
    {
        Assert.ThrowsAsync<ArgumentException>(
            async () => await SimulateAsyncOperationAsync(input: -1));
    }

    [Test]
    public async Task AsyncSetup_DataAvailableInTest()
    {
        var data = await LoadTestDataAsync();

        Assert.That(data, Is.Not.Null.And.Not.Empty);
    }

    // --- Stubs simulating async dependencies ---

    private static async Task<int> SimulateAsyncOperationAsync(int input)
    {
        await Task.Delay(10); // simulate I/O
        if (input < 0) throw new ArgumentException("Input must be non-negative", nameof(input));
        return input * 2;
    }

    private static async Task<string> LoadTestDataAsync()
    {
        await Task.Delay(5);
        return "test-data";
    }
}
