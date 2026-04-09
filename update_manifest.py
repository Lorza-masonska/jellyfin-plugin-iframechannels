#!/usr/bin/env python3
"""
Run after building to update manifest.json with correct MD5 checksum.
Usage: python update_manifest.py path/to/IFrameChannels.zip
"""
import sys
import json
import hashlib

def md5(path):
    h = hashlib.md5()
    with open(path, 'rb') as f:
        for chunk in iter(lambda: f.read(8192), b''):
            h.update(chunk)
    return h.hexdigest()

if len(sys.argv) < 2:
    print("Usage: python update_manifest.py IFrameChannels.zip")
    sys.exit(1)

zip_path = sys.argv[1]
checksum = md5(zip_path)
print(f"MD5: {checksum}")

with open('manifest.json', 'r') as f:
    manifest = json.load(f)

manifest[0]['versions'][0]['checksum'] = checksum

with open('manifest.json', 'w') as f:
    json.dump(manifest, f, indent=2)

print("manifest.json updated!")
