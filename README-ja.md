# AIKernel.Demo

[English README](README.md)

AIKernel.Demo は、AIKernel 0.1.2 package family の利用者向け sample workspace です。
AIKernel.Core の抽象を、console、API host、browser、VFS、PDP、pipeline、
replay-inspection demo として実際に見える形にします。

AIOS SDK において、AIKernel.Demo は OS の `/usr/share/examples` に相当する
公式 example workspace です。kernel runtime、provider、control、WASM、GPU、
tools layer を組み合わせ、独自の AIOS distribution を構築する流れを示します。

AIKernel には、公式 AIOS ディストリビューションである **AIKernel.Monolith** もあります。
Monolith は 0.1.x 系の安定化後に全 SDK layer を統合する標準 AIOS として
開発が開始されています。Demo はその layer を理解するための教材面を担います。

## リポジトリの役割

AIKernel.Demo は、application が Core、Kernel、Capability module、VFS、PDP、
pipeline、Replay inspection をどのように合成するかを示します。Demo 専用 code を
contract repository や runtime repository に入れません。

この repository は、完了した 0.1.0 prototype validation phase から、公開済みの
Core、Control、Providers、Wasm、Tools package contract を消費する 0.1.2 release line
への移行を示します。

Demo は利用者側です。Runtime execution engine は AIKernel.Control に属します。
外部 Provider / Capability module は AIKernel.Providers または専用 runtime repository に
属します。CLI、replay、inspector、instrumentation は AIKernel.Tools に属します。

## プロジェクト

- `AIKernel.Demo.Console` - 1 回の inference pipeline を実行する最小 CLI demo。
  Kernel/Provider DI、`IInputNormalizer -> StructurePhase -> Provider ->
  OutputPolisher`、deterministic replay logging、Mock/OpenAI/Local provider 選択を扱います。
- `AIKernel.Demo.WebApi` - `/v1/chat/completions` を AIKernel 経由で実装する
  OpenAI-compatible API host demo。ProviderRouter、PDP Allow/Deny、ReplayLog HTTP
  response headers を扱います。
- `AIKernel.Demo.Wasm` - Context、Execution、VFS 可視化、WebAssembly provider call、
  WebGPU CPU fallback、DAG step animation、PromptRules signature verification の
  browser Playground。
- `AIKernel.Demo.Os` - `AIKernel.Providers.Standard` を消費し、CPU compute、
  process supervision、安全な scheduler example を示す standard OS provider demo。
- `AIKernel.Demo.CoreRuntime` - routing、capability registry、clock、VFS、Hosting、
  Kernel helper surface を扱う Core runtime demo。
- `AIKernel.Demo.Contracts` - public Contracts、DTO、Enums、execution HashChain DTO
  を data-only boundary object として示す demo。
- `AIKernel.Demo.Control` - deterministic governance execution を扱う Control / Bonsai
  emulator demo。
- `AIKernel.Demo.Providers` - ChatHistory、ChatOpenAI、CudaCompute、LocalLlm、
  MicrosoftAI、DynamicPipelineCompiler を dry-run で扱う公式 external Provider demo。
- `AIKernel.Demo.StandardProviders` - file system、logging、event bus、network metadata、
  profiler surface を扱う standard OS driver demo。
- `AIKernel.Demo.Tools` - instrumentation、canonical formatting、replay、inspector、ROM、
  export helper を扱う demo。
- `AIKernel.Demo.Cuda` - Windows native CUDA 13.0 dry-run demo。Windows 以外では
  deterministic skip を返します。
- `AIKernel.Demo.Providers.Mock` - contract test 用の deterministic mock Provider。
  chat/embedding capability declaration、fixed response、ProviderRouter behavior を確認します。
- `AIKernel.Demo.Vfs.Git` - Git repository を virtual file system として mount し、
  RAG MaterialContext separation を示す Git-backed VFS demo。
- `AIKernel.Demo.Pipelines` - Chat、RAG、Reasoning、Multi-step / Multi-model routing
  pipeline examples。
- `AIKernel.Demo.PDP` - Allow、Deny、Require-Review、cost limit、external-send guard、
  audit log の PDP visualization。
- `AIKernel.Demo.ReplayInspector` - ReplayLog loading、provider selection replay、
  PromptRules version comparison、ExecutionState diff の deterministic replay inspector。
- `AIKernel.Demo.Python` - 同じ contract semantics を Python で再現する教材デモ。
  DSL parsing、モナド風 pipeline、VFS snapshot、semantic delta、deterministic replay を扱います。

## ドキュメント

- [Documentation index](docs/README-ja.md)
- [User Guide](docs/user-guide/index-ja.md)
- [Architecture](docs/architecture/index-ja.md)
- [Pipelines](docs/pipelines/index-ja.md)

## クイックスタート

