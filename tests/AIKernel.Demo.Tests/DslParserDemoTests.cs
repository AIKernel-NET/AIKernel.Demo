using AIKernel.Demo.Dsl;
using AIKernel.Demo.Execution;
using AIKernel.Dtos.Dsl;

namespace AIKernel.Demo.Tests;

public sealed class DslParserDemoTests
{
    [Fact]
    public void ParseReturnsDslDocumentForValidSteps()
    {
        var result = DslParserDemo.Parse("""
            normalize
            structure
            provider
            polish
            """);

        Assert.True(result.IsSuccessState);
        var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
        Assert.Equal(4, root.Steps.Count);
        Assert.Equal(
            ["normalize", "structure", "provider", "polish"],
            root.Steps.Cast<StepNode>().Select(step => step.Name));
    }

    [Fact]
    public void ParseFailsForUnknownStep()
    {
        var result = DslParserDemo.Parse("""
            normalize
            INVALID_STEP
            """);

        Assert.False(result.IsSuccessState);
        Assert.Contains("Unknown step: INVALID_STEP", result.Error.Message);
    }

    [Fact]
    public void ParseIgnoresEmptyAndCommentLines()
    {
        var result = DslParserDemo.Parse("""
            normalize

            # comment
            provider
            """);

        Assert.True(result.IsSuccessState);
        var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
        Assert.Equal(["normalize", "provider"], root.Steps.Cast<StepNode>().Select(step => step.Name));
    }

    [Fact]
    public void ParseUsesInjectedAllowedSteps()
    {
        var previous = DslParserDemo.AllowedSteps;
        try
        {
            DslParserDemo.AllowedSteps = new HashSet<string>(StringComparer.Ordinal)
            {
                "normalize",
                "call",
                "suspend"
            };

            var result = DslParserDemo.Parse("""
                normalize
                call
                suspend
                """);

            Assert.True(result.IsSuccessState);
            var root = Assert.IsType<PipelineRootNode>(result.Value.Root);
            Assert.Equal(["normalize", "call", "suspend"], root.Steps.Cast<StepNode>().Select(step => step.Name));
        }
        finally
        {
            DslParserDemo.AllowedSteps = previous;
        }
    }

    [Fact]
    public void ParseUsesOrdinalCaseSensitiveAllowedSteps()
    {
        var previous = DslParserDemo.AllowedSteps;
        try
        {
            DslParserDemo.AllowedSteps = new HashSet<string>(StringComparer.Ordinal)
            {
                "normalize"
            };

            var result = DslParserDemo.Parse("NORMALIZE");

            Assert.False(result.IsSuccessState);
            Assert.Contains("Unknown step: NORMALIZE", result.Error.Message);
        }
        finally
        {
            DslParserDemo.AllowedSteps = previous;
        }
    }

    [Fact]
    public void ConvertBuildsLinearGraphFromDslDocument()
    {
        var parsed = DslParserDemo.Parse("""
            normalize
            structure
            provider
            polish
            """);

        Assert.True(parsed.IsSuccessState);
        var converted = DslToGraphDemo.Convert(parsed.Value);

        Assert.True(converted.IsSuccessState);
        Assert.IsType<DemoExecutionGraph>(converted.Value);
        Assert.Equal(4, converted.Value.Nodes.Count);
        Assert.Equal(
            ["normalize", "structure", "provider", "polish"],
            converted.Value.Nodes.Select(node => node.StepName));
    }
}
