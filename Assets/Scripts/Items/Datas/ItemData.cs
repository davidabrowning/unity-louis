using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public ItemType ItemType;
        public string ItemName;
        public string ItemDescription;
        public GameObject ItemPrefab;
        public Vector2Int Size;
        public List<ResourceAmount> Cost { get { return CostCalculator.GetItemCosts(ItemType); } }
        public virtual void InstantiateObject(List<Vector2Int> location, Transform parentGameObjectFolder)
        {
            ItemInstance itemInstance = new ItemInstance(this, location);

            GameObject obj = Instantiate(ItemPrefab, new Vector2(), Quaternion.identity);
            Vector2 visualCenter = CalculateVisualCenter(location);
            obj.transform.position = visualCenter;
            obj.transform.SetParent(parentGameObjectFolder, false);
            obj.GetComponent<ItemBehaviour>().ItemInstance = itemInstance;

            GridManagerScript.Instance.AddItem(itemInstance);
        }
        protected Vector2 CalculateVisualCenter(List<Vector2Int> tiles)
        {
            float visualX = (float)tiles.Average(t => t.x);
            float visualY = (float)tiles.Average(t => t.y);
            return new Vector2(visualX, visualY);
        }
    }
}