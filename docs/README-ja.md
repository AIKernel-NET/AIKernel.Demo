# AIKernel.Demo ドキュメント

AIKernel.Demo は、AIKernel package family の実行可能な sample workspace です。
どの demo を実行するか、ローカルでどう確認するか、背後の runtime 責務が
どの repository に属するかを確認する起点として使用してください。

## 最初に読むもの

- [User Guide](user-guide/index-ja.md)
- [Architecture](architecture/index-ja.md)
- [Pipelines](pipelines/index-ja.md)
- [Architecture English](architecture/index.md)
- [Pipelines English](pipelines/index.md)

## Demo の原則

- Demo code は AIKernel package の consumer であり、runtime contract を定義しません。
- deterministic replay と PDP の例は、Core と Tools の動作を理解するための教材です。
- runtime execution engine は Control、Providers、Wasm repository に属します。
