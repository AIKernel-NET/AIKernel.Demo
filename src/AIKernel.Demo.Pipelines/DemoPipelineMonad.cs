using AIKernel.Common.Results;

namespace AIKernel.Demo.Pipelines;

public static class DemoPipelineMonad
{
    public static Task<Result<string>> RunAsync(string input)
        =>
            from normalized in NormalizeAsync(input)
            from compiled in CompileAsync(normalized)
            from executed in ExecuteAsync(compiled)
            select executed;

    public static Task<Result<string>> NormalizeAsync(string? input)
        => Task.FromResult(input is null
            ? Result<string>.Fail("Pipeline input is required.")
            : Result<string>.Success(input.Trim()));

    public static Task<Result<string>> CompileAsync(string normalized)
        => Task.FromResult(Result<string>.Success($"compiled({normalized})"));

    public static Task<Result<string>> ExecuteAsync(string compiled)
        => Task.FromResult(Result<string>.Success($"executed({compiled})"));
}
