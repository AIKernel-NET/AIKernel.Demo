# Educational Comment Guidelines

AIKernel.Demo is not published as a package. It is a teaching repository, so its
public C# members should explain intent, boundaries, and usage more fully than
runtime package code.

## C# XML Documentation

Use standard .NET XML documentation comments directly in source files:

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

Do not use external XML include files for Demo comments. Demo readers should be
able to open one source file and understand the lesson.

## Required Shape

- Every public class, record, struct, interface, enum, property, constructor,
  method, and delegate must have XML documentation.
- Each public member must include both `[EN]` and `[JA]` text.
- Use `<param>` to explain the role of each input in the teaching flow.
- Use `<returns>` to explain what the caller can learn from the result.
- Use `<example>` when a member is a primary entry point or a reusable demo
  pattern.

## Style

- Prefer concrete teaching language over generic descriptions.
- State when a type is a mock, simplified carrier, or deterministic teaching
  substitute.
- Do not describe behavior that the code does not implement.
- Keep package boundary terms explicit: Contracts, PDP, Execution, Kernel,
  Pipelines, Replay, Routing, VFS, Providers, Control, Wasm, Tools, and Cuda.

## Placeholder Cleanup

Replace template text such as:

- `EN:  JA:`
- `パラメーターです`
- `結果を返します`
- `public demo surface used as readable reference code`

with meaningful bilingual explanations before treating a file as complete.
