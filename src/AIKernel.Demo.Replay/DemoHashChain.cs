using System.Security.Cryptography;
using System.Text;

namespace AIKernel.Demo.Replay;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public static class DemoHashChain
{
    /// <summary>
    /// [EN] Defines a deterministic constant used by the demo reference surface.
    /// [JA] Demo 参照サーフェスで使用する決定論的な定数を定義します。
    /// </summary>
    public const string GenesisHash = "GENESIS";

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="entryPayload">
    /// [EN] The demo value supplied for <c>entryPayload</c>.
    /// [JA] <c>entryPayload</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static string ComputeEntryHash(
        string entryPayload)
    {
        ArgumentNullException.ThrowIfNull(entryPayload);

        return Sha256Hex(entryPayload);
    }

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="previousHash">
    /// [EN] The demo value supplied for <c>previousHash</c>.
    /// [JA] <c>previousHash</c> として渡される Demo 値です。
    /// </param>
    /// <param name="entryHash">
    /// [EN] The demo value supplied for <c>entryHash</c>.
    /// [JA] <c>entryHash</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static string ComputeNext(
        string previousHash,
        string entryHash)
    {
        ArgumentNullException.ThrowIfNull(previousHash);
        ArgumentNullException.ThrowIfNull(entryHash);

        return Sha256Hex(previousHash + entryHash);
    }

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="stepName">
    /// [EN] The demo value supplied for <c>stepName</c>.
    /// [JA] <c>stepName</c> として渡される Demo 値です。
    /// </param>
    /// <param name="deltaSummary">
    /// [EN] The demo value supplied for <c>deltaSummary</c>.
    /// [JA] <c>deltaSummary</c> として渡される Demo 値です。
    /// </param>
    /// <param name="timestamp">
    /// [EN] The demo value supplied for <c>timestamp</c>.
    /// [JA] <c>timestamp</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static string CanonicalizePayload(
        string stepName,
        string deltaSummary,
        string timestamp)
    {
        ArgumentNullException.ThrowIfNull(stepName);
        ArgumentNullException.ThrowIfNull(deltaSummary);
        ArgumentNullException.ThrowIfNull(timestamp);

        return string.Join(
            '\n',
            $"delta_summary={Normalize(deltaSummary)}",
            $"step_name={Normalize(stepName)}",
            $"timestamp={Normalize(timestamp)}");
    }

    private static string Sha256Hex(
        string value)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(value))).ToLowerInvariant();

    private static string Normalize(
        string value)
        => value.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n').Trim();
}
