# AIKernel.Demo.Cuda

Verifies a GPU memory round trip using the CUDA Driver bundled with `AIKernel.Kernel` 0.1.4. The demo requires the NVIDIA driver and a CUDA-capable GPU on Windows x64 or Linux x64.

```powershell
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

The demo creates a CUDA context on device 0, uploads four bytes to VRAM, and compares the downloaded bytes with the original. A successful run prints:

```text
AIKernel.Demo.Cuda
status=ok
backend=aikernel_cuda_driver
roundtrip.bytes=4
```

An unsupported platform, a missing NVIDIA driver, or no available CUDA device produces `status=skipped` with the reason. Other CUDA errors and transfer mismatches cause the command to fail.
