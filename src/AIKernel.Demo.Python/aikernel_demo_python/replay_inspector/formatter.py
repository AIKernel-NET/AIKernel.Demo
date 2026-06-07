from __future__ import annotations


class ReplaySummaryFormatter:
    @staticmethod
    def format(replay_hash: str | None, step_count: int) -> str:
        if not replay_hash:
            return "steps=0; hash=invalid"
        short_hash = replay_hash[:8] if len(replay_hash) > 8 else replay_hash
        return f"steps={step_count}; hash={short_hash}"
