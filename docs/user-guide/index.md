# AIKernel.Demo User Guide

This guide helps users run the AIKernel demo workspace without confusing sample
code with runtime ownership.

## Build

```powershell
dotnet build AIKernel.Demo.slnx -c Release
```

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

## Run Python Demo Tests

```powershell
py -m pytest tests/AIKernel.Demo.Python.Tests
```

The Python demo mirrors the same contract ideas for teaching and smoke tests.

## Repository Boundaries

| Need | Repository |
| --- | --- |
| Runtime contracts and deterministic core behavior | AIKernel.Core |
| Standard providers and OS driver implementations | AIKernel.Providers |
| CLI, replay, inspectors, and tooling | AIKernel.Tools |
| Physical execution engines | AIKernel.Control |
| Browser and WebAssembly runtime | AIKernel.Wasm |
| Standard OS driver implementations | AIKernel.Providers |
| Runnable examples | AIKernel.Demo |

## Safe Usage

Keep demo changes focused on examples. When behavior belongs to a runtime,
provider, or CLI surface, move the implementation to the owning repository and
consume it from Demo.
