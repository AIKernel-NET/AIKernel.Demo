from __future__ import annotations

from aikernel_demo_python.result import Result

from .ast import DslDocument, PipelineRootNode, StepNode


class DslParserDemo:
    allowed_steps: set[str] = {"normalize", "structure", "provider", "polish"}

    @staticmethod
    def parse(text: str) -> Result[DslDocument]:
        steps: list[StepNode] = []
        for raw_line in text.splitlines():
            name = raw_line.strip()
            if not name or name.startswith("#"):
                continue
            if name not in DslParserDemo.allowed_steps:
                return Result.fail(f"Unknown step: {name}")
            steps.append(StepNode(name))
        return Result.ok(DslDocument(PipelineRootNode(tuple(steps))))
