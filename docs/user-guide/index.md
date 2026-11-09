# AIKernel.Demo User Guide

This guide helps users run the AIKernel demo workspace without confusing sample
code with runtime ownership.

Demo is the AIOS SDK official examples workspace. Treat it like an
`/usr/share/examples` tree: run the samples to see how Core, Providers,
Control, Wasm, GPU, and Tools layers combine into an AIOS distribution.

AIKernel.Monolith is the official AIOS distribution now in development. It will
serve as the standard reference distribution that embodies semantic runtime,
capability graph, and governance after the 0.1.x line stabilizes.

## Build

```powershell
dotnet build AIKernel.Demo.slnx -c Release
```

## Recommended Learning Path

Use this order if you are new to AIKernel. Each step is dry-run friendly and
keeps external services behind package boundaries.

| Order | Run | Why it matters |
| --- | --- | --- |
| 1 | `AIKernel.Demo.CoreRuntime` | Confirms that Core routing, capability registry, VFS, clock, hosting, and kernel helper surfaces are available. |
| 2 | `AIKernel.Demo.Contracts` | Shows DTOs and execution hash-chain values as immutable data boundaries. |
| 3 | `AIKernel.Demo.StandardProviders` | Shows host OS driver surfaces without external network calls. |
| 4 | `AIKernel.Demo.Providers` | Shows extension Provider descriptors and manifests without live model invocation. |
| 5 | `AIKernel.Demo.Control` | Shows deterministic governance and emulator execution. |
| 6 | `AIKernel.Demo.Tools` | Shows inspection, replay, canonical formatting, ROM, and export tooling. |
| 7 | `AIKernel.Demo.Wasm` tests | Shows browser/WASM runtime contracts through deterministic test surfaces. |
| 8 | `AIKernel.Demo.Cuda` | Shows CUDA package identity and request validation, with deterministic non-Windows skip. |

## Run the Console Demo

```powershell
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
```

Use this path to validate the minimal pipeline shape: normalize input, run the
structured phase, select a provider, polish output, and write replay metadata.

## Run the API Demo

```powershell
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
```

The API host demonstrates an OpenAI-compatible endpoint implemented through
AIKernel routing, PDP decisions, and replay headers.

## Run OS Provider Examples

`AIKernel.Demo.Os` consumes `AIKernel.Providers.Standard` through the package
boundary. The executable tests cover CPU vector addition, Core process
supervision, and a safe one-minute scheduler example.

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter StandardProviderDemoSurfaceTests
```

## Run WASM Provider Examples

`AIKernel.Demo.Wasm` consumes `AIKernel.Wasm.Runtime` and the WebGPU compute
provider package. The executable tests cover the Core `IProcess` lifecycle and
WebGPU vector addition through deterministic CPU fallback.

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter WasmDemoSurfaceTests
```

## Run 0.1.3 Coverage Demos

These demos cover the major 0.1.3 package family without external network calls.
They are intended to show AIKernel as an OS-shaped runtime surface.
`AIKernel.Demo.Contracts` also constructs `AIKernel.Dtos.Execution.HashChain`
directly so the execution DTO boundary is visible without entering Core
runtime internals.

```powershell
dotnet run --project src/AIKernel.Demo.CoreRuntime/AIKernel.Demo.CoreRuntime.csproj -c Release
dotnet run --project src/AIKernel.Demo.Contracts/AIKernel.Demo.Contracts.csproj -c Release
dotnet run --project src/AIKernel.Demo.Control/AIKernel.Demo.Control.csproj -c Release
dotnet run --project src/AIKernel.Demo.Providers/AIKernel.Demo.Providers.csproj -c Release
dotnet run --project src/AIKernel.Demo.StandardProviders/AIKernel.Demo.StandardProviders.csproj -c Release
dotnet run --project src/AIKernel.Demo.Tools/AIKernel.Demo.Tools.csproj -c Release
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

The CUDA demo builds with the workspace but reports a deterministic skip outside
Windows-native CUDA environments.

`AIKernel.Tools.CLI` is a .NET tool package rather than an in-process library.
Install and exercise it through the `aik` command:

```powershell
dotnet tool install -g AIKernel.Tools.CLI --version 0.1.3
aik runtime ping
aik system info
aik system vfs --vfs-root .
aik capabilities list
```

## Run Python Demo Tests

```powershell
py -m pytest tests/AIKernel.Demo.Python.Tests
```

The Python demo mirrors the same contract ideas for teaching and smoke tests.

## Repository Boundaries

| Need | Repository |
| --- | --- |
| Runtime contracts and deterministic core behavior | AIKernel.Core |
| Standard providers and OS driver implementations for compute, file system, network, event bus, logging, process, scheduler, and profiler surfaces | AIKernel.Providers |
| CLI, replay, inspectors, and tooling | AIKernel.Tools |
| Physical execution engines | AIKernel.Control |
| Browser and WebAssembly runtime | AIKernel.Wasm |
| Runnable examples | AIKernel.Demo |

## Safe Usage

Keep demo changes focused on examples. When behavior belongs to a runtime,
provider, or CLI surface, move the implementation to the owning repository and
consume it from Demo.
