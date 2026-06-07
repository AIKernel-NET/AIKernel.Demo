using AIKernel.Demo.Vfs;

namespace AIKernel.Demo.Tests;

public sealed class DemoVfsTests
{
    [Fact]
    public void SnapshotReadsFiles()
    {
        var snapshot = new DemoVfsSnapshot()
            .AddFile("/rom/demo.txt", "hello");

        Assert.Equal("hello", snapshot.ReadFile("/rom/demo.txt"));
    }

    [Fact]
    public void SnapshotListsDirectoryDeterministically()
    {
        var snapshot = new DemoVfsSnapshot()
            .AddDirectory("/rom/nested")
            .AddFile("/rom/b.txt", "b")
            .AddFile("/rom/a.txt", "a");

        var entries = snapshot.ListDirectory("/rom");

        Assert.Equal(["/rom/a.txt", "/rom/b.txt", "/rom/nested"], entries);
        Assert.True(entries.SequenceEqual(entries.OrderBy(entry => entry, StringComparer.Ordinal)));
    }

    [Fact]
    public void SnapshotHashIsDeterministic()
    {
        var first = new DemoVfsSnapshot()
            .AddFile("/rom/b.txt", "b")
            .AddFile("/rom/a.txt", "a");
        var second = new DemoVfsSnapshot()
            .AddFile("/rom/a.txt", "a")
            .AddFile("/rom/b.txt", "b");

        Assert.Equal(first.ComputeSnapshotHash(), second.ComputeSnapshotHash());
    }

    [Fact]
    public void FileAndDirectoryCanonicalizePaths()
    {
        var file = new DemoVfsFile(@" /rom\demo.txt ", "hello");
        var directory = new DemoVfsDirectory(@" /rom\nested/ ");
        var relative = new DemoVfsFile("rom/demo.txt", "hello");

        Assert.Equal("/rom/demo.txt", file.Path);
        Assert.Equal("/rom/nested", directory.Path);
        Assert.Equal("/rom/demo.txt", relative.Path);
    }

    [Fact]
    public void AddFileReturnsNewSnapshot()
    {
        var original = new DemoVfsSnapshot();
        var next = original.AddFile("/rom/a.txt", "a");

        Assert.Empty(original.Files);
        Assert.Equal("a", next.ReadFile("/rom/a.txt"));
    }

    [Fact]
    public void SnapshotHashNormalizesContentWhitespace()
    {
        var first = new DemoVfsSnapshot().AddFile("/rom/a.txt", "hello\r\n");
        var second = new DemoVfsSnapshot().AddFile(@" /rom\a.txt ", "hello\n");

        Assert.Equal(first.ComputeSnapshotHash(), second.ComputeSnapshotHash());
    }
}
