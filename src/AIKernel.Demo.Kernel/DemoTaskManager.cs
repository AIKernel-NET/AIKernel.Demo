using AIKernel.Demo.Execution;

namespace AIKernel.Demo.Kernel;

public sealed class DemoTaskManager
{
    public IReadOnlyList<DemoExecutionNode> Schedule(
        DemoExecutionGraph graph)
    {
        ArgumentNullException.ThrowIfNull(graph);
        return graph.Traverse().ToArray();
    }
}
