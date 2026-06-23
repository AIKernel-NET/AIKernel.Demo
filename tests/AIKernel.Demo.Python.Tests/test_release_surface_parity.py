"""
AIKernel Demo Python Release Surface Parity Tests

[EN]
Purpose:
    Ensures the Python demo package has a visible counterpart for each
    AIKernel.Demo 0.1.3 C# golden-path demo, including the direct GPU rev3
    console teaching path.

[JA]
目的:
    AIKernel.Demo 0.1.3 の C# golden-path demo それぞれに対して、直接 GPU
    rev3 console 教材面を含め Python demo package 側にも見える対応面があることを
    確認します。
"""

from __future__ import annotations

from aikernel_demo_python import DemoSurfaceResult, run_all_release_surfaces
from aikernel_demo_python.release_surfaces import run_cuda_demo


def test_python_release_surfaces_match_csharp_demo_map() -> None:
    surfaces = run_all_release_surfaces()
    assert all(isinstance(surface, DemoSurfaceResult) for surface in surfaces)
    assert [surface.csharp_project for surface in surfaces] == [
        "AIKernel.Demo.CoreRuntime",
        "AIKernel.Demo.Contracts",
        "AIKernel.Demo.Control",
        "AIKernel.Demo.Providers",
        "AIKernel.Demo.StandardProviders",
        "AIKernel.Demo.Tools",
        "AIKernel.Demo.Wasm",
        "AIKernel.Demo.Gpu",
        "AIKernel.Demo.Cuda",
    ]


def test_python_release_surfaces_are_deterministic_and_dry_run() -> None:
    first = run_all_release_surfaces()
    second = run_all_release_surfaces()
    assert first == second
    assert all(surface.lines[0] == surface.csharp_project for surface in first)
    assert all("http" not in "\n".join(surface.lines).lower() for surface in first)


def test_cuda_surface_keeps_non_windows_skip_and_windows_dry_run_paths() -> None:
    skipped = run_cuda_demo()
    windows = run_cuda_demo(is_windows=True)
    assert "status=skipped" in skipped.lines
    assert "reason=CUDA package is Windows native" in skipped.lines
    assert "status=dry-run" in windows.lines
    assert "request.valid=true" in windows.lines


def test_wasm_surface_exposes_gpu_rev3_pass_vocabulary() -> None:
    wasm = next(surface for surface in run_all_release_surfaces() if surface.name == "Wasm")
    assert "gpu.rev3=true" in wasm.lines
    assert "gpu.passes=gpu.aisthesis.raw-frame,gpu.spatial-reasoning,gpu.hud.composite" in wasm.lines
    assert "gpu.raw=raw-framebuffer" in wasm.lines
    assert "gpu.hud=offscreen-composite" in wasm.lines
    assert "gpu.matrix=topos,route,threat,zoe" in wasm.lines


def test_gpu_surface_is_direct_rev3_console_demo() -> None:
    gpu = next(surface for surface in run_all_release_surfaces() if surface.name == "Gpu")
    assert gpu.csharp_project == "AIKernel.Demo.Gpu"
    assert "passes=gpu.aisthesis.raw-frame,gpu.spatial-reasoning,gpu.hud.composite" in gpu.lines
    assert "rawAisthesisOnly=True" in gpu.lines
    assert "hudCompositeOffscreen=True" in gpu.lines
    assert "passBridge=optional-native-or-js" in gpu.lines
