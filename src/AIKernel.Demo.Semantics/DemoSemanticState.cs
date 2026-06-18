namespace AIKernel.Demo.Semantics;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoSemanticState
{
    private readonly SortedDictionary<string, string> _orchestrationState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _expressionState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _materialState = new(StringComparer.Ordinal);

    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyDictionary<string, string> Values => _orchestrationState;
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
    public DemoSemanticState Apply(
        DemoSemanticDelta delta)
    {
        ArgumentNullException.ThrowIfNull(delta);

        var next = Clone();
        next.TargetState(delta.Slot)[delta.StepName] = delta.After;
        return next;
    }

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="deltas">EN:  JA: deltas パラメーターです。
    /// [EN] The demo value supplied for <c>deltas</c>.
    /// [JA] <c>deltas</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoSemanticState FromDeltas(
        IEnumerable<DemoSemanticDelta> deltas)
    {
        ArgumentNullException.ThrowIfNull(deltas);

        var state = new DemoSemanticState();
        foreach (var delta in deltas)
            state = state.Apply(delta);

        return state;
    }

    private DemoSemanticState Clone()
    {
        var next = new DemoSemanticState();
        CopyTo(_orchestrationState, next._orchestrationState);
        CopyTo(_expressionState, next._expressionState);
        CopyTo(_materialState, next._materialState);
        return next;
    }

    private SortedDictionary<string, string> TargetState(
        DemoSemanticSlot slot)
        => slot switch
        {
            DemoSemanticSlot.Orchestration => _orchestrationState,
            DemoSemanticSlot.Expression => _expressionState,
            DemoSemanticSlot.Material => _materialState,
            _ => _orchestrationState
        };

    private static void CopyTo(
        SortedDictionary<string, string> source,
        SortedDictionary<string, string> target)
    {
        foreach (var item in source)
            target[item.Key] = item.Value;
    }
}
