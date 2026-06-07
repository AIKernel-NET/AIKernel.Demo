namespace AIKernel.Demo.PDP;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public enum DemoPolicyCode
{
    Allow,
    Deny
}

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
/// <param name="Allowed">
/// [EN] The demo value supplied for <c>Allowed</c>.
/// [JA] <c>Allowed</c> として渡される Demo 値です。
/// </param>
/// <param name="Code">
/// [EN] The demo value supplied for <c>Code</c>.
/// [JA] <c>Code</c> として渡される Demo 値です。
/// </param>
/// <param name="Reason">
/// [EN] The demo value supplied for <c>Reason</c>.
/// [JA] <c>Reason</c> として渡される Demo 値です。
/// </param>
public sealed record DemoPolicyDecision(
    bool Allowed,
    DemoPolicyCode Code,
    string Reason)
{
    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="reason">
    /// [EN] The demo value supplied for <c>reason</c>.
    /// [JA] <c>reason</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoPolicyDecision Allow(
        string? reason)
        => new(true, DemoPolicyCode.Allow, reason ?? string.Empty);

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="reason">
    /// [EN] The demo value supplied for <c>reason</c>.
    /// [JA] <c>reason</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static DemoPolicyDecision Deny(
        string? reason)
        => new(false, DemoPolicyCode.Deny, reason ?? string.Empty);
}
