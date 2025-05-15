using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class SeedSplicerBehaviour : BuildingBehaviour
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "splice_seeds", "Splice seeds " + ResourceAmount.ListOut(CostCalculator.GetItemCosts(ItemType.SplicedSeeds))));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "splice_seeds":
                    StartCoroutine(TrySpliceSeeds());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TrySpliceSeeds()
        {
            if (!PlayerScript.Instance.ElectricityIsOn)
            {
                DialogueManagerScript.Instance.ShowDialogue("The seed splicer requires electricity!");
                yield break;
            }
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.GetItemCosts(ItemType.SplicedSeeds)))
            {
                DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                yield break;
            }
            PlayerScript.Instance.RemoveFromInventory(CostCalculator.GetItemCosts(ItemType.SplicedSeeds));
            GetComponent<AudioSource>().Play();
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            PlayerScript.Instance.AddToInventory(new ResourceAmount(ItemType.Seed, 1));
        }
    }
}
