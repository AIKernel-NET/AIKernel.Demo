namespace AIKernel.Demo.Vfs;

public static class DemoVfsPath
{
    public static string Canonicalize(
        string path)
    {
        var normalized = path.Replace('\\', '/').Trim().TrimEnd('/');
        return normalized.StartsWith("/", StringComparison.Ordinal)
            ? normalized
            : "/" + normalized;
    }
}
