namespace AIKernel.Demo.Replay;

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
/// <param name="DeltaSummary">EN:  JA: DeltaSummary パラメーターです。
/// [EN] The demo value supplied for <c>DeltaSummary</c>.
/// [JA] <c>DeltaSummary</c> として渡される Demo 値です。
/// </param>
/// <param name="EntryHash">EN:  JA: EntryHash パラメーターです。
/// [EN] The demo value supplied for <c>EntryHash</c>.
/// [JA] <c>EntryHash</c> として渡される Demo 値です。
/// </param>
/// <param name="ChainHash">EN:  JA: ChainHash パラメーターです。
/// [EN] The demo value supplied for <c>ChainHash</c>.
/// [JA] <c>ChainHash</c> として渡される Demo 値です。
/// </param>
/// <param name="Timestamp">EN:  JA: Timestamp パラメーターです。
/// [EN] The demo value supplied for <c>Timestamp</c>.
/// [JA] <c>Timestamp</c> として渡される Demo 値です。
/// </param>
public sealed record DemoReplayEntry(
    string StepName,
    string DeltaSummary,
    string EntryHash,
    string ChainHash,
    string Timestamp)
{
    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public string Hash => ChainHash;
}
