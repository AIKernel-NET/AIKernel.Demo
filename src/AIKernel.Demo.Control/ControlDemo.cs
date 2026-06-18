using AIKernel.Control.CPU;
using AIKernel.Control.Core.Bonsai;
using AIKernel.Control.Emulator;
using AIKernel.Control.GPU;
using AIKernel.Dtos.Control;

namespace AIKernel.Demo.Control;

/// <summary>
/// [EN] Demonstrates fail-closed governance control without external execution.
/// [JA] 外部実行なしで fail-closed governance control を示します。
/// </summary>
/// <remarks>
/// [EN] Control is the governance execution layer. The demo uses the emulator so learners can inspect graph, policy, scheduler, and Bonsai-related public surfaces without starting external work.
/// [JA] Control は governance execution layer です。このデモは emulator を使うため、外部処理を開始せずに graph、policy、scheduler、Bonsai 関連の public surface を確認できます。
/// </remarks>
public static class ControlDemo
{
    /// <summary>
    /// [EN] Runs the minimal Control golden path.
    /// [JA] 最小 Control golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The graph contains one dry-run node. That keeps the sample deterministic while still showing how a request flows through the Control engine boundary.
    /// [JA] graph は dry-run node を 1 つだけ持ちます。これにより sample を決定論的に保ちながら、request が Control engine boundary をどう流れるかを示します。
    /// </remarks>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] A readable log containing emulator id, graph id, execution status, and Bonsai surface names.
    /// [JA] emulator id、graph id、execution status、Bonsai surface 名を含む読みやすいログです。
    /// </returns>
    public static async Task<IReadOnlyList<string>> RunAsync()
    {
        var engine = new ControlEmulatorEngine(new DeterministicNodeScheduler(), new AllowAllControlPolicy());
        var graph = new EmulatedExecutionGraph(
            "demo.control.graph",
            [new EmulatedExecutionNode("node.prepare", "demo.noop", new Dictionary<string, string> { ["phase"] = "prepare" })]);
        var result = await engine.ExecuteAsync(
            graph,
            new ControlExecutionRequest("demo.control.execution", new Dictionary<string, string> { ["mode"] = "dry-run" }))
            .ConfigureAwait(false);
        var bonsaiTypes = new[]
        {
            typeof(IBonsaiInferenceKernel).Name,
            typeof(BonsaiBuiltInProvider).Name,
            typeof(BonsaiTokenizer).Name,
            typeof(Bonsai1BitCpuKernel).Name,
            typeof(IBonsaiGpuExecutionDelegate).Name
        };

        return
        [
            "AIKernel.Demo.Control",
            $"engine={engine.EngineId}",
            $"graph={graph.GraphId}",
            $"status={result.Status}",
            $"bonsai.map={string.Join(',', bonsaiTypes)}"
        ];
    }
}
