using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.TMP_Compatibility;

namespace FarmerDemo
{
    internal class AutoHarvesterBehaviour : BuildingBehaviour
    {
        protected override void Start()
        {
            base.Start();
            StartCoroutine(AutoHarvestAdjacentTiles());
        }
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator AutoHarvestAdjacentTiles()
        {
            while (true)
            {
                yield return new WaitForSeconds(3);
                if (PlayerScript.Instance.ElectricityIsOn)
                {
                    StartWorkingAnimation();
                    AutoHarvestTile(ItemInstance.BottomLeft + Vector2Int.left);
                    AutoHarvestTile(ItemInstance.BottomLeft + Vector2Int.up);
                    AutoHarvestTile(ItemInstance.BottomLeft + Vector2Int.right);
                    AutoHarvestTile(ItemInstance.BottomLeft + Vector2Int.down);
                }
                else
                {
                    StartIdleAnimation();
                }
            }
        }

        private void AutoHarvestTile(Vector2Int location)
        {
            AutoHarvestItem(location);
            AutoHarvestRegionType(location);
        }

        private void AutoHarvestItem(Vector2Int location)
        {
            ItemInstance item = GridManagerScript.Instance.GetItemAt(location);
            if (item != null && item is IAutoHarvestable)
            {
                IAutoHarvestable harvestable = (IAutoHarvestable)item;
                PlayerScript.Instance.AddToInventory(harvestable.AutoHarvest());
            }
        }

        private void AutoHarvestRegionType(Vector2Int location)
        {
            if (TileBuilderScript.Instance.GetRegionType(location) == RegionTypeEnum.Water)
            {
                PlayerScript.Instance.AddToInventory(ItemType.Fish, 1);
            }
        }
    }
}
