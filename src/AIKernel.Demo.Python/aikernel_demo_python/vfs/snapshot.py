"""
AIKernel Demo Reference Module

[EN]
Purpose:
    Python reference implementation mirroring the AIKernel demo architecture.

Runtime Specifications and Prerequisites:
    This module is intentionally lightweight and deterministic. It is used as a
    learning artifact for the AIKernel 0.1.2 release line, where semantic
    structure, policy decisions, routing, execution, and replay are kept visible.

Architectural Intent:
    The Python demo mirrors the C# demo surface without copying internal runtime
    responsibilities. Each public object should be understandable as a small
    contract-facing step in the larger semantic runtime.

[JA]
目的:
    AIKernel Demo architecture を Python で対応させた参照実装です。

実行仕様と前提条件:
    この module は軽量かつ決定論的であることを意図しています。AIKernel
    0.1.2 release line の学習教材として、semantic structure、policy
    decision、routing、execution、replay の境界が見えるようにしています。

アーキテクチャ意図:
    Python Demo は C# Demo の公開サーフェスに対応しますが、内部 runtime
    責務は複製しません。各 public object は、semantic runtime 全体の中の
    小さな contract-facing step として読める必要があります。
"""

from __future__ import annotations

import hashlib
from dataclasses import dataclass, field


# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
def canonicalize_path(path: str) -> str:
    """[EN]
    Executes the canonicalize path operation.
    Args:
        path: Input value for canonicalize path.
    
    Returns:
        Result produced by the operation.
    
    [JA]
    canonicalize path 操作を実行します。
    引数:
        path: canonicalize path に渡す入力値です。
    
    戻り値:
        操作によって生成される結果です。
    """
    normalized = path.replace("\\", "/").strip()
    normalized = "/" + normalized.strip("/")
    return "/" if normalized == "/" else normalized.rstrip("/")


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoVfsFile:
    """[EN]
    Represents the DemoVfsFile public Python API surface.
    
    [JA]
    DemoVfsFile の公開 Python API サーフェスを表します。
    """
    path: str
    content: str

    def __post_init__(self) -> None:
        object.__setattr__(self, "path", canonicalize_path(self.path))


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoVfsDirectory:
    """[EN]
    Represents the DemoVfsDirectory public Python API surface.
    
    [JA]
    DemoVfsDirectory の公開 Python API サーフェスを表します。
    """
    path: str

    def __post_init__(self) -> None:
        object.__setattr__(self, "path", canonicalize_path(self.path))


@dataclass(frozen=True)
# EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
class DemoVfsSnapshot:
    """[EN]
    Represents the DemoVfsSnapshot public Python API surface.
    
    [JA]
    DemoVfsSnapshot の公開 Python API サーフェスを表します。
    """
    files: dict[str, str] = field(default_factory=dict)
    directories: frozenset[str] = frozenset()

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def add_directory(self, path: str) -> DemoVfsSnapshot:
        """[EN]
        Executes the add directory operation.
        Args:
            path: Input value for add directory.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        add directory 操作を実行します。
        引数:
            path: add directory に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return DemoVfsSnapshot(dict(self.files), self.directories | {canonicalize_path(path)})

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def add_file(self, path: str, content: str) -> DemoVfsSnapshot:
        """[EN]
        Executes the add file operation.
        Args:
            path: Input value for add file.
            content: Input value for add file.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        add file 操作を実行します。
        引数:
            path: add file に渡す入力値です。
            content: add file に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        normalized = canonicalize_path(path)
        files = dict(self.files)
        files[normalized] = content
        directories = set(self.directories)
        parent = normalized.rsplit("/", 1)[0]
        if parent:
            directories.add(parent)
        return DemoVfsSnapshot(files, frozenset(directories))

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def read_file(self, path: str) -> str:
        """[EN]
        Executes the read file operation.
        Args:
            path: Input value for read file.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        read file 操作を実行します。
        引数:
            path: read file に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        return self.files[canonicalize_path(path)]

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def list_directory(self, path: str) -> tuple[str, ...]:
        """[EN]
        Executes the list directory operation.
        Args:
            path: Input value for list directory.
        
        Returns:
            Result produced by the operation.
        
        [JA]
        list directory 操作を実行します。
        引数:
            path: list directory に渡す入力値です。
        
        戻り値:
            操作によって生成される結果です。
        """
        prefix = canonicalize_path(path)
        prefix = "" if prefix == "/" else prefix + "/"
        values = [d for d in self.directories if d.startswith(prefix)]
        values.extend(f for f in self.files if f.startswith(prefix))
        return tuple(sorted(set(values)))

    # EN: Public demo surface for readers. / JA: 読み手向けの公開 Demo サーフェスです。
    def compute_snapshot_hash(self) -> str:
        """[EN]
        Executes the compute snapshot hash operation.
        Returns:
            Result produced by the operation.
        
        [JA]
        compute snapshot hash 操作を実行します。
        戻り値:
            操作によって生成される結果です。
        """
        lines: list[str] = []
        for directory in sorted(self.directories):
            lines.append(_canonical_line("directory", directory, ""))
        for path, content in sorted(self.files.items()):
            lines.append(_canonical_line("file", path, content))
        return hashlib.sha256(("\n".join(lines) + "\n").encode("utf-8")).hexdigest()


def _canonical_line(kind: str, path: str, content: str) -> str:
    normalized = content.replace("\r\n", "\n").replace("\r", "\n").strip()
    return "|".join([f"content={normalized}", f"kind={kind}", f"path={canonicalize_path(path)}"])
