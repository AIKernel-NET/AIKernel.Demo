from __future__ import annotations

from dataclasses import dataclass
from typing import Callable, Generic, TypeVar

T = TypeVar("T")
U = TypeVar("U")


@dataclass(frozen=True)
class Result(Generic[T]):
    value: T | None = None
    error: str | None = None

    @property
    def is_success_state(self) -> bool:
        return self.error is None

    @property
    def is_failure_state(self) -> bool:
        return self.error is not None

    def map(self, mapper: Callable[[T], U]) -> Result[U]:
        if self.is_failure_state:
            return Result.fail(self.error or "Unknown failure.")
        try:
            return Result.ok(mapper(self.value))  # type: ignore[arg-type]
        except Exception as exc:  # fail-closed demo boundary
            return Result.fail(str(exc))

    def bind(self, binder: Callable[[T], Result[U]]) -> Result[U]:
        if self.is_failure_state:
            return Result.fail(self.error or "Unknown failure.")
        try:
            return binder(self.value)  # type: ignore[arg-type]
        except Exception as exc:
            return Result.fail(str(exc))

    @staticmethod
    def ok(value: T) -> Result[T]:
        return Result(value=value)

    @staticmethod
    def fail(error: str) -> Result[T]:
        return Result(error=error)
