using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    [CreateAssetMenu(fileName = "BuildingData", menuName = "Scriptable Objects/BuildingData")]
    public class BuildingData : ItemData, IConstructable
    {
        public List<ResourceAmount> ConstructionCosts => CostCalculator.GetItemCosts(ItemType);
    }
}
