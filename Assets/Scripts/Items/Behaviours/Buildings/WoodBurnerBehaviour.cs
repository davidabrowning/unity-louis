using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class WoodBurnerBehaviour : BuildingBehaviour
    {
        public AudioClip WorkingAudioClip;
        public AudioClip PowerDownAudioClip;

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "add_wood", "Add a twig to the burner"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "add_wood":
                    if (PlayerScript.Instance.HasInInventory(new ResourceAmount(ItemType.Twig, 1)))
                    {
                        PlayerScript.Instance.RemoveFromInventory(new ResourceAmount(ItemType.Twig, 1));
                        StartCoroutine(BurnATwig());
                    }
                    else
                    {
                        DialogueManagerScript.Instance.ShowDialogue("We need to gather some twigs first.");
                    }
                    break;
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        private IEnumerator BurnATwig()
        {
            GetComponent<AudioSource>().PlayOneShot(WorkingAudioClip);
            StartWorkingAnimation();
            PlayerScript.Instance.AddActivePowerProducer(gameObject);
            yield return new WaitForSeconds(10);
            StartIdleAnimation();
            GetComponent<AudioSource>().PlayOneShot(PowerDownAudioClip);
            PlayerScript.Instance.RemoveActivePowerProducer(gameObject);
        }
    }
}
