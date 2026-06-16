using AIKernel.Common.Results;
using AIKernel.Demo.Execution;
using AIKernel.Dtos.Dsl;

namespace AIKernel.Demo.Dsl;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DslToGraphDemo
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="document">EN:  JA: document パラメーターです。
    /// [EN] The demo value supplied for <c>document</c>.
    /// [JA] <c>document</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static Result<DemoExecutionGraph> Convert(
        DslDocument document)
    {
        if (document.Root is not PipelineRootNode root)
            return Result<DemoExecutionGraph>.Fail("DSL root must be a pipeline.");

        var stepNames = new List<string>(root.Steps.Count);
        foreach (var node in root.Steps)
        {
            if (node is not StepNode step)
                return Result<DemoExecutionGraph>.Fail($"Unsupported DSL node: {node.Type}");

            stepNames.Add(step.Name);
        }

        return Result<DemoExecutionGraph>.Success(DemoExecutionGraph.FromSteps(stepNames));
    }
}
