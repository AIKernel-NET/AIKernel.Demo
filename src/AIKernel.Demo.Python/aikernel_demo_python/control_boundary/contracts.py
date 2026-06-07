from __future__ import annotations

from dataclasses import dataclass
from typing import Protocol


class IExecutionNode(Protocol):
    id: str
    operator_id: str
    metadata: dict[str, str]


class IExecutionGraph(Protocol):
    graph_id: str
    nodes: tuple[IExecutionNode, ...]


@dataclass(frozen=True)
class ControlExecutionRequest:
    execution_id: str
    graph_id: str
    metadata: dict[str, str]


@dataclass(frozen=True)
class ControlStateSnapshot:
    execution_id: str
    node_id: str
    metadata: dict[str, str]


@dataclass(frozen=True)
class ControlExecutionResult:
    execution_id: str
    status: str
    metadata: dict[str, str]
