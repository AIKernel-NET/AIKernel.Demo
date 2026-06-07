from __future__ import annotations

from aikernel_demo_python.result import Result


class DemoPipelineMonad:
    @staticmethod
    async def run_async(input_text: str | None) -> Result[str]:
        return (
            await DemoPipelineMonad._normalize_async(input_text)
        ).bind(DemoPipelineMonad._compile).bind(DemoPipelineMonad._execute)

    @staticmethod
    async def _normalize_async(input_text: str | None) -> Result[str]:
        if input_text is None:
            return Result.fail("Pipeline input is required.")
        return Result.ok(input_text.strip())

    @staticmethod
    def _compile(normalized: str) -> Result[str]:
        return Result.ok(f"compiled({normalized})")

    @staticmethod
    def _execute(compiled: str) -> Result[str]:
        return Result.ok(f"executed({compiled})")
