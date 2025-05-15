using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FarmerDemo
{
    public class ItemBuilderScript : MonoBehaviourSingletonBase<ItemBuilderScript>
    {
        private ItemData _itemData;
        public Transform ParentFolder;

        public bool TryBuildItemWithinRange(Vector2Int rangeBottomleft, Vector2Int rangeTopRight, ItemData itemData, out ItemInstance itemInstance)
        {
            itemInstance = null;

            _itemData = itemData;
            if (!LocationHelper.TryGetOpenTilesWithinRange(itemData.Size, rangeBottomleft, rangeTopRight, out List<Vector2Int> openTiles))
                return false;

            _itemData.InstantiateObject(openTiles, ParentFolder);
            return true;
        }

        public bool TryBuildItemWithinRange(List<Vector2Int> locations, ItemData itemData, out ItemInstance itemInstance)
        {
            Vector2Int rangeBottomLeft = LocationHelper.GetBottomLeft(locations);
            Vector2Int rangeTopRight = LocationHelper.GetTopRight(locations);
            return TryBuildItemWithinRange(rangeBottomLeft, rangeTopRight, itemData, out itemInstance);
        }
    }
}
