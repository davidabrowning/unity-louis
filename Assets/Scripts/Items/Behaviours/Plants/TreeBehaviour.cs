using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class TreeBehaviour : ItemInteractableBehaviour
    {
        public ItemData TwigData;
        public const int MaxTwigDelay = 100;
        protected override void Start()
        {
            base.Start();
            StartCoroutine(DropTwigs());
        }

        private IEnumerator DropTwigs()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(1, MaxTwigDelay));
                ItemBuilderScript.Instance.TryBuildItemWithinRange(ItemInstance.BottomLeft - Vector2Int.one, ItemInstance.TopRight + Vector2Int.one, TwigData, out ItemInstance builtTwig);
            }
        }

        public void DropMaxTwigs()
        {
            for (int i = 0; i < 5; i++)
                ItemBuilderScript.Instance.TryBuildItemWithinRange(ItemInstance.BottomLeft - Vector2Int.one, ItemInstance.TopRight + Vector2Int.one, TwigData, out ItemInstance builtTwig);
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "shake_tree", "Shake down some twigs"));
            Actions.Add(new ObjectAction(this, "cut_tree", "Cut down tree"));
        }

        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "shake_tree":
                    DropMaxTwigs();
                    break;
                case "cut_tree":
                    PlayerScript.Instance.AddToInventory(ItemType.Twig, 5);
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
            List<ResourceAmount> harvestedTwigs = new List<ResourceAmount>(){
                new ResourceAmount(ItemType.Twig, 1)
            };
            return harvestedTwigs;
        }
    }
}
