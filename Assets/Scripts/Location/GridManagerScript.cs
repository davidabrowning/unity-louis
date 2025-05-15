using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class GridManagerScript : MonoBehaviourSingletonBase<GridManagerScript>
    {
        private List<ItemInstance> placedItems = new();
        public bool IsOccupied(Vector2Int cell)
        {
            if (PlayerScript.Instance.LocationInt() == cell)
                return true;
            if (TileBuilderScript.Instance.GetRegionType(cell) == RegionTypeEnum.Water)
                return true;
            return placedItems
                .Where(item => item != null)
                .Where(item => item.OccupiedTiles.Contains(cell))
                .Any();
        }

        public bool IsOccupied(List<Vector2Int> cells)
        {
            foreach (var cell in cells)
                if (IsOccupied(cell))
                    return true;
            return false;
        }

        public void AddItem(ItemInstance item)
        {
            placedItems.Add(item);
        }

        public void RemoveItem(ItemInstance item)
        {
            placedItems.Remove(item);
        }

        public ItemInstance GetItemAt(Vector2Int location)
        {
            return placedItems.Where(p => p.OccupiedTiles.Contains(location)).FirstOrDefault();
        }
    }
}