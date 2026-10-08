"""Crop, redact and shrink raw Datadog screenshots into assets/screenshots/.

The course repo is mirrored publicly, so raw captures never live in it. Capture
full-resolution screenshots somewhere outside the repo, then run:

    python scripts/sanitize_screenshots.py --raw-dir <folder-outside-the-repo>

Each entry in screenshots.json names a raw file, a crop box and optional redact
boxes, all in raw-image pixels. Redaction is a solid fill (blur can sometimes be
reversed on small text). Output PNGs are re-encoded from pixels only, so no
metadata from the capture survives.

Needs Pillow (pip install pillow), ideally in a throwaway virtualenv.
"""

import argparse
import json
import sys
from pathlib import Path

from PIL import Image, ImageDraw

COURSE_DIR = Path(__file__).resolve().parent.parent
REPO_DIR = COURSE_DIR.parent.parent
MANIFEST = Path(__file__).resolve().parent / "screenshots.json"
OUT_DIR = COURSE_DIR / "assets" / "screenshots"
FILL = (40, 44, 52)


def sanitize(raw_path: Path, shot: dict, max_width: int) -> Image.Image:
    im = Image.open(raw_path).convert("RGB")
    draw = ImageDraw.Draw(im)
    for box in shot.get("redact", []):
        draw.rectangle(box, fill=FILL)
    im = im.crop(tuple(shot["crop"]))
    if im.width > max_width:
        im = im.resize((max_width, round(im.height * max_width / im.width)), Image.LANCZOS)
    # Rebuild from raw pixels so no ancillary PNG chunks carry over.
    return Image.frombytes("RGB", im.size, im.tobytes())


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    parser.add_argument("--raw-dir", required=True, type=Path, help="folder holding the raw captures (outside the repo)")
    parser.add_argument("--only", nargs="*", help="output names to process (default: all)")
    args = parser.parse_args()

    raw_dir = args.raw_dir.resolve()
    if raw_dir == REPO_DIR or REPO_DIR in raw_dir.parents:
        print(f"refusing: --raw-dir {raw_dir} is inside the repo; keep raw captures outside it", file=sys.stderr)
        return 2

    manifest = json.loads(MANIFEST.read_text(encoding="utf-8"))
    max_width = manifest.get("max_width", 1600)
    OUT_DIR.mkdir(parents=True, exist_ok=True)

    for shot in manifest["shots"]:
        if args.only and shot["out"] not in args.only:
            continue
        raw_path = raw_dir / shot["raw"]
        if not raw_path.exists():
            print(f"missing raw capture: {raw_path}", file=sys.stderr)
            return 1
        out_path = OUT_DIR / shot["out"]
        sanitize(raw_path, shot, max_width).save(out_path, optimize=True)
        print(f"{shot['out']}  <-  {shot['raw']}")
    return 0


if __name__ == "__main__":
    sys.exit(main())
