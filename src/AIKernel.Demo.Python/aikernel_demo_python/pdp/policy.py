from __future__ import annotations

from dataclasses import dataclass
from enum import Enum
from typing import Iterable


class DemoPolicyCode(str, Enum):
    ALLOW = "Allow"
    DENY = "Deny"


@dataclass(frozen=True)
class DemoPolicyDecision:
    allowed: bool
    code: DemoPolicyCode
    reason: str

    @staticmethod
    def allow(reason: str | None) -> DemoPolicyDecision:
        return DemoPolicyDecision(True, DemoPolicyCode.ALLOW, reason or "")

    @staticmethod
    def deny(reason: str | None) -> DemoPolicyDecision:
        return DemoPolicyDecision(False, DemoPolicyCode.DENY, reason or "")


@dataclass(frozen=True)
class DemoPolicyRule:
    max_length: int
    reason: str

    def evaluate(self, text: str) -> DemoPolicyDecision:
        return (
            DemoPolicyDecision.allow("allowed")
            if len(text) <= self.max_length
            else DemoPolicyDecision.deny(self.reason)
        )


class DemoPolicyEngine:
    def __init__(self, rules: Iterable[DemoPolicyRule] | None = None) -> None:
        self._rules = tuple(rules or (DemoPolicyRule(32, "input-too-large"),))

    def evaluate(self, text: str) -> DemoPolicyDecision:
        for rule in self._rules:
            decision = rule.evaluate(text)
            if not decision.allowed:
                return decision
        return DemoPolicyDecision.allow("allowed")
