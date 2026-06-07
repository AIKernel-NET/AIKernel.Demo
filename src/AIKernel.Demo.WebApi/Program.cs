// <summary>
// [EN] AIKernel.Demo.WebApi is a top-level demo entry point.
// [JA] AIKernel.Demo.WebApi は top-level の Demo entry point です。
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

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/demo/pipeline"));
app.MapGet("/demo/pipeline", () =>
{
    var run = DemoPipelineCatalog.CreateDefaultRun();

    return Results.Ok(new
    {
        run.PipelineId,
        run.Decision,
        run.ContractAlignment,
        run.StepCount,
        Replay = ReplaySummaryFormatter.Format(run.ReplayHash, run.StepCount)
    });
});

app.Run();
