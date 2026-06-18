namespace AIKernel.Demo.Semantics;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="StepName">EN:  JA: StepName パラメーターです。
/// [EN] The demo value supplied for <c>StepName</c>.
/// [JA] <c>StepName</c> として渡される Demo 値です。
/// </param>
/// <param name="Before">EN:  JA: Before パラメーターです。
/// [EN] The demo value supplied for <c>Before</c>.
/// [JA] <c>Before</c> として渡される Demo 値です。
/// </param>
/// <param name="After">EN:  JA: After パラメーターです。
/// [EN] The demo value supplied for <c>After</c>.
/// [JA] <c>After</c> として渡される Demo 値です。
/// </param>
/// <param name="Slot">EN:  JA: Slot パラメーターです。
/// [EN] The demo value supplied for <c>Slot</c>.
/// [JA] <c>Slot</c> として渡される Demo 値です。
/// </param>
public sealed record DemoSemanticDelta(
    string StepName,
    string Before,
    string After,
    DemoSemanticSlot Slot = DemoSemanticSlot.Orchestration);

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public enum DemoSemanticSlot
{
    Orchestration,
    Expression,
    Material
}
