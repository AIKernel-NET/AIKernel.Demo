# AIKernel.Demo.Wasm

Shows the AIKernel WASM runtime surface through executable tests. The demo
covers process lifecycle, memory, file system, event, audio, screenshot,
save-state, time, WebGPU descriptor/backend surface, deterministic CPU
fallback for vector addition, and the canonical GPU rev3 HUD/Aisthesis/spatial
pipeline.

Run:

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter WasmDemoSurfaceTests
```

Expected test surface:

```text
WasmDemoSurfaceTests.ProcessLifecycle...
WasmDemoSurfaceTests.WebGpuFallback...
WasmDemoSurfaceTests.RunGpuRev3Pipeline...
```

The WebGPU paths use deterministic CPU fallback for automated tests. The rev3
pipeline still exercises the canonical pass vocabulary:

- `gpu.aisthesis.raw-frame`
- `gpu.spatial-reasoning`
- `gpu.hud.composite`

It also fixes the public teaching path for raw-framebuffer Aisthesis,
offscreen HUD composition, and the `topos,route,threat,zoe` spatial matrix
order. Real GPU browser validation belongs to manual or browser-hosted E2E
testing.
