using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class ARMBehaviour : BuildingBehaviour
    {
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "research_cure", "Research a cure for X87R-56 " + ResourceAmount.ListOut(CostCalculator.ARMCure())));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "research_cure":
                    StartCoroutine(TryResearchCure());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryResearchCure()
        {
            if (!PlayerScript.Instance.ElectricityIsOn)
            {
                DialogueManagerScript.Instance.ShowDialogue("The ARM requires electricity!");
                yield break;
            }
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.ARMCure()))
            {
                DialogueManagerScript.Instance.ShowDialogue("We are missing some resources for that.");
                yield break;
            }
            GetComponent<AudioSource>().Play();
            PlayerScript.Instance.RemoveFromInventory(CostCalculator.ARMCure());
            StartWorkingAnimation();
            yield return new WaitForSeconds(5);
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
            EraManagerScript.Instance.AdvanceEra();
        }
    }
}
