using AIKernel.Demo.Pipelines;

namespace AIKernel.Demo.Tests;

public sealed class DemoPipelineMonadTests
{
    [Fact]
    public async Task RunAsyncComposesPipelineWithLinqQuerySyntax()
    {
        var result = await DemoPipelineMonad.RunAsync(" hello ");

        Assert.True(result.IsSuccessState);
        Assert.Equal("executed(compiled(hello))", result.Value);
    }

    [Fact]
    public async Task RunAsyncNormalizesInputBeforeCompileAndExecute()
    {
        var result = await DemoPipelineMonad.RunAsync("  hello  ");

        Assert.True(result.IsSuccessState);
        Assert.Equal("executed(compiled(hello))", result.Value);
    }

    [Fact]
    public async Task RunAsyncFailsClosedWhenInputIsNull()
    {
        var result = await DemoPipelineMonad.RunAsync(null!);

        Assert.False(result.IsSuccessState);
        Assert.Contains("Pipeline input is required.", result.Error.Message);
    }
}
