using AIKernel.Demo.Pipelines;
using AIKernel.Demo.ReplayInspector;

var run = DemoPipelineCatalog.CreateDefaultRun();

Console.WriteLine("AIKernel.Demo.Console");
Console.WriteLine($"Pipeline: {run.PipelineId}");
Console.WriteLine($"Decision: {run.Decision}");
Console.WriteLine($"Routing: {run.ContractAlignment.RoutingProviderId}/{run.ContractAlignment.RoutingModelId}");
Console.WriteLine($"Replay: {ReplaySummaryFormatter.Format(run.ReplayHash, run.StepCount)}");
