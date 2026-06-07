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
