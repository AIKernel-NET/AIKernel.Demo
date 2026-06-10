# AIKernel.Demo.Cuda

Shows the CUDA 13.0 package through a guarded dry-run. The demo builds everywhere but only reports CUDA as runnable on Windows.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

Expected log shape on Windows:

```text
AIKernel.Demo.Cuda
status=dry-run
capability=libtorch.llama.cuda13.0.libtorch2.12.win-x64
request.valid=True
cuda.map=LibTorchCapabilityInvoker,...
```

Expected log shape outside Windows:

```text
AIKernel.Demo.Cuda
status=skipped
reason=CUDA package is Windows native
capability=libtorch.llama.cuda13.0.libtorch2.12.win-x64
```
