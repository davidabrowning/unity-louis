using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public class BerryBushBehaviour : ItemInteractableBehaviour, IManualHarvestable
    {
        public int BerryCount = 0;
        public float MinBerryGrowthInterval = 2f;
        public float MaxBerryGrowthInterval = 8f;
        private float MaxBerryCount = 2f;
        public Sprite EmptyBushSprite;
        public Sprite OneBerryBushSprite;
        public Sprite TwoBerriesBushSprite;
        public AudioClip BerryBushRustleSoundClip;

        protected override void Awake()
        {
            base.Awake();
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptyBushSprite;
        }

        protected override void Start()
        {
            base.Start();
            StartCoroutine(GrowBerries());
        }

        public IEnumerator GrowBerries()
        {
            while (BerryCount < MaxBerryCount)
            {
                float waitTime = Random.Range(MinBerryGrowthInterval, MaxBerryGrowthInterval);
                yield return new WaitForSeconds(waitTime);
                GrowOneBerry();
            }
        }

        private void GrowOneBerry()
        {
            BerryCount++;
            if (BerryCount == 1)
                gameObject.GetComponent<SpriteRenderer>().sprite = OneBerryBushSprite;
            if (BerryCount > 1)
                gameObject.GetComponent<SpriteRenderer>().sprite = TwoBerriesBushSprite;
        }

        public void ClearBerries()
        {
            BerryCount = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = EmptyBushSprite;
            StartCoroutine(GrowBerries());
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "collect_berries", "Collect berries"));
            Actions.Add(new ObjectAction(this, "trample_bush", "Trample"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "collect_berries":
                    PlayerScript.Instance.AddToInventory(ManualHarvest());
                    break;
                case "trample_bush":
                    PlayerScript.Instance.AddToInventory(ItemType.Twig, 1);
                    PlayerScript.Instance.AddToInventory(ManualHarvest());
                    GridManagerScript.Instance.RemoveItem(ItemInstance);
                    Destroy(gameObject);
                    break;
                default:
                    Debug.Log("Unknown action.");
                    break;
            }
        }

        public List<ResourceAmount> AutoHarvest()
        {
            List<ResourceAmount> harvestedBerries = new List<ResourceAmount>(){
                new ResourceAmount(ItemType.Berry, BerryCount)
            };
            ClearBerries();
            return harvestedBerries;
        }

        public List<ResourceAmount> ManualHarvest()
        {
            List<ResourceAmount> collectedResources = new();
            if (PlayerScript.Instance.HasBasket)
            {
                PlayerScript.Instance.AudioSource.PlayOneShot(BerryBushRustleSoundClip);
                collectedResources.Add(new ResourceAmount(ItemType.Berry, BerryCount));
                ClearBerries();
                if (PlayerScript.Instance.AmountInInventory(ItemType.Berry) > 20)
                {
                    DialogueManagerScript.Instance.ShowDialogue("My basket is getting full");
                }
            }
            else
            {
                DialogueManagerScript.Instance.ShowDialogue("We cannot collect berries without a basket :-(");
            }
            return collectedResources;
        }
    }
}
