using System.Security.Cryptography;
using System.Text;

namespace AIKernel.Demo.Vfs;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoVfsSnapshot
{
    private readonly SortedDictionary<string, string> _files = new(StringComparer.Ordinal);
    private readonly SortedSet<string> _directories = new(StringComparer.Ordinal);

    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyDictionary<string, string> Files => _files;
    /// <summary>
    /// [EN] Represents a public demo member used by the AIKernel reference implementation.
    /// [JA] AIKernel 参照実装で使用する公開 Demo メンバーを表します。
    /// </summary>
    public IReadOnlyCollection<string> Directories => _directories;

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="path">
    /// [EN] The demo value supplied for <c>path</c>.
    /// [JA] <c>path</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public DemoVfsSnapshot AddDirectory(
        string path)
    {
        var next = Clone();
        next._directories.Add(DemoVfsPath.Canonicalize(path));
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
    /// <param name="path">
    /// [EN] The demo value supplied for <c>path</c>.
    /// [JA] <c>path</c> として渡される Demo 値です。
    /// </param>
    /// <param name="content">
    /// [EN] The demo value supplied for <c>content</c>.
    /// [JA] <c>content</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public DemoVfsSnapshot AddFile(
        string path,
        string content)
    {
        var next = Clone();
        var normalized = DemoVfsPath.Canonicalize(path);
        var directory = GetDirectory(normalized);
        if (directory.Length > 0)
            next._directories.Add(directory);

        next._files[normalized] = content;
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
    /// <param name="path">
    /// [EN] The demo value supplied for <c>path</c>.
    /// [JA] <c>path</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public string ReadFile(
        string path)
        => _files[DemoVfsPath.Canonicalize(path)];

    /// <summary>
    /// [EN] Executes a deterministic demo operation on the AIKernel teaching surface.
    /// [JA] AIKernel 教材サーフェス上で決定論的な Demo 操作を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The operation is documented at the member boundary so generated reference pages can explain the runtime intent without changing behavior.
    /// [JA] この操作は member boundary で文書化されるため、振る舞いを変えずに生成 reference page が runtime intent を説明できます。
    /// </remarks>
    /// <param name="path">
    /// [EN] The demo value supplied for <c>path</c>.
    /// [JA] <c>path</c> として渡される Demo 値です。
    /// </param>
    /// <returns>
    /// [EN] The deterministic result produced by this demo member.
    /// [JA] この Demo メンバーが生成する決定論的な結果です。
    /// </returns>
    public IReadOnlyList<string> ListDirectory(
        string path)
    {
        var normalized = DemoVfsPath.Canonicalize(path);
        var prefix = normalized.Length switch
        {
            0 => string.Empty,
            _ => normalized + "/"
        };

        return _directories
            .Where(directory => directory.StartsWith(prefix, StringComparison.Ordinal))
            .Concat(_files.Keys.Where(file => file.StartsWith(prefix, StringComparison.Ordinal)))
            .Distinct(StringComparer.Ordinal)
            .Order(StringComparer.Ordinal)
            .ToArray();
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
    public string ComputeSnapshotHash()
    {
        var builder = new StringBuilder();
        foreach (var directory in _directories)
            builder.Append(CanonicalLine("directory", directory, string.Empty)).Append('\n');
        foreach (var file in _files)
            builder.Append(CanonicalLine("file", file.Key, file.Value)).Append('\n');

        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(builder.ToString())))
            .ToLowerInvariant();
    }

    private DemoVfsSnapshot Clone()
    {
        var next = new DemoVfsSnapshot();
        foreach (var directory in _directories)
            next._directories.Add(directory);
        foreach (var file in _files)
            next._files[file.Key] = file.Value;

        return next;
    }

    private static string CanonicalLine(
        string kind,
        string path,
        string content)
        => string.Join(
            '|',
            $"content={NormalizeContent(content)}",
            $"kind={kind}",
            $"path={DemoVfsPath.Canonicalize(path)}");

    private static string NormalizeContent(
        string content)
        => content.Replace("\r\n", "\n", StringComparison.Ordinal).Replace('\r', '\n').Trim();

    private static string GetDirectory(
        string path)
    {
        var index = path.LastIndexOf('/');
        return index switch
        {
            <= 0 => string.Empty,
            _ => path[..index]
        };
    }
}
