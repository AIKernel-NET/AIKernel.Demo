# AIKernel.Demo User Guide

このガイドは、sample code と runtime ownership を混同せずに AIKernel.Demo を
実行するための手順をまとめたものです。

## ビルド

```powershell
dotnet build AIKernel.Demo.slnx -c Release
```

## Console Demo の実行

```powershell
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
```

この demo では、input normalize、structured phase、provider selection、
output polish、replay metadata という最小 pipeline の形を確認できます。

## API Demo の実行

```powershell
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
```

API host は、AIKernel routing、PDP decision、replay header を通じて
OpenAI-compatible endpoint を実装する例です。

## OS Provider Examples

`AIKernel.Demo.Os` は `AIKernel.Providers.Standard` を package 境界越しに
消費します。実行可能な test で、CPU vector addition、Core process
supervision、安全な 1 分間隔 scheduler example を確認します。

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter StandardProviderDemoSurfaceTests
```

## WASM Provider Examples

`AIKernel.Demo.Wasm` は `AIKernel.Wasm.Runtime` と WebGPU compute provider package
を消費します。実行可能な test で、Core `IProcess` lifecycle と deterministic
CPU fallback 経由の WebGPU vector addition を確認します。

```powershell
dotnet test tests/AIKernel.Demo.Tests/AIKernel.Demo.Tests.csproj -c Release --filter WasmDemoSurfaceTests
```

## Python Demo Tests

```powershell
py -m pytest tests/AIKernel.Demo.Python.Tests
```

Python demo は、同じ contract idea を教材と smoke test として再現します。

## Repository 境界

| Need | Repository |
| --- | --- |
| runtime contract と deterministic core behavior | AIKernel.Core |
| standard provider と OS driver implementation | AIKernel.Providers |
| CLI、replay、inspector、tooling | AIKernel.Tools |
| physical execution engine | AIKernel.Control |
| browser / WebAssembly runtime | AIKernel.Wasm |
| standard OS driver implementation | AIKernel.Providers |
| runnable example | AIKernel.Demo |

## Safe Usage

Demo への変更は example に集中させます。runtime、provider、CLI surface に属する
振る舞いは、責務を持つ repository へ移動し、Demo から利用してください。
