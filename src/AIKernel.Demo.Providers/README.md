# AIKernel.Demo.Providers

Shows official external provider packages through dry-run initialization and descriptors. No network call is made.

Run:

```powershell
dotnet run --project src/AIKernel.Demo.Providers/AIKernel.Demo.Providers.csproj -c Release
```

Expected log shape:

```text
AIKernel.Demo.Providers
chat-history.provider=chat-history
chat-history.capability=chat-history
openai.provider=providers.openai
local-llm.provider=providers.local-llm
compiler.capability=providers.dynamic-pipeline
cuda.capability=providers.cuda
provider.map=ChatHistoryRecord,...
```
