using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;
using System.Collections.Immutable;

namespace AIKernel.Demo.Kernel;

public sealed class DemoKernel
{
    private readonly DemoTaskManager _taskManager;
    private readonly DemoProviderRouter _providerRouter;
    private readonly DemoLlmController _llmController;
    private readonly DemoExecutionEngine _executionEngine;

    public DemoKernel(
        DemoTaskManager taskManager,
        DemoProviderRouter providerRouter,
        DemoLlmController llmController,
        DemoExecutionEngine executionEngine)
    {
        _taskManager = taskManager;
        _providerRouter = providerRouter;
        _llmController = llmController;
        _executionEngine = executionEngine;
    }

    public Task<DemoKernelRun> ExecuteAsync(
        DemoExecutionGraph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);

        var scheduled = _taskManager.Schedule(graph);
        var routing = _providerRouter.Route();
        var providerOutputs = scheduled
            .Select(node => _llmController.Generate(node.StepName))
            .ToImmutableArray();

        var run = _executionEngine.Execute(graph) with
        {
            ProviderId = routing.ProviderId,
            ModelId = routing.ModelId,
            RouteReason = routing.RouteReason,
            ProviderOutputs = providerOutputs
        };

        return Task.FromResult(run);
    }
}
