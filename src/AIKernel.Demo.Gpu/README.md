# AIKernel.Demo.Gpu

Direct console entry point for the canonical AIKernel 0.1.3 GPU rev3 teaching
path.

This project calls `AIKernel.Demo.Wasm.WasmDemoSurface.RunGpuRev3PipelineAsync`
and prints the shared GPU pass vocabulary used by browser WebGPU, native Dawn,
and CUDA-facing providers:

- `gpu.aisthesis.raw-frame`
- `gpu.spatial-reasoning`
- `gpu.hud.composite`

The demo intentionally uses deterministic CPU fallback so it can run during
package validation without browser GPU hardware, native CUDA dispatch, or Dawn
runtime loading.

```powershell
dotnet run --project src/AIKernel.Demo.Gpu/AIKernel.Demo.Gpu.csproj -c Release
```
