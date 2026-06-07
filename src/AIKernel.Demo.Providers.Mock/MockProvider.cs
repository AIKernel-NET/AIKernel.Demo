namespace AIKernel.Demo.Providers.Mock;

public sealed class MockProvider
{
    public MockProviderResponse Generate(
        string prompt)
        => new(
            ProviderId: "demo.mock",
            Output: $"mock:{prompt}",
            OutputHash: $"demo-hash:{prompt.Length}");
}
