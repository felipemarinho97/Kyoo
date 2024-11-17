import os
import xml.etree.ElementTree as ET
from typing import Optional
from logging import getLogger

logger = getLogger(__name__)

class NFOParser:
    """Utility class to parse NFO files."""

    @staticmethod
    def parse_nfo(path: str) -> Optional[dict]:
        """Parse an NFO file and return a dictionary of metadata."""
        if not os.path.exists(path):
            return None
        try:
            tree = ET.parse(path)
            root = tree.getroot()
            media_type = root.tag.lower()
            uniqueids = {
                child.attrib.get("type", "unknown"): child.text
                for child in root.findall("uniqueid")
            }
            return {"type": media_type, "uniqueids": uniqueids}
        except ET.ParseError as e:
            logger.warning("Failed to parse NFO file at %s: %s", path, str(e))
            return None
