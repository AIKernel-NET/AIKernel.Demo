# AIKernel.Demo.Wasm

Shows the AIKernel WASM runtime surface through executable tests. The demo
covers process lifecycle, memory, file system, event, audio, screenshot,
save-state, time, WebGPU descriptor/backend surface, and deterministic CPU
fallback for vector addition.

Run:

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter WasmDemoSurfaceTests
```

Expected test surface:

```text
WasmDemoSurfaceTests.ProcessLifecycle...
WasmDemoSurfaceTests.WebGpuFallback...
```

The WebGPU path uses deterministic CPU fallback for automated tests. Real GPU
browser validation belongs to manual or browser-hosted E2E testing.
