from __future__ import annotations

from dataclasses import dataclass, field
from types import MappingProxyType
from typing import Mapping

from .slot import DemoSemanticSlot


@dataclass(frozen=True)
class DemoSemanticState:
    orchestration_state: Mapping[str, str] = field(default_factory=dict)
    expression_state: Mapping[str, str] = field(default_factory=dict)
    material_state: Mapping[str, str] = field(default_factory=dict)

    @property
    def values(self) -> Mapping[str, str]:
        return self.orchestration_state

    def apply(self, delta) -> DemoSemanticState:
        orchestration = dict(self.orchestration_state)
        expression = dict(self.expression_state)
        material = dict(self.material_state)
        target = {
            DemoSemanticSlot.ORCHESTRATION: orchestration,
            DemoSemanticSlot.EXPRESSION: expression,
            DemoSemanticSlot.MATERIAL: material,
        }[delta.slot]
        target[delta.step_name] = delta.after
        return DemoSemanticState(
            MappingProxyType(dict(sorted(orchestration.items()))),
            MappingProxyType(dict(sorted(expression.items()))),
            MappingProxyType(dict(sorted(material.items()))),
        )

    @staticmethod
    def from_deltas(deltas) -> DemoSemanticState:
        state = DemoSemanticState()
        for delta in deltas:
            state = state.apply(delta)
        return state
