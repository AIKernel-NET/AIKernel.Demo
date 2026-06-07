using AIKernel.Demo.Providers.Mock;

namespace AIKernel.Demo.Tests;

public sealed class MockProviderTests
{
    [Fact]
    public void GenerateReturnsDeterministicSha256Hash()
    {
        var provider = new MockProvider();

        var first = provider.Generate("hello-rom");
        var second = provider.Generate("hello-rom");

        Assert.Equal(first, second);
        Assert.Equal(
            "F12749ED10DFAF5A3CFACC44A2F444EF9E1C1312631108447C94C82B16CCF9AD",
            first.OutputHash);
        Assert.Matches("^[A-F0-9]{64}$", first.OutputHash);
    }

    [Fact]
    public void GenerateRejectsNullInput()
    {
        var provider = new MockProvider();

        Assert.Throws<ArgumentNullException>(() => provider.Generate(null!));
    }
}
