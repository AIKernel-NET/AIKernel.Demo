# AIKernel.Demo.Tools

Shows instrumentation, canonical formatting, replay, chat-history export helpers, clock inspectors, VFS inspectors, and ROM bridge surfaces.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.Tools/AIKernel.Demo.Tools.csproj -c Release
```

Expected log shape:

```text
AIKernel.Demo.Tools
format={ demo = tools, status = ok }
inspect=type: System.String
replay.events=2
export.markdown.length=...
export.rom.length=...
tools.map=CanonicalSerializer,...
```
