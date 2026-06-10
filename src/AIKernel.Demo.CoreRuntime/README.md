# AIKernel.Demo.CoreRuntime

Shows the AIKernel Core runtime surface: semantic routing, capability registry, kernel clock, VFS provider, hosting extension types, and kernel helper types.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.CoreRuntime/AIKernel.Demo.CoreRuntime.csproj -c Release
```

Expected log shape:

```text
AIKernel.Demo.CoreRuntime
router.route=local.default
registry=InMemoryCapabilityRegistry
clock.replaying=False
vfs.provider=MemoryFileProvider
contract.map=AIKernelCoreBuilder,...
```
