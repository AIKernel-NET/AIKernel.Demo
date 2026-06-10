# AIKernel.Demo

[English README](README.md)

AIKernel.Demo は、AIKernel 0.1.1 package family の利用者向け sample workspace です。
AIKernel.Core の抽象を、console、API host、browser、VFS、PDP、pipeline、
replay-inspection demo として実際に見える形にします。

## リポジトリの役割

AIKernel.Demo は、application が Core、Kernel、Capability module、VFS、PDP、
pipeline、Replay inspection をどのように合成するかを示します。Demo 専用 code を
contract repository や runtime repository に入れません。

この repository は、完了した 0.1.0 prototype validation phase から、公開済みの
Core、Control、Providers、Wasm、Tools package contract を消費する 0.1.1 release line
への移行を示します。

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
  WebGPU CPU fallback、DAG step animation、PromptRules signature verification の
  browser Playground。
- `AIKernel.Demo.Os` - `AIKernel.Providers.Standard` を消費し、CPU compute、
  process supervision、安全な scheduler example を示す standard OS provider demo。
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

## ビルド

```powershell
dotnet build AIKernel.Demo.slnx
dotnet run --project src/AIKernel.Demo.Console/AIKernel.Demo.Console.csproj
dotnet run --project src/AIKernel.Demo.WebApi/AIKernel.Demo.WebApi.csproj
py -m pytest tests/AIKernel.Demo.Python.Tests
```

共通 project property は `Directory.Build.props` に集約されています。

## Source Alignment

Demo は Core の design decision を意図的に反映します。Pipeline は TaskManager によって
決定論的に制御される DAG であり、Provider は交換可能な Capability を宣言し、LLM は
提案者で PDP が最終決定者です。Replay は同じ execution を再実行するために必要な
material を保存します。

0.1.1 release には `AIKernel.Demo.Pipelines` の contract-alignment smoke path が
含まれます。Routing data は `AIKernel.Dtos.Routing.KernelProviderRoutingDecision`、
DSL semantic IR は `AIKernel.Dtos.Dsl` を通じて構築します。Demo code は
AIKernel.NET contracts の consumer であり、Core internal DSL / History runtime type には
依存しません。

0.1.1 release では、`AIKernelPackageVersion` と Core、Providers、Wasm、Tools の
package version property は公開済みの 0.1.1 package family を指します。Demo は
個別に package 公開する対象ではなく、release validation workspace として扱います。

## コントリビュータ向けガイドライン

Demo の変更は、AIKernel 共通の開発規律に従ってください。

- [AIKernel 開発ガイドライン](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES-jp.md)
- [AIKernel Development Guidelines](../AIKernel.NET/docs/guidelines/AIKERNEL_DEVELOPMENT_GUIDELINES.md)

Demo は public contract の consumer に留め、internal runtime type へ依存せず、
deterministic replay example を維持し、local validation で使用する package
version assumption を明記してください。

## ライセンス

Apache License 2.0.