まず Release build を行い、次に 0.1.2 の public package surface が利用できることを
最小 demo で確認します。以下のコマンドは外部 network、secret、model download、
native CUDA hardware を必要としません。

最初に触るべき demo: `AIKernel.Demo.CoreRuntime`

```powershell
dotnet build AIKernel.Demo.slnx -c Release
dotnet run --project src/AIKernel.Demo.CoreRuntime/AIKernel.Demo.CoreRuntime.csproj -c Release
dotnet run --project src/AIKernel.Demo.Contracts/AIKernel.Demo.Contracts.csproj -c Release
dotnet run --project src/AIKernel.Demo.StandardProviders/AIKernel.Demo.StandardProviders.csproj -c Release
```

共通 project property は `Directory.Build.props` に集約されています。

## Demo Map の実行順

AIKernel 0.1.2 package family を、個別 sample ではなく OS-shaped runtime として
理解したい場合は、以下の順番で実行してください。

| Step | Demo | 利用者が理解できること |
| --- | --- | --- |
| 1 | `AIKernel.Demo.CoreRuntime` | routing、capability registration、VFS、clock、hosting、kernel helper surface のつながり。 |
| 2 | `AIKernel.Demo.Contracts` | DTO、enum、orchestration context、policy result、execution hash-chain が data-only boundary として扱われること。 |
| 3 | `AIKernel.Demo.Control` | Control / Bonsai surface による deterministic governance execution の形。 |
| 4 | `AIKernel.Demo.Providers` | 公式 extension Provider が descriptor、ID、invoker を公開しつつ live external call を行わない境界。 |
| 5 | `AIKernel.Demo.StandardProviders` | file system、logging、event bus、network metadata、profiler を OS driver として扱う方法。 |
| 6 | `AIKernel.Demo.Tools` | canonical formatting、inspection、replay、ROM、export helper による再現可能な診断。 |
| 7 | `AIKernel.Demo.Wasm` | browser / WASM runtime surface を process lifecycle と deterministic WebGPU fallback path で検証する方法。 |
| 8 | `AIKernel.Demo.Cuda` | Windows-native CUDA package contract を見せつつ、非 Windows では deterministic skip する設計。 |

console demo をまとめて実行する場合:

```powershell
dotnet run --project src/AIKernel.Demo.Control/AIKernel.Demo.Control.csproj -c Release
dotnet run --project src/AIKernel.Demo.Providers/AIKernel.Demo.Providers.csproj -c Release
dotnet run --project src/AIKernel.Demo.Tools/AIKernel.Demo.Tools.csproj -c Release
dotnet run --project src/AIKernel.Demo.Cuda/AIKernel.Demo.Cuda.csproj -c Release
```

validation test を実行する場合:

```powershell
dotnet test AIKernel.Demo.slnx -c Release --no-build
py -m pytest tests/AIKernel.Demo.Python.Tests
```

## CLI ツール

`AIKernel.Tools.CLI` は .NET tool package として公開されるため、in-process の
demo project から `PackageReference` で消費する対象ではありません。`aik` command
として install し、OS command surface を直接実行します。

```powershell
dotnet tool install -g AIKernel.Tools.CLI --version 0.1.2
aik runtime ping
aik system info
aik system vfs --vfs-root .
aik capabilities list
```

## Source Alignment

Demo は Core の design decision を意図的に反映します。Pipeline は TaskManager によって
決定論的に制御される DAG であり、Provider は交換可能な Capability を宣言し、LLM は
提案者で PDP が最終決定者です。Replay は同じ execution を再実行するために必要な
material を保存します。

0.1.2 release には `AIKernel.Demo.Contracts` と `AIKernel.Demo.Pipelines` の
contract-alignment smoke path が含まれます。Execution hash-chain data は
`AIKernel.Dtos.Execution.HashChain`、Routing data は
`AIKernel.Dtos.Routing.KernelProviderRoutingDecision`、DSL semantic IR は
`AIKernel.Dtos.Dsl` を通じて構築します。Demo code は AIKernel.NET contracts の
consumer であり、Core internal DSL / History runtime type には依存しません。

0.1.2 release では、`AIKernelPackageVersion` と Core、Control、Cuda、Providers、
Wasm、Tools の package version property は公開済みの 0.1.2 package family を指します。
Demo は個別に package 公開する対象ではなく、release validation workspace として扱います。

## コントリビュータ向けガイドライン

Demo の変更は、AIKernel 共通の開発規律に従ってください。

- [AIKernel 開発ガイドライン](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES-jp.md)
- [AIKernel Development Guidelines](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES.md)

Demo は public contract の consumer に留め、internal runtime type へ依存せず、
deterministic replay example を維持し、local validation で使用する package
version assumption を明記してください。

## ライセンス

Apache License 2.0.
