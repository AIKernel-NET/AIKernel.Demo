namespace AIKernel.Demo.Execution;

public sealed record DemoExecutionGraph(
    IReadOnlyList<DemoExecutionNode> Nodes)
{
    public static DemoExecutionGraph FromSteps(
        IEnumerable<string> stepNames)
    {
        ArgumentNullException.ThrowIfNull(stepNames);

        var nodes = stepNames
            .Select((stepName, index) => new DemoExecutionNode(
                Id: $"{index + 1:00}-{stepName}",
                StepName: stepName))
            .ToArray();

        return new DemoExecutionGraph(nodes);
    }

    public IReadOnlyList<DemoExecutionNode> Traverse()
        => Nodes;
}
