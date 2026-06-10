namespace AIKernel.Demo.Execution;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="Nodes">
/// [EN] The demo value supplied for <c>Nodes</c>.
/// [JA] <c>Nodes</c> として渡される Demo 値です。
/// </param>
public sealed record DemoExecutionGraph(
    IReadOnlyList<DemoExecutionNode> Nodes)
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="stepNames">
    /// [EN] The demo value supplied for <c>stepNames</c>.
    /// [JA] <c>stepNames</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoExecutionGraph FromSteps(
        IEnumerable<string> stepNames)
    {
        ArgumentNullException.ThrowIfNull(stepNames);

        var nodes = stepNames
            .Select((stepName, index) => new DemoExecutionNode(
                Id: $"{index + 1:00}-{stepName}",
                StepName: stepName))
            .ToArray();

        return new DemoExecutionGraph(nodes);
    }

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public IReadOnlyList<DemoExecutionNode> Traverse()
        => Nodes;
}
