from __future__ import annotations

from aikernel_demo_python.execution import DemoExecutionGraph
from aikernel_demo_python.result import Result

from .ast import DslDocument


class DslToGraphDemo:
    @staticmethod
    def convert(document: DslDocument) -> Result[DemoExecutionGraph]:
        if document.root is None:
            return Result.fail("DSL root must be a pipeline.")
        return Result.ok(DemoExecutionGraph.from_steps(step.name for step in document.root.steps))
