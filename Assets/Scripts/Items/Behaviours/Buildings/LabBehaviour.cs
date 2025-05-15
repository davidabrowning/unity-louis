using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class LabBehaviour : BuildingBehaviour
    {
        protected override void Awake()
        {
            base.Awake();
            ResearchManagerScript.Instance.ShowResearchStatusBar();
        }

        private void OnEnable()
        {
            EraManagerScript.Instance.EraUpdate += HandleEraUpdate;
        }
        private void OnDisable()
        {
            EraManagerScript.Instance.EraUpdate -= HandleEraUpdate;
        }
        private void HandleEraUpdate()
        {
            Actions.Clear();
            PopulateActions();
        }
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "research", "Study a sample " + CostCalculator.StandardResearch()));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "research":
                    StartCoroutine(TryPerformResearch());
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator TryPerformResearch()
        {
            if (EraManagerScript.Instance.CurrentEra == EraType.ScientificAdvancement)
                yield break;
            if (!PlayerScript.Instance.HasInInventory(CostCalculator.StandardResearch()))
            {
                DialogueManagerScript.Instance.ShowDialogue("We don't have any units of " + CostCalculator.StandardResearch().ItemType.ToString().ToLower() + ".");
                yield break;
            }
            GetComponent<AudioSource>().Play();
            while (PlayerScript.Instance.HasInInventory(CostCalculator.StandardResearch()))
            {
                PlayerScript.Instance.RemoveFromInventory(CostCalculator.StandardResearch());
                StartWorkingAnimation();
                for (int i = 0; i < 10; i++)
                {
                    yield return new WaitForSeconds(0.05f);
                    ResearchManagerScript.Instance.IncrementResearch();
                }
            }
            StartIdleAnimation();
            yield return new WaitForSeconds(1);
        }
    }
}
