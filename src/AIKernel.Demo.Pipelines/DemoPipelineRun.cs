using AIKernel.Demo.PDP;

namespace AIKernel.Demo.Pipelines;

public sealed record DemoPipelineRun(
    string PipelineId,
    DemoPolicyCode Decision,
    string ReplayHash,
    int StepCount,
    DemoContractAlignment ContractAlignment);
