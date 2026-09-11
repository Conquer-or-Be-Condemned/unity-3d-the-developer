"""Regression tests, including the empty TextMesh Pro folder checkout case."""

from pathlib import Path
import tempfile
import unittest

from validate_meta import validate


class MetaValidationTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.assets = Path(self.temp.name) / "Assets"
        self.assets.mkdir()

    def write(self, name, content="fileFormatVersion: 2\nguid: test\n"):
        path = self.assets / name
        path.parent.mkdir(parents=True, exist_ok=True)
        path.write_text(content, encoding="utf-8")

    def errors(self):
        return validate(self.assets)[0]

    def test_asset_pair(self):
        self.write("PlayerBullet.cs", "class PlayerBullet {}")
        self.write("PlayerBullet.cs.meta")
        self.assertEqual(self.errors(), [])

    def test_missing_script_meta_fails(self):
        self.write("PlayerBullet.cs", "class PlayerBullet {}")
        self.assertEqual(len(self.errors()), 1)

    def test_orphan_file_meta_fails(self):
        self.write("PlayerBullet.cs.meta")
        self.assertEqual(len(self.errors()), 1)

    def test_empty_folder_after_git_checkout_passes(self):
        self.write("TextMesh Pro.meta", "folderAsset: yes\n")
        self.write("TextMesh Pro/Documentation 2.meta", "folderAsset: yes\r\n")
        self.assertEqual(self.errors(), [])
        self.assertEqual(len(validate(self.assets)[1]), 1)

    def test_missing_parent_folder_meta_fails(self):
        self.write("TextMesh Pro/Documentation 2.meta", "folderAsset: yes\n")
        self.assertEqual(len(self.errors()), 1)

    def test_folder_meta_cannot_hide_missing_file_meta(self):
        self.write("Folder.meta", "folderAsset: yes\n")
        self.write("Folder/PlayerBullet.cs", "class PlayerBullet {}")
        self.assertEqual(len(self.errors()), 1)

    def test_folder_marker_on_file_fails(self):
        self.write("PlayerBullet.cs", "class PlayerBullet {}")
        self.write("PlayerBullet.cs.meta", "folderAsset: yes\n")
        self.assertEqual(len(self.errors()), 1)

    def test_directory_requires_folder_marker(self):
        (self.assets / "Folder").mkdir()
        self.write("Folder.meta")
        self.assertEqual(len(self.errors()), 1)

    def test_missing_assets_fails(self):
        self.assertEqual(len(validate(self.assets / "absent")[0]), 1)


if __name__ == "__main__":
    unittest.main()
