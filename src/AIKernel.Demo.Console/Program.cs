// <summary>
// [EN] AIKernel.Demo.Console is a top-level demo entry point.
// [JA] AIKernel.Demo.Console は top-level の Demo entry point です。
// </summary>
// <remarks>
// [EN] Top-level statements cannot receive attached XML documentation comments,
// [EN] so this file uses XML tag-shaped line comments to keep the same readable
// [EN] teaching structure without changing runtime behavior.
// [JA] top-level statement には XML documentation comment を紐づけられないため、
// [JA] runtime behavior を変えずに同じ教材構造を保つ目的で XML tag 形式の
// [JA] line comment を使用します。
// </remarks>

using AIKernel.Demo.Pipelines;
using AIKernel.Demo.ReplayInspector;

var run = DemoPipelineCatalog.CreateDefaultRun();

Console.WriteLine("AIKernel.Demo.Console");
Console.WriteLine($"Pipeline: {run.PipelineId}");
Console.WriteLine($"Decision: {run.Decision}");
Console.WriteLine($"Routing: {run.ContractAlignment.RoutingProviderId}/{run.ContractAlignment.RoutingModelId}");
Console.WriteLine($"Replay: {ReplaySummaryFormatter.Format(run.ReplayHash, run.StepCount)}");
