using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class ItemInstance
    {
        public ItemData ItemData;
        public List<Vector2Int> OccupiedTiles;
        public int ClockwiseRotationDegrees;
        public Vector2Int BottomLeft { get { return LocationHelper.GetBottomLeft(OccupiedTiles); } }
        public Vector2Int TopRight { get { return LocationHelper.GetTopRight(OccupiedTiles); } }
        
        public ItemInstance(ItemData itemData, List<Vector2Int> occupiedTiles) { 
            ItemData = itemData;
            OccupiedTiles = occupiedTiles;
        }
    }
}