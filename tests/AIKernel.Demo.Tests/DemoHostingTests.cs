using AIKernel.Demo.Hosting;
using AIKernel.Demo.Kernel;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Tests;

public sealed class DemoHostingTests
{
    private const string Dsl = """
        normalize
        structure
        provider
        polish
        """;

    [Fact]
    public void KernelHostBuildsSuccessfully()
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());

        Assert.NotNull(host);
        Assert.NotNull(host.Get<DemoTaskManager>());
        Assert.NotNull(host.Get<DemoExecutionEngine>());
        Assert.NotNull(host.Get<DemoProviderRouter>());
        Assert.NotNull(host.Get<DemoLlmController>());
    }

    [Fact]
    public void KernelCanBeRetrieved()
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());

        var kernel = host.GetKernel();

        Assert.NotNull(kernel);
        Assert.IsType<DemoKernel>(kernel);
        Assert.Same(kernel, host.Get<DemoKernel>());
    }

    [Fact]
    public async Task PipelineExecutesDeterministically()
    {
        var first = await DemoHostingBootstrap.ExecuteSimplePipelineAsync(Dsl);
        var second = await DemoHostingBootstrap.ExecuteSimplePipelineAsync(Dsl);

        Assert.True(first.IsSuccessState);
        Assert.True(second.IsSuccessState);
        Assert.Equal(first.Value.FinalHash, second.Value.FinalHash);
        Assert.Equal(4, first.Value.SemanticDeltas.Count);
    }
}
