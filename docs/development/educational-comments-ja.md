# 教材向けコメント規約

AIKernel.Demo は package として公開しません。教材リポジトリであるため、
public な C# メンバーは runtime package のコードよりも詳しく、意図、境界、
使い方を説明します。

## C# XML ドキュメント

コメントは通常の .NET XML documentation comment をソース内に直接記述します。

```csharp
/// <summary>
/// [EN] Explains what this demo member teaches and which AIKernel boundary it
/// illustrates.
/// [JA] この Demo メンバーが何を教材として示し、AIKernel のどの境界を説明するかを記述します。
/// </summary>
/// <remarks>
/// [EN] Describe the learning context, especially when the code is intentionally
/// deterministic, simplified, or mock-based.
/// [JA] とくに決定論的、簡略化、または mock ベースである場合は、その学習上の文脈を説明します。
/// </remarks>
```

Demo では外部 XML include 方式を使いません。読者が 1 つのソースファイルを
開くだけで、そのコードの教材意図を理解できることを優先します。

## 必須形式

- public な class、record、struct、interface、enum、property、constructor、
  method、delegate には XML コメントを付与します。
- すべての public member に `[EN]` と `[JA]` の説明を含めます。
- `<param>` では入力が教材フローの中で果たす役割を説明します。
- `<returns>` では戻り値から呼び出し側が何を学べるかを説明します。
- 主要な entry point や再利用可能な demo pattern には `<example>` を付けます。

## 文体

- 汎用的な説明よりも、何を学べるかが分かる具体的な説明を優先します。
- mock、簡略化 carrier、決定論的な教材用代替である場合は明記します。
- 実装していない振る舞いを書かないでください。
- Contracts、PDP、Execution、Kernel、Pipelines、Replay、Routing、VFS、
  Providers、Control、Wasm、Tools、Cuda などの package boundary を明示します。

## 仮テンプレートの除去

以下のような仮テンプレートは、完了扱いにする前に意味のあるバイリンガル説明へ
置き換えます。

- `EN:  JA:`
- `パラメーターです`
- `結果を返します`
- `public demo surface used as readable reference code`
