namespace AIKernel.Demo.Pipelines;

public sealed record DemoPipelineRun(
    string PipelineId,
    string Decision,
    string ReplayHash,
    int StepCount);
