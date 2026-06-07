using AIKernel.Common.Results;
using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;

namespace AIKernel.Demo.Hosting;

public static class DemoHostingBootstrap
{
    public static async Task<Result<DemoKernelRun>> ExecuteSimplePipelineAsync(
        string dslText)
        =>
            await (
                from parsed in DslParserDemo.Parse(dslText)
                from graph in DslToGraphDemo.Convert(parsed)
                from run in ExecuteKernelAsync(graph)
                select run)
            .ConfigureAwait(false);

    private static async Task<Result<DemoKernelRun>> ExecuteKernelAsync(
        DemoExecutionGraph graph)
    {
        using var host = DemoKernelHost.Build(new DemoKernelModule(), new DemoProviderModule());
        var kernel = host.GetKernel();
        var run = await kernel.ExecuteAsync(graph).ConfigureAwait(false);
        return Result<DemoKernelRun>.Success(run);
    }
}
