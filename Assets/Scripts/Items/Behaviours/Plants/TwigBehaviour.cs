using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class TwigBehaviour : ItemBehaviour, IManualHarvestable
    {
        public List<ResourceAmount> ManualHarvest()
        {
            List<ResourceAmount> collectedResources = new();
            GridManagerScript.Instance.RemoveItem(ItemInstance);
            Destroy(gameObject);
            collectedResources.Add(new ResourceAmount(ItemType.Twig, 1));
            return collectedResources;
        }
    }
}