from __future__ import annotations

from dataclasses import dataclass


@dataclass(frozen=True)
class StepNode:
    name: str


@dataclass(frozen=True)
class PipelineRootNode:
    steps: tuple[StepNode, ...]


@dataclass(frozen=True)
class DslDocument:
    root: PipelineRootNode
