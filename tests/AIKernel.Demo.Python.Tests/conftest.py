from __future__ import annotations

import sys
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
PACKAGE_ROOT = ROOT / "src" / "AIKernel.Demo.Python"
sys.path.insert(0, str(PACKAGE_ROOT))
