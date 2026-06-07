using AIKernel.Demo.Execution;

namespace AIKernel.Demo.Tests;

public sealed class DemoExecutionGraphTests
{
    [Fact]
    public void FromStepsBuildsLinearGraph()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Equal(2, graph.Nodes.Count);
        Assert.Equal("01-normalize", graph.Nodes[0].Id);
        Assert.Equal("02-provider", graph.Nodes[1].Id);
    }

    [Fact]
    public void TraverseReturnsNodesInOrder()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "structure", "provider"]);

        Assert.Equal(
            ["normalize", "structure", "provider"],
            graph.Traverse().Select(node => node.StepName));
    }

    [Fact]
    public void TraverseReturnsUnderlyingNodes()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Same(graph.Nodes, graph.Traverse());
    }

    [Fact]
    public void NodeIdsFollowCanonicalFormat()
    {
        var graph = DemoExecutionGraph.FromSteps(["normalize", "provider"]);

        Assert.Matches(@"^\d{2}-[a-z]+$", graph.Nodes[0].Id);
        Assert.Matches(@"^\d{2}-[a-z]+$", graph.Nodes[1].Id);
        Assert.Equal("01-normalize", graph.Nodes[0].Id);
        Assert.Equal("02-provider", graph.Nodes[1].Id);
    }
}
