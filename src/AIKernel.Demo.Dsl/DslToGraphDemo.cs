using AIKernel.Common.Results;
using AIKernel.Demo.Execution;
using AIKernel.Dtos.Dsl;

namespace AIKernel.Demo.Dsl;

public static class DslToGraphDemo
{
    public static Result<DemoExecutionGraph> Convert(
        DslDocument document)
    {
        if (document.Root is not PipelineRootNode root)
            return Result<DemoExecutionGraph>.Fail("DSL root must be a pipeline.");

        var stepNames = new List<string>(root.Steps.Count);
        foreach (var node in root.Steps)
        {
            if (node is not StepNode step)
                return Result<DemoExecutionGraph>.Fail($"Unsupported DSL node: {node.Type}");

            stepNames.Add(step.Name);
        }

        return Result<DemoExecutionGraph>.Success(DemoExecutionGraph.FromSteps(stepNames));
    }
}
