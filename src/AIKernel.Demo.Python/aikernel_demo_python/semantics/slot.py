from __future__ import annotations

from enum import Enum


class DemoSemanticSlot(str, Enum):
    ORCHESTRATION = "orchestration"
    EXPRESSION = "expression"
    MATERIAL = "material"
