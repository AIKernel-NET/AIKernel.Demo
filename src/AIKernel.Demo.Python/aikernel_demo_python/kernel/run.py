from __future__ import annotations

from dataclasses import dataclass, field

from aikernel_demo_python.replay import DemoReplayLog, DemoSemanticDelta


@dataclass(frozen=True)
class DemoKernelRun:
    semantic_deltas: tuple[DemoSemanticDelta, ...]
    replay_log: DemoReplayLog
    provider_id: str = ""
    model_id: str = ""
    route_reason: str = ""
    provider_outputs: tuple[str, ...] = field(default_factory=tuple)

    @property
    def final_hash(self) -> str:
        return self.replay_log.final_hash
