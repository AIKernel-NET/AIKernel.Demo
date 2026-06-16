using AIKernel.Common.Results;
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
public static class DslParserDemo
{
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>AllowedSteps</c>.
    /// [JA] <c>AllowedSteps</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    /// <param name="Ordinal">EN:  JA: Ordinal パラメーターです。
    /// [EN] The demo value supplied for <c>Ordinal</c>.
    /// [JA] <c>Ordinal</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static IReadOnlySet<string> AllowedSteps { get; set; } = new HashSet<string>(StringComparer.Ordinal)
    {
        "normalize",
        "structure",
        "provider",
        "polish",
    };

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="text">EN:  JA: text パラメーターです。
    /// [EN] The demo value supplied for <c>text</c>.
    /// [JA] <c>text</c> として渡される Demo 値です。
    /// </param>
    /// <returns>EN:  JA: 結果を返します。
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public static Result<DslDocument> Parse(string text)
    {
        if (text is null)
            return Result<DslDocument>.Fail("DSL text is required.");

        var names = text
            .Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Split('\n')
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .Where(line => !line.StartsWith("#", StringComparison.Ordinal))
            .ToArray();

        foreach (var name in names)
        {
            if (!AllowedSteps.Contains(name))
                return Result<DslDocument>.Fail($"Unknown step: {name}");
        }

        var steps = names
            .Select<string, PipelineNode>(name => new StepNode(name))
            .ToArray();

        return Result<DslDocument>.Success(new DslDocument(new PipelineRootNode(steps)));
    }
}
