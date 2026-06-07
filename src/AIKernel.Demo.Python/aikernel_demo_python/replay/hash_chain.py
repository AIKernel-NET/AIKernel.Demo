from __future__ import annotations

import hashlib


class DemoHashChain:
    GENESIS_HASH = "GENESIS"

    @staticmethod
    def compute_entry_hash(entry_payload: str) -> str:
        return DemoHashChain._sha256(entry_payload)

    @staticmethod
    def compute_next(previous_hash: str, entry_hash: str) -> str:
        return DemoHashChain._sha256(previous_hash + entry_hash)

    @staticmethod
    def canonicalize_payload(step_name: str, delta_summary: str, timestamp: str) -> str:
        return "\n".join(
            [
                f"delta_summary={DemoHashChain._normalize(delta_summary)}",
                f"step_name={DemoHashChain._normalize(step_name)}",
                f"timestamp={DemoHashChain._normalize(timestamp)}",
            ]
        )

    @staticmethod
    def _sha256(value: str) -> str:
        return hashlib.sha256(value.encode("utf-8")).hexdigest()

    @staticmethod
    def _normalize(value: str) -> str:
        return value.replace("\r\n", "\n").replace("\r", "\n").strip()
