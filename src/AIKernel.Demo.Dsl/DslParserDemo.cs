using AIKernel.Common.Results;
using AIKernel.Dtos.Dsl;

namespace AIKernel.Demo.Dsl;

public static class DslParserDemo
{
    public static IReadOnlySet<string> AllowedSteps { get; set; } = new HashSet<string>(StringComparer.Ordinal)
    {
        "normalize",
        "structure",
        "provider",
        "polish",
    };

    public static Result<DslDocument> Parse(string text)
    {
        if (text is null)
            return Result<DslDocument>.Fail("DSL text is required.");

        var names = text
            .Replace("\r\n", "\n", StringComparison.Ordinal)
            .Replace('\r', '\n')
            .Split('\n')
            .Select(line => line.Trim())
            .Where(line => line.Length > 0)
            .Where(line => !line.StartsWith("#", StringComparison.Ordinal))
            .ToArray();

        foreach (var name in names)
        {
            if (!AllowedSteps.Contains(name))
                return Result<DslDocument>.Fail($"Unknown step: {name}");
        }

        var steps = names
            .Select<string, PipelineNode>(name => new StepNode(name))
            .ToArray();

        return Result<DslDocument>.Success(new DslDocument(new PipelineRootNode(steps)));
    }
}
