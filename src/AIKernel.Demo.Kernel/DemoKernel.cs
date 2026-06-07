using AIKernel.Demo.Execution;
using AIKernel.Demo.KernelExecution;
using System.Collections.Immutable;

namespace AIKernel.Demo.Kernel;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoKernel
{
    private readonly DemoTaskManager _taskManager;
    private readonly DemoProviderRouter _providerRouter;
    private readonly DemoLlmController _llmController;
    private readonly DemoExecutionEngine _executionEngine;

    /// <summary>
    /// [EN] Executes a deterministic operation on the demo contract surface.
    /// [JA] Demo 契約サーフェス上で決定論的な操作を実行します。
    /// </summary>
    /// <param name="taskManager">
    /// [EN] The demo value supplied for <c>taskManager</c>.
    /// [JA] <c>taskManager</c> として渡される Demo 値です。
    /// </param>
    /// <param name="providerRouter">
    /// [EN] The demo value supplied for <c>providerRouter</c>.
    /// [JA] <c>providerRouter</c> として渡される Demo 値です。
    /// </param>
    /// <param name="llmController">
    /// [EN] The demo value supplied for <c>llmController</c>.
    /// [JA] <c>llmController</c> として渡される Demo 値です。
    /// </param>
    /// <param name="executionEngine">
    /// [EN] The demo value supplied for <c>executionEngine</c>.
    /// [JA] <c>executionEngine</c> として渡される Demo 値です。
    /// </param>
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

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="graph">
    /// [EN] The demo value supplied for <c>graph</c>.
    /// [JA] <c>graph</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
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
