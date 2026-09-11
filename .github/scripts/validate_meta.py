"""Validate Unity asset pairs without rejecting Git-omitted empty folders."""

import argparse
from pathlib import Path
import re
import sys


def validate(assets):
    errors = []
    empty_folders = []
    if not assets.is_dir():
        return [(assets, "Missing Assets directory")], empty_folders

    paths = sorted(assets.rglob("*"))
    # Folder metadata preserves empty Unity folders that Git cannot store.
    # Include their ancestors so nested empty folders still need parent metadata.
    folders = {path for path in paths if path.is_dir()}
    for meta in (path for path in paths if path.is_file() and path.suffix == ".meta"):
        asset = meta.with_suffix("")
        content = meta.read_text(encoding="utf-8-sig")
        is_folder = re.search(r"^folderAsset: yes\s*$", content, re.MULTILINE)
        if is_folder:
            if asset.exists() and not asset.is_dir():
                errors.append((meta, "Folder metadata points to a file"))
            elif not asset.exists():
                empty_folders.append(asset)
            parent = asset
            while parent != assets:
                folders.add(parent)
                parent = parent.parent
        elif not asset.is_file():
            errors.append((meta, "Missing asset file or folderAsset: yes marker"))

    assets_to_check = folders | {
        path for path in paths if path.is_file() and path.suffix != ".meta"
    }
    for asset in sorted(assets_to_check):
        meta = Path(str(asset) + ".meta")
        if not meta.is_file():
            errors.append((asset, f"Missing .meta file: {meta.as_posix()}"))
    return errors, empty_folders


def escape_annotation(value):
    return str(value).replace("%", "%25").replace("\r", "%0D").replace("\n", "%0A").replace(":", "%3A").replace(",", "%2C")


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("assets", nargs="?", type=Path, default=Path("Assets"))
    args = parser.parse_args()
    errors, empty_folders = validate(args.assets)
    for path, message in errors:
        print(f"::error file={escape_annotation(path.as_posix())}::{escape_annotation(message)}")
    print(f"Unity .meta validation: {len(errors)} error(s), {len(empty_folders)} Git-omitted empty folder(s).")
    return 1 if errors else 0


if __name__ == "__main__":
    sys.exit(main())
