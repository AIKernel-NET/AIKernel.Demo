using System.Security.Cryptography;
using System.Text;

namespace AIKernel.Demo.Vfs;

public sealed class DemoVfsSnapshot
{
    private readonly SortedDictionary<string, string> _files = new(StringComparer.Ordinal);
    private readonly SortedSet<string> _directories = new(StringComparer.Ordinal);

    public IReadOnlyDictionary<string, string> Files => _files;
    public IReadOnlyCollection<string> Directories => _directories;

    public DemoVfsSnapshot AddDirectory(
        string path)
    {
        var next = Clone();
        next._directories.Add(DemoVfsPath.Canonicalize(path));
        return next;
    }

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

    public string ReadFile(
        string path)
        => _files[DemoVfsPath.Canonicalize(path)];

    public IReadOnlyList<string> ListDirectory(
        string path)
    {
        var normalized = DemoVfsPath.Canonicalize(path);
        var prefix = normalized.Length == 0 ? string.Empty : normalized + "/";

        return _directories
            .Where(directory => directory.StartsWith(prefix, StringComparison.Ordinal))
            .Concat(_files.Keys.Where(file => file.StartsWith(prefix, StringComparison.Ordinal)))
            .Distinct(StringComparer.Ordinal)
            .Order(StringComparer.Ordinal)
            .ToArray();
    }

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
        return index <= 0 ? string.Empty : path[..index];
    }
}
