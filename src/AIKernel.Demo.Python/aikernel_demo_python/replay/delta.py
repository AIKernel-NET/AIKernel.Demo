from __future__ import annotations

from dataclasses import dataclass

from aikernel_demo_python.semantics import DemoSemanticSlot


@dataclass(frozen=True)
class DemoSemanticDelta:
    step_name: str
    before: str
    after: str
    slot: DemoSemanticSlot = DemoSemanticSlot.ORCHESTRATION
