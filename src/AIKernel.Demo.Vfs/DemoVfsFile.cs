namespace AIKernel.Demo.Vfs;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed record DemoVfsFile
{
    /// <summary>
    /// [EN] Executes a deterministic operation on the demo contract surface.
    /// [JA] Demo 契約サーフェス上で決定論的な操作を実行します。
    /// </summary>
    /// <param name="path">EN:  JA: path パラメーターです。
    /// [EN] The demo value supplied for <c>path</c>.
    /// [JA] <c>path</c> として渡される Demo 値です。
    /// </param>
    /// <param name="content">EN:  JA: content パラメーターです。
    /// [EN] The demo value supplied for <c>content</c>.
    /// [JA] <c>content</c> として渡される Demo 値です。
    /// </param>
    public DemoVfsFile(
        string path,
        string content)
    {
        Path = DemoVfsPath.Canonicalize(path);
        Content = content;
    }

    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>Path</c>.
    /// [JA] <c>Path</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public string Path { get; }
    /// <summary>
    /// [EN] Gets the deterministic demo value for <c>Content</c>.
    /// [JA] <c>Content</c> に対応する決定論的な Demo 値を取得します。
    /// </summary>
    public string Content { get; }
}
