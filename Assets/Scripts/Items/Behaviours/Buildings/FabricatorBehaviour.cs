using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class FabricatorBehaviour : BuildingBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            BuildMenuManagerScript.Instance.ShowLabInventorySlot();
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "craft_berry_basket", "Craft berry basket " + ResourceAmount.ListOut(CostCalculator.GetItemCosts(ItemType.Basket))));
            Actions.Add(new ObjectAction(this, "craft_pickaxe", "Craft pickaxe " + ResourceAmount.ListOut(CostCalculator.GetItemCosts(ItemType.Pickaxe))));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "craft_berry_basket":
                    StartCoroutine(TryCraftBerryBasket());
                    break;
                case "craft_pickaxe":
                    StartCoroutine(TryCraftPickaxe());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryCraftBerryBasket()
        {
            if (PlayerScript.Instance.HasInInventory(CostCalculator.GetItemCosts(ItemType.Basket)))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.GetItemCosts(ItemType.Basket));
                GetComponent<AudioSource>().Play();
                StartWorkingAnimation();
                yield return new WaitForSeconds(5);
                StartIdleAnimation();
                GetComponent<AudioSource>().Stop();
                PlayerScript.Instance.PlayShortSuccessSound();
                yield return new WaitForSeconds(0.5f);
                PlayerScript.Instance.SetHasBasket(true);
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have enough resources.");
            }
        }

        private IEnumerator TryCraftPickaxe()
        {
            if (PlayerScript.Instance.HasInInventory(CostCalculator.GetItemCosts(ItemType.Pickaxe)))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.GetItemCosts(ItemType.Pickaxe));
                StartWorkingAnimation();
                yield return new WaitForSeconds(5);
                StartIdleAnimation();
                yield return new WaitForSeconds(1);
                PlayerScript.Instance.SetHasPickaxe(true);
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have enough resources.");
            }
        }
    }
}
