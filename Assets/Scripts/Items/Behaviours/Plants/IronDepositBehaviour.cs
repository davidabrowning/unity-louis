using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class IronDepositBehaviour : ItemInteractableBehaviour, IAutoHarvestable
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "mine_iron", "Mine iron (requires pickaxe)"));
            Actions.Add(new ObjectAction(this, "kick_iron", "Kick"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "mine_iron":
                    if (PlayerScript.Instance.HasPickaxe)
                    {
                        PlayerScript.Instance.PlayMiningSound();
                        PlayerScript.Instance.AddToInventory(ItemType.Iron, Random.Range(10, 20));
                    }
                    else
                        DialogueManagerScript.Instance.ShowDialogue("We need a pickaxe to do that.");
                    break;
                case "kick_iron":
                    PlayerScript.Instance.PlayOuchSound();
                    DialogueManagerScript.Instance.ShowDialogue("Ouch!");
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }

        public List<ResourceAmount> AutoHarvest()
        {
            List<ResourceAmount> harvestedIron = new List<ResourceAmount>(){
                new ResourceAmount(ItemType.Iron, 1)
            };
            return harvestedIron;
        }
    }
}