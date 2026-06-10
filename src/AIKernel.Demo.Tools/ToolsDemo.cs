using AIKernel.Tools.Capability.RomStorage;
using AIKernel.Tools.Inspectors.ChatHistoryScraper;
using AIKernel.Tools.Inspectors.ChatHistoryScraper.Export;
using AIKernel.Tools.Inspectors.KernelClock.Commands;
using AIKernel.Tools.Inspectors.Vfs.Commands;
using AIKernel.Tools.Instrumentation;
using AIKernel.Providers.ChatHistory;

namespace AIKernel.Demo.Tools;

/// <summary>
/// [EN] Demonstrates AIKernel tooling for canonicalization, replay, inspectors, and ROM bridge surfaces.
/// [JA] canonicalization、replay、inspector、ROM bridge surface 向け AIKernel tooling を示します。
/// </summary>
/// <remarks>
/// [EN] Tools are operational surfaces rather than runtime owners. This demo shows formatting, inspection, replay, chat-history export, clock inspectors, VFS inspectors, and ROM bridge types with local deterministic input.
/// [JA] Tools は runtime owner ではなく operational surface です。このデモは local deterministic input を使って formatting、inspection、replay、chat-history export、clock inspector、VFS inspector、ROM bridge type を示します。
/// </remarks>
public static class ToolsDemo
{
    /// <summary>
    /// [EN] Runs the tooling golden path with a local temporary replay file.
    /// [JA] local temporary replay file を使って tooling golden path を実行します。
    /// </summary>
    /// <remarks>
    /// [EN] The temporary replay file and fixed chat-history records make the example repeatable. The exporter lines demonstrate how operational evidence can be rendered into Markdown and ROM-style artifacts.
    /// [JA] temporary replay file と固定 chat-history record により、この例は再実行可能です。exporter の行は operational evidence を Markdown と ROM-style artifact に変換する方法を示します。
    /// </remarks>
    /// <returns>
    /// [EN] A deterministic tooling log that can be compared across runs.
    /// [JA] 実行間で比較できる決定論的 tooling log です。
    /// </returns>
    public static IReadOnlyList<string> Run()
    {
        var formatter = new CanonicalFormatter();
        var inspector = new Inspector();
        var replayPath = Path.Combine(Path.GetTempPath(), "aikernel-demo-replay.log");
        File.WriteAllLines(replayPath, ["event:boot", "event:halt"]);
        var replay = new ReplayEngine().Load(replayPath).Run();
        var records = new[]
        {
            new ChatHistoryRecord
            {
                Role = "user",
                Content = "inspect the kernel",
                Timestamp = new DateTimeOffset(2026, 6, 10, 0, 0, 0, TimeSpan.Zero)
            },
            new ChatHistoryRecord
            {
                Role = "assistant",
                Content = "kernel inspection complete",
                Timestamp = new DateTimeOffset(2026, 6, 10, 0, 0, 1, TimeSpan.Zero)
            }
        };
        var markdown = MdExporter.ToMarkdown(records);
        var rom = RomExporter.ToRom(records);
        var typeMap = new[]
        {
            typeof(CanonicalSerializer).Name,
            typeof(ReplaySession).Name,
            typeof(ChatHistoryScraper).Name,
            typeof(AIKernel.Tools.Inspectors.ChatHistoryScraper.Export.ChatHistoryPythonBridge).Name,
            typeof(RomStoragePythonBridge).Name,
            typeof(NowCommand).Name,
            typeof(TimelineCommand).Name,
            typeof(InfoCommand).Name,
            typeof(TreeCommand).Name
        };

        return
        [
            "AIKernel.Demo.Tools",
            $"format={formatter.Format(new { demo = "tools", status = "ok" })}",
            $"inspect={inspector.Inspect(replay.State)}",
            $"replay.events={replay.Events.Count}",
            $"export.markdown.length={markdown.Length}",
            $"export.rom.length={rom.Length}",
            $"tools.map={string.Join(',', typeMap)}"
        ];
    }
}
