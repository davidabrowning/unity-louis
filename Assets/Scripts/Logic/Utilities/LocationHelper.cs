using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    public static class LocationHelper
    {
        public static Vector2Int GetBottomLeft(List<Vector2Int> locations)
        {
            int minX = locations.Select(loc => loc.x).Min();
            int minY = locations.Select(loc => loc.y).Min();
            return new Vector2Int(minX, minY);
        }
        public static Vector2Int GetTopRight(List<Vector2Int> locations)
        {
            int maxX = locations.Select(loc => loc.x).Max();
            int maxY = locations.Select(loc => loc.y).Max();
            return new Vector2Int(maxX, maxY);
        }
        public static List<Vector2Int> GetLocationListFromAnchorAndSize(Vector2Int anchor, Vector2Int size)
        {
            List<Vector2Int> targetTiles = new();
            for (int x = 0; x < size.x; x++)
                for (int y = 0; y < size.y; y++)
                    targetTiles.Add(new Vector2Int(anchor.x + x, anchor.y + y));
            return targetTiles;
        }
        public static Vector2Int GetRandomItemAnchorLocationWithinRange(Vector2Int itemSize, Vector2 rangeBottomLeft, Vector2 rangeTopRight)
        {
            int randomX = Mathf.RoundToInt(Random.Range(rangeBottomLeft.x, rangeTopRight.x - (itemSize.x - 1)));
            int randomY = Mathf.RoundToInt(Random.Range(rangeBottomLeft.y, rangeTopRight.y - (itemSize.y - 1)));
            return new Vector2Int(randomX, randomY);
        }
        public static bool TryGetOpenTilesWithinRange(Vector2Int itemSize, Vector2 rangeBottomLeft, Vector2 rangeTopRight, out List<Vector2Int> openTiles)
        {
            openTiles = null;
            for (int attempts = 0; attempts < 100; attempts++)
            {
                Vector2Int anchor = LocationHelper.GetRandomItemAnchorLocationWithinRange(itemSize, rangeBottomLeft, rangeTopRight);
                List<Vector2Int> targetTiles = LocationHelper.GetLocationListFromAnchorAndSize(anchor, itemSize);
                if (!GridManagerScript.Instance.IsOccupied(targetTiles))
                {
                    openTiles = targetTiles;
                    return true;
                }
            }
            return false;
        }
    }
}
