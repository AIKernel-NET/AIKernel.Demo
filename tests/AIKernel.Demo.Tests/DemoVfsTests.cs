using AIKernel.Demo.Vfs;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoVfsTests
{
    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void SnapshotReadsFiles()
    {
        var snapshot = new DemoVfsSnapshot()
            .AddFile("/rom/demo.txt", "hello");

        Assert.Equal("hello", snapshot.ReadFile("/rom/demo.txt"));
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
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

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void AddFileReturnsNewSnapshot()
    {
        var original = new DemoVfsSnapshot();
        var next = original.AddFile("/rom/a.txt", "a");

        Assert.Empty(original.Files);
        Assert.Equal("a", next.ReadFile("/rom/a.txt"));
    }

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    public void SnapshotHashNormalizesContentWhitespace()
    {
        var first = new DemoVfsSnapshot().AddFile("/rom/a.txt", "hello\r\n");
        var second = new DemoVfsSnapshot().AddFile(@" /rom\a.txt ", "hello\n");

        Assert.Equal(first.ComputeSnapshotHash(), second.ComputeSnapshotHash());
    }
}
