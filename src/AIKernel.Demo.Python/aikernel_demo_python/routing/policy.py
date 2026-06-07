from __future__ import annotations

from dataclasses import dataclass
from typing import Iterable


@dataclass(frozen=True)
class DemoRoutingDecision:
    provider_id: str
    model_id: str
    route_reason: str


class DemoRoutingPolicy:
    LOCAL_THRESHOLD = 32

    @staticmethod
    def route(input_size: int, provider_tiers: Iterable[str]) -> DemoRoutingDecision:
        tiers = set(provider_tiers)
        if input_size <= DemoRoutingPolicy.LOCAL_THRESHOLD and "local" in tiers:
            return DemoRoutingDecision("demo.local", "local-fixed", "local-threshold")
        if input_size > DemoRoutingPolicy.LOCAL_THRESHOLD and "remote" in tiers:
            return DemoRoutingDecision("demo.remote", "remote-fixed", "remote-threshold")
        return DemoRoutingDecision("demo.mock", "mock-fixed", "no-matching-provider-tier")
