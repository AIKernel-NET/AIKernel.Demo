namespace AIKernel.Demo.Semantics;

public sealed class DemoSemanticState
{
    private readonly SortedDictionary<string, string> _orchestrationState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _expressionState = new(StringComparer.Ordinal);
    private readonly SortedDictionary<string, string> _materialState = new(StringComparer.Ordinal);

    public IReadOnlyDictionary<string, string> Values => _orchestrationState;
    public IReadOnlyDictionary<string, string> OrchestrationState => _orchestrationState;
    public IReadOnlyDictionary<string, string> ExpressionState => _expressionState;
    public IReadOnlyDictionary<string, string> MaterialState => _materialState;

    public DemoSemanticState Apply(
        DemoSemanticDelta delta)
    {
        ArgumentNullException.ThrowIfNull(delta);

        var next = Clone();
        next.TargetState(delta.Slot)[delta.StepName] = delta.After;
        return next;
    }

    public static DemoSemanticState FromDeltas(
        IEnumerable<DemoSemanticDelta> deltas)
    {
        ArgumentNullException.ThrowIfNull(deltas);

        var state = new DemoSemanticState();
        foreach (var delta in deltas)
            state = state.Apply(delta);

        return state;
    }

    private DemoSemanticState Clone()
    {
        var next = new DemoSemanticState();
        CopyTo(_orchestrationState, next._orchestrationState);
        CopyTo(_expressionState, next._expressionState);
        CopyTo(_materialState, next._materialState);
        return next;
    }

    private SortedDictionary<string, string> TargetState(
        DemoSemanticSlot slot)
        => slot switch
        {
            DemoSemanticSlot.Orchestration => _orchestrationState,
            DemoSemanticSlot.Expression => _expressionState,
            DemoSemanticSlot.Material => _materialState,
            _ => _orchestrationState
        };

    private static void CopyTo(
        SortedDictionary<string, string> source,
        SortedDictionary<string, string> target)
    {
        foreach (var item in source)
            target[item.Key] = item.Value;
    }
}
