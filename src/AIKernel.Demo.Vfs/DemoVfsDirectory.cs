namespace AIKernel.Demo.Vfs;

public sealed record DemoVfsDirectory
{
    public DemoVfsDirectory(
        string path)
        => Path = DemoVfsPath.Canonicalize(path);

    public string Path { get; }
}
