namespace AIKernel.Demo.Semantics;

public sealed record DemoSemanticDelta(
    string StepName,
    string Before,
    string After,
    DemoSemanticSlot Slot = DemoSemanticSlot.Orchestration);

public enum DemoSemanticSlot
{
    Orchestration,
    Expression,
    Material
}
