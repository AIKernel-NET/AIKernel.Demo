from __future__ import annotations

import hashlib
from dataclasses import dataclass


@dataclass(frozen=True)
class MockProviderResponse:
    output_hash: str


class MockProvider:
    def generate(self, input_text: str | None) -> MockProviderResponse:
        if input_text is None:
            raise ValueError("input_text is required.")
        digest = hashlib.sha256(input_text.encode("utf-8")).hexdigest().upper()
        return MockProviderResponse(digest)
