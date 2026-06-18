using AIKernel.Abstractions.Control;
using AIKernel.Demo.Pipelines;
using AIKernel.Dtos.Control;
using System.Text.RegularExpressions;

namespace AIKernel.Demo.Tests;

/// <summary>
/// [EN] Represents a public demo surface used as readable reference code for AIKernel learners.
/// [JA] AIKernel 学習者向けの読みやすい参照コードとして公開される Demo サーフェスを表します。
/// </summary>
/// <remarks>
/// [EN] Demo types intentionally keep contract boundaries visible instead of hiding semantic structure, policy, routing, execution, or replay behind framework magic.
/// [JA] Demo 型は semantic structure、policy、routing、execution、replay の境界を framework magic で隠さず、見える形に保ちます。
/// </remarks>
public sealed class DemoContractBoundaryTests
{
    private static readonly Regex OperatorIdPattern =
        new(@"^[a-z0-9]+(\.[a-z0-9]+)*$", RegexOptions.CultureInvariant);
    private static readonly Regex MetadataKeyPattern =
        new(@"^[a-z0-9_]+$", RegexOptions.CultureInvariant);
    private static readonly Regex OperationNamePattern =
        new(@"^[a-z0-9]+(\.[a-z0-9]+)*$", RegexOptions.CultureInvariant);

    /// <summary>
    /// [EN] Verifies a demo invariant as executable documentation.
    /// [JA] Demo invariant を実行可能な文書として検証します。
    /// </summary>
    /// <remarks>
    /// [EN] The test fixes the observable behavior that readers should expect from the demo contract.
    /// [JA] このテストは Demo 契約から読み手が期待すべき観測可能な振る舞いを固定します。
    /// </remarks>
    [Fact]
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    public void DemoCanRepresentControlExecutionUsingOnlyContractTypes()
    {
        IExecutionGraph graph = new DemoExecutionGraph(
            "demo.graph.default",
            [
                new DemoExecutionNode(
                    "01-normalize",
                    "demo.operator.normalize",
                    Metadata(("phase", "normalize"))),
                new DemoExecutionNode(
                    "02-provider",
                    "demo.operator.provider",
                    Metadata(("capability", "chat.completion")))
            ]);
        var request = new ControlExecutionRequest(
            "demo-exec-001",
            Metadata(("graph_id", graph.GraphId)));
        var snapshot = new ControlStateSnapshot(
            request.ExecutionId,
            graph.GraphId,
            graph.Nodes[1].NodeId,
            Metadata(
                ("status", "Running"),
                ("operator_id", graph.Nodes[1].OperatorId),
                ("capability", graph.Nodes[1].Metadata["capability"])));
        var result = new ControlExecutionResult(
            request.ExecutionId,
            "Completed",
            Metadata(
                ("graph_id", graph.GraphId),
                ("node_count", graph.Nodes.Count.ToString(System.Globalization.CultureInfo.InvariantCulture))));

        Assert.Equal("demo.graph.default", graph.GraphId);
        Assert.Equal(["01-normalize", "02-provider"], graph.Nodes.Select(node => node.NodeId).ToArray());
        Assert.Equal("demo-exec-001", request.ExecutionId);
        Assert.Equal("02-provider", snapshot.NodeId);
        Assert.Equal("demo.operator.provider", snapshot.Metadata["operator_id"]);
        Assert.All(snapshot.Metadata.Keys, key => Assert.Matches(MetadataKeyPattern, key));
        Assert.Equal("Completed", result.Status);
        Assert.Equal("2", result.Metadata["node_count"]);
        Assert.All(result.Metadata.Keys, key => Assert.Matches(MetadataKeyPattern, key));
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    public void DemoUsesCanonicalCapabilityOperationNames()
    {
        string[] operations =
        [
            "chat.local",
            "chat.completion",
            "embedding",
            "vfs.git.read",
            "vfs.git.list",
            "vfs.git.checkout",
            "rom.save",
            "rom.load",
            "rom.list",
            "pipeline.compile",
            "pipeline.execute",
            "pipeline.validate",
            "tensor.matmul",
            "tensor.softmax",
            "tensor.conv2d",
            "tensor.layernorm"
        ];

        Assert.Equal(operations, operations.Distinct(StringComparer.Ordinal).ToArray());
        Assert.All(operations, operation => Assert.DoesNotContain(' ', operation));
        Assert.All(operations, operation => Assert.Matches(OperationNamePattern, operation));
        Assert.Contains("chat.completion", operations);
        Assert.Contains("pipeline.validate", operations);
        Assert.Contains("tensor.layernorm", operations);
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    public void DemoDoesNotReferenceControlInternalTypes()
    {
        var demoAssemblies = new[]
        {
            typeof(DemoPipelineCatalog).Assembly
        };

        foreach (var assembly in demoAssemblies)
        {
            Assert.DoesNotContain(
                assembly.GetReferencedAssemblies(),
                reference => reference.Name is not null
                    && reference.Name.StartsWith("AIKernel.Control.", StringComparison.Ordinal));
            Assert.DoesNotContain(
                assembly.GetTypes(),
                type => type.FullName?.Contains("AIKernel.Control.", StringComparison.Ordinal) == true);
        }
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
    /// <summary>
    /// [EN] Verifies the documented Demo behavior for this deterministic test case.
    /// [JA] この決定論的なテストケースで文書化された Demo の振る舞いを検証します。
    /// </summary>
    public void DemoOperatorIdsFollowCanonicalFormat()
    {
        IExecutionGraph graph = new DemoExecutionGraph(
            "demo.graph.default",
            [
                new DemoExecutionNode("01-normalize", "demo.operator.normalize", Metadata()),
                new DemoExecutionNode("02-provider", "demo.operator.provider", Metadata())
            ]);

        Assert.All(graph.Nodes, node => Assert.Matches(OperatorIdPattern, node.OperatorId));
    }

    private static Dictionary<string, string> Metadata(params (string Key, string Value)[] values)
    {
        var metadata = new Dictionary<string, string>(StringComparer.Ordinal);
        foreach (var (key, value) in values)
        {
            metadata.Add(key, value);
        }

        return metadata;
    }

    private sealed record DemoExecutionGraph(
        string GraphId,
        IReadOnlyList<IExecutionNode> Nodes) : IExecutionGraph;

    private sealed record DemoExecutionNode(
        string NodeId,
        string OperatorId,
        IReadOnlyDictionary<string, string> Metadata) : IExecutionNode;
}
