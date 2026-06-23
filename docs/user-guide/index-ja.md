# AIKernel.Demo User Guide

このガイドは、sample code と runtime ownership を混同せずに AIKernel.Demo を
実行するための手順をまとめたものです。

Demo は AIOS SDK の公式 example workspace です。OS の `/usr/share/examples`
のように扱い、Core、Providers、Control、Wasm、GPU、Tools layer を組み合わせて
AIOS distribution を構築する流れを sample から確認できます。

公式 AIOS ディストリビューション **AIKernel.Monolith** の開発も開始されています。
Monolith は 0.1.x 系の安定化後に semantic runtime、capability graph、governance を
体現する標準 reference distribution として位置づけられます。

## ビルド

```powershell
dotnet build AIKernel.Demo.slnx -c Release
```

## 推奨学習順

AIKernel に初めて触れる場合は、次の順番で確認してください。各 step は dry-run
で理解できるように設計されており、外部 service は package boundary の外側に
閉じています。

| Order | 実行対象 | 何を理解できるか |
| --- | --- | --- |
| 1 | `AIKernel.Demo.CoreRuntime` | Core routing、capability registry、VFS、clock、hosting、kernel helper surface が利用可能であること。 |
| 2 | `AIKernel.Demo.Contracts` | DTO と execution hash-chain が immutable data boundary として扱われること。 |
| 3 | `AIKernel.Demo.StandardProviders` | 外部 network call なしで host OS driver surface を確認できること。 |
| 4 | `AIKernel.Demo.Providers` | live model invocation を行わずに extension Provider descriptor / manifest を確認できること。 |
| 5 | `AIKernel.Demo.Control` | deterministic governance と emulator execution の関係。 |
| 6 | `AIKernel.Demo.Tools` | inspection、replay、canonical formatting、ROM、export tooling の役割。 |
| 7 | `AIKernel.Demo.Wasm` tests | deterministic test surface を通じた browser / WASM runtime contract。 |
| 8 | `AIKernel.Demo.Cuda` | CUDA package identity と request validation。非 Windows では deterministic skip。 |

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

## 0.1.3 Coverage Demo の実行

これらの Demo は、外部 network call なしで 0.1.3 package family の主要部分を
カバーします。AIKernel を OS-shaped runtime surface として理解するための入口です。
`AIKernel.Demo.Contracts` では `AIKernel.Dtos.Execution.HashChain` も直接生成し、
Core runtime internal に入らず execution DTO boundary を確認できるようにしています。

```powershell
dotnet run --project src/AIKernel.Demo.CoreRuntime/AIKernel.Demo.CoreRuntime.csproj -c Release
dotnet run --project src/AIKernel.Demo.Contracts/AIKernel.Demo.Contracts.csproj -c Release
dotnet run --project src/AIKernel.Demo.Control/AIKernel.Demo.Control.csproj -c Release
dotnet run --project src/AIKernel.Demo.Providers/AIKernel.Demo.Providers.csproj -c Release
dotnet run --project src/AIKernel.Demo.StandardProviders/AIKernel.Demo.StandardProviders.csproj -c Release
dotnet run --project src/AIKernel.Demo.Tools/AIKernel.Demo.Tools.csproj -c Release
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

CUDA demo は workspace と一緒に build できますが、Windows native CUDA environment
以外では deterministic skip を返します。

`AIKernel.Tools.CLI` は in-process library ではなく .NET tool package です。
`aik` command として install し、OS command surface を直接確認します。

```powershell
dotnet tool install -g AIKernel.Tools.CLI --version 0.1.3
aik runtime ping
aik system info
aik system vfs --vfs-root .
aik capabilities list
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
| compute、file system、network、event bus、logging、process、scheduler、profiler surface の standard provider / OS driver implementation | AIKernel.Providers |
| CLI、replay、inspector、tooling | AIKernel.Tools |
| physical execution engine | AIKernel.Control |
| browser / WebAssembly runtime | AIKernel.Wasm |
| runnable example | AIKernel.Demo |

## Safe Usage

Demo への変更は example に集中させます。runtime、provider、CLI surface に属する
振る舞いは、責務を持つ repository へ移動し、Demo から利用してください。
