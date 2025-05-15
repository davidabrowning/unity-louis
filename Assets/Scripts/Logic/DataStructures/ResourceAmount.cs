
using System.Collections.Generic;

namespace FarmerDemo
{
    [System.Serializable]
    public class ResourceAmount
    {
        public ItemType ItemType;
        public int Amount;
        public ResourceAmount(ItemType itemType, int itemAmount)
        {
            ItemType = itemType;
            Amount = itemAmount;
        }
        public override string ToString()
        {
            return $"[{ItemType}: {Amount}]";
        }
        public static string ListOut(List<ResourceAmount> resourceAmountList)
        {
            string result = "";
            foreach (ResourceAmount resourceAmount in resourceAmountList)
                result += resourceAmount.ToString() ;
            return result;
        }
    }
}