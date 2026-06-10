# AIKernel.Demo.Control

Shows Control/Bonsai governance through a deterministic emulator run. The demo does not execute external work.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.Control/AIKernel.Demo.Control.csproj -c Release
```

Expected log shape:

```text
AIKernel.Demo.Control
engine=control-emulator
graph=demo.control.graph
status=Completed
bonsai.map=IBonsaiInferenceKernel,...
```
