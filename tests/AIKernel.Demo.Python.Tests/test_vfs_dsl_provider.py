from __future__ import annotations

import re

import pytest

from aikernel_demo_python.dsl import DslParserDemo
from aikernel_demo_python.providers.mock import MockProvider
from aikernel_demo_python.replay_inspector import ReplaySummaryFormatter
from aikernel_demo_python.vfs import DemoVfsDirectory, DemoVfsFile, DemoVfsSnapshot


def test_vfs_snapshot_reads_lists_and_hashes_deterministically() -> None:
    first = DemoVfsSnapshot().add_file("/rom/a.txt", "A\r\n").add_file("/rom/b.txt", "B")
    second = DemoVfsSnapshot().add_file("/rom/b.txt", "B").add_file("/rom/a.txt", "A\n")
    assert first.read_file(" /rom\\a.txt ") == "A\r\n"
    assert first.list_directory("/rom") == tuple(sorted(first.list_directory("/rom")))
    assert first.compute_snapshot_hash() == second.compute_snapshot_hash()


def test_vfs_path_canonicalization_and_immutability() -> None:
    file = DemoVfsFile(" /rom\\demo.txt ", "x")
    directory = DemoVfsDirectory("rom")
    snapshot = DemoVfsSnapshot()
    next_snapshot = snapshot.add_file("rom/demo.txt", "x")
    assert file.path == "/rom/demo.txt"
    assert directory.path == "/rom"
    assert snapshot.files == {}
    assert next_snapshot.read_file("/rom/demo.txt") == "x"


def test_dsl_parser_fails_closed_and_allows_injected_steps() -> None:
    original = set(DslParserDemo.allowed_steps)
    try:
        assert DslParserDemo.parse("normalize\nINVALID_STEP").is_failure_state
        DslParserDemo.allowed_steps = {"loop"}
        parsed = DslParserDemo.parse("\n# comment\nloop\n")
        assert parsed.is_success_state
        assert [step.name for step in parsed.value.root.steps] == ["loop"]  # type: ignore[union-attr]
        assert DslParserDemo.parse("LOOP").is_failure_state
    finally:
        DslParserDemo.allowed_steps = original


def test_mock_provider_returns_deterministic_uppercase_sha256() -> None:
    provider = MockProvider()
    first = provider.generate("hello-rom")
    second = provider.generate("hello-rom")
    assert first == second
    assert re.fullmatch(r"^[A-F0-9]{64}$", first.output_hash)
    with pytest.raises(ValueError):
        provider.generate(None)


def test_replay_summary_formatter_fails_closed_and_shortens() -> None:
    assert ReplaySummaryFormatter.format(None, 4) == "steps=0; hash=invalid"
    assert ReplaySummaryFormatter.format("", 4) == "steps=0; hash=invalid"
    assert ReplaySummaryFormatter.format("abcdef", 4) == "steps=4; hash=abcdef"
    assert ReplaySummaryFormatter.format("abcdef123456", 4) == "steps=4; hash=abcdef12"
