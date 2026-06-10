# AIKernel.Demo.StandardProviders

Shows standard OS driver providers for file system, logging, event bus, network metadata, and profiling.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.StandardProviders/AIKernel.Demo.StandardProviders.csproj -c Release
```

Expected log shape:

```text
AIKernel.Demo.StandardProviders
fs.exists=True
fs.text=AIKernel standard provider demo
event.observed=2
profiler.memory=...
standard.map=FileSystemProviderBase,...
```
