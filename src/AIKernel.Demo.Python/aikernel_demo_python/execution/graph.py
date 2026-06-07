from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable


@dataclass(frozen=True)
class DemoExecutionNode:
    id: str
    operator_id: str
    metadata: dict[str, str]

    @property
    def step_name(self) -> str:
        return self.id.split("-", 1)[1] if "-" in self.id else self.id


@dataclass(frozen=True)
class DemoExecutionGraph:
    graph_id: str
    nodes: tuple[DemoExecutionNode, ...]

    @staticmethod
    def from_steps(steps: Iterable[str]) -> DemoExecutionGraph:
        nodes = tuple(
            DemoExecutionNode(
                id=f"{index:02d}-{step}",
                operator_id=f"demo.operator.{step}",
                metadata={"step_name": step},
            )
            for index, step in enumerate(steps, start=1)
        )
        return DemoExecutionGraph("demo.graph.default", nodes)

    def traverse(self) -> tuple[DemoExecutionNode, ...]:
        return self.nodes
