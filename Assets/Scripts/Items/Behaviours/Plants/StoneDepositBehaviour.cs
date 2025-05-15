using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class StoneDepositBehaviour : ItemInteractableBehaviour, IAutoHarvestable
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "mine_stone", "Mine stone (requires pickaxe)"));
            Actions.Add(new ObjectAction(this, "search_stone", "Search for loose stones"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "mine_stone":
                    if (PlayerScript.Instance.HasPickaxe)
                        PlayerScript.Instance.AddToInventory(ItemType.Stone, Random.Range(20, 50));
                    else
                        DialogueManagerScript.Instance.ShowDialogue("We need a pickaxe to do that.");
                    break;
                case "search_stone":
                    PlayerScript.Instance.AddToInventory(ItemType.Stone, Random.Range(1, 4));
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }

        public List<ResourceAmount> AutoHarvest()
        {
            List<ResourceAmount> harvestedStone = new List<ResourceAmount>(){
                new ResourceAmount(ItemType.Stone, 1)
            };
            return harvestedStone;
        }
    }
}