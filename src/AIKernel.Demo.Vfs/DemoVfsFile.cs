namespace AIKernel.Demo.Vfs;

public sealed record DemoVfsFile
{
    public DemoVfsFile(
        string path,
        string content)
    {
        Path = DemoVfsPath.Canonicalize(path);
        Content = content;
    }

    public string Path { get; }
    public string Content { get; }
}
