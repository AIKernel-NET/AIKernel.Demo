# AIKernel.Demo

[English README](README.md)

AIKernel.Demo は、AIKernel 0.1.0 package family の利用者向け sample workspace です。
AIKernel.Core の抽象を、console、API host、browser、VFS、PDP、pipeline、
replay-inspection demo として実際に見える形にします。

## リポジトリの役割

AIKernel.Demo は、application が Core、Kernel、Capability module、VFS、PDP、
pipeline、Replay inspection をどのように合成するかを示します。Demo 専用 code を
contract repository や runtime repository に入れません。

この repository は、完了した 0.0.x design-implementation phase から、2026-06-09
予定の 0.1.0 prototype validation phase への移行を示します。

Demo は利用者側です。Runtime execution engine は AIKernel.Control に属します。
外部 Capability module は AIKernel.Tools または専用 Capability repository に属します。

## プロジェクト

- `AIKernel.Demo.Console` - 1 回の inference pipeline を実行する最小 CLI demo。
  Kernel/Provider DI、`IInputNormalizer -> StructurePhase -> Provider ->
  OutputPolisher`、deterministic replay logging、Mock/OpenAI/Local provider 選択を扱います。
- `AIKernel.Demo.WebApi` - `/v1/chat/completions` を AIKernel 経由で実装する
  OpenAI-compatible API host demo。ProviderRouter、PDP Allow/Deny、ReplayLog HTTP
  response headers を扱います。
- `AIKernel.Demo.Wasm` - Context、Execution、VFS 可視化、WebAssembly provider call、
  DAG step animation、PromptRules signature verification の browser Playground。
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

## ドキュメント

- [Architecture](docs/architecture/index-ja.md)
- [Pipelines](docs/pipelines/index-ja.md)

## ビルド

```powershell
dotnet build AIKernel.Demo.slnx
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
```

共通 project property は `Directory.Build.props` に集約されています。

## Source Alignment

Demo は Core の design decision を意図的に反映します。Pipeline は TaskManager によって
決定論的に制御される DAG であり、Provider は交換可能な Capability を宣言し、LLM は
提案者で PDP が最終決定者です。Replay は同じ execution を再実行するために必要な
material を保存します。

初期 0.1.0 prototype には `AIKernel.Demo.Pipelines` の contract-alignment smoke path が
含まれます。Routing data は `AIKernel.Dtos.Routing.KernelProviderRoutingDecision`、
DSL semantic IR は `AIKernel.Dtos.Dsl` を通じて構築します。Demo code は
AIKernel.NET contracts の consumer であり、Core internal DSL / History runtime type には
依存しません。

0.1.0 prototype development 中は、NuGet cache collision を避けるために
`AIKernelPackageVersion` が `0.1.0.2` のような local build を指す場合があります。
Public release build では package family を固定版の 0.1.0 release version に揃えます。

## ライセンス

Apache License 2.0.
