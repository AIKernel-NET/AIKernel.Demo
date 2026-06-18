using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.KernelExecution;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoExecutionContext
{
    private readonly SortedDictionary<string, string> _orchestrationState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _expressionState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _materialState = new(StringComparer.Ordinal);

    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyDictionary<string, string> OrchestrationState => _orchestrationState;
    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyDictionary<string, string> ExpressionState => _expressionState;
    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyDictionary<string, string> MaterialState => _materialState;

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="delta">EN:  JA: delta パラメーターです。
    /// [EN] The demo value supplied for <c>delta</c>.
    /// [JA] <c>delta</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public DemoExecutionContext ApplyDelta(
        DemoSemanticDelta delta)
    {
        ArgumentNullException.ThrowIfNull(delta);

        _orchestrationState[delta.StepName] = delta.After;
        _expressionState["last_step"] = delta.StepName;
        _materialState[$"{delta.StepName}.transition"] = $"{delta.Before}->{delta.After}";

        return this;
    }
}
