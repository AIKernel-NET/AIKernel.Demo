# AIKernel.Demo ドキュメント

AIKernel.Demo は、AIKernel package family の実行可能な sample workspace です。
どの demo を実行するか、ローカルでどう確認するか、背後の runtime 責務が
どの repository に属するかを確認する起点として使用してください。

この docs は、AIOS SDK の公式 example workspace として Demo を説明します。
OS の `/usr/share/examples` に相当し、Core、Providers、Control、Wasm、GPU、
Tools layer を組み合わせる流れを、runtime behavior を Demo 側へ移さずに示します。

公式 AIOS ディストリビューション **AIKernel.Monolith** の開発も開始されています。
Monolith は 0.1.x 系の安定化後に全 SDK layer を統合する標準 reference distribution
として位置づけられます。Demo はそれらの layer を学ぶための入口です。

## リポジトリ横断整合

共有の repository boundary、0.1.1.1 local NuGet versioning、この更新ラインでの
NuGet-only / no-PyPI rule は
[AIKernel Repository Alignment v0.1.1.1](https://github.com/AIKernel-NET/AIKernel.NET/blob/main/docs/development/repository-alignment-v0.1.1.1-ja.md)
で定義します。

Demo は package family を消費する runnable example を所有します。Runtime contract、
production package ownership、shared scenario semantics は定義しません。

## 最初に読むもの

- [User Guide](user-guide/index-ja.md)
- [User Guide English](user-guide/index.md)
- [Architecture](architecture/index-ja.md)
- [Pipelines](pipelines/index-ja.md)
- [Architecture English](architecture/index.md)
- [Pipelines English](pipelines/index.md)

## どのページを読むべきか

- コマンドと期待される demo behavior を確認したい場合は User Guide を読んでください。
- repository ownership と、Demo が runtime behavior を実装しない理由を確認したい場合は
  Architecture を読んでください。
- DAG-style sample execution model を理解したい場合は Pipelines を読んでください。

## Demo の原則

- Demo code は AIKernel package の consumer であり、runtime contract を定義しません。
- deterministic replay と PDP の例は、Core と Tools の動作を理解するための教材です。
- runtime contract と deterministic core behavior は AIKernel.Core に属します。
- physical execution engine は AIKernel.Control に属します。
- standard provider と OS driver implementation は AIKernel.Providers に属します。
- browser / WebAssembly runtime behavior は AIKernel.Wasm に属します。
- CLI、replay、inspector、tooling は AIKernel.Tools に属します。
