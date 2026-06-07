using AIKernel.Demo.Semantics;

namespace AIKernel.Demo.KernelExecution;

public sealed class DemoExecutionContext
{
    private readonly SortedDictionary<string, string> _orchestrationState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _expressionState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _materialState = new(StringComparer.Ordinal);

    public IReadOnlyDictionary<string, string> OrchestrationState => _orchestrationState;
    public IReadOnlyDictionary<string, string> ExpressionState => _expressionState;
    public IReadOnlyDictionary<string, string> MaterialState => _materialState;

    public DemoExecutionContext ApplyDelta(
        DemoSemanticDelta delta)
    {
        ArgumentNullException.ThrowIfNull(delta);

        _orchestrationState[delta.StepName] = delta.After;
        _expressionState["last_step"] = delta.StepName;
        _materialState[$"{delta.StepName}.transition"] = $"{delta.Before}->{delta.After}";

        return this;
    }
}
