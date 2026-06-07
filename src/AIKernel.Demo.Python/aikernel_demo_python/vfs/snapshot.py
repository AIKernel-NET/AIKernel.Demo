from __future__ import annotations

import hashlib
from dataclasses import dataclass, field


def canonicalize_path(path: str) -> str:
    normalized = path.replace("\\", "/").strip()
    normalized = "/" + normalized.strip("/")
    return "/" if normalized == "/" else normalized.rstrip("/")


@dataclass(frozen=True)
class DemoVfsFile:
    path: str
    content: str

    def __post_init__(self) -> None:
        object.__setattr__(self, "path", canonicalize_path(self.path))


@dataclass(frozen=True)
class DemoVfsDirectory:
    path: str

    def __post_init__(self) -> None:
        object.__setattr__(self, "path", canonicalize_path(self.path))


@dataclass(frozen=True)
class DemoVfsSnapshot:
    files: dict[str, str] = field(default_factory=dict)
    directories: frozenset[str] = frozenset()

    def add_directory(self, path: str) -> DemoVfsSnapshot:
        return DemoVfsSnapshot(dict(self.files), self.directories | {canonicalize_path(path)})

    def add_file(self, path: str, content: str) -> DemoVfsSnapshot:
        normalized = canonicalize_path(path)
        files = dict(self.files)
        files[normalized] = content
        directories = set(self.directories)
        parent = normalized.rsplit("/", 1)[0]
        if parent:
            directories.add(parent)
        return DemoVfsSnapshot(files, frozenset(directories))

    def read_file(self, path: str) -> str:
        return self.files[canonicalize_path(path)]

    def list_directory(self, path: str) -> tuple[str, ...]:
        prefix = canonicalize_path(path)
        prefix = "" if prefix == "/" else prefix + "/"
        values = [d for d in self.directories if d.startswith(prefix)]
        values.extend(f for f in self.files if f.startswith(prefix))
        return tuple(sorted(set(values)))

    def compute_snapshot_hash(self) -> str:
        lines: list[str] = []
        for directory in sorted(self.directories):
            lines.append(_canonical_line("directory", directory, ""))
        for path, content in sorted(self.files.items()):
            lines.append(_canonical_line("file", path, content))
        return hashlib.sha256(("\n".join(lines) + "\n").encode("utf-8")).hexdigest()


def _canonical_line(kind: str, path: str, content: str) -> str:
    normalized = content.replace("\r\n", "\n").replace("\r", "\n").strip()
    return "|".join([f"content={normalized}", f"kind={kind}", f"path={canonicalize_path(path)}"])
