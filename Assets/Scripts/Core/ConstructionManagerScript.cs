using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    public class ConstructionManagerScript : MonoBehaviourSingletonBase<ConstructionManagerScript>
    {
        public bool BuildModeOn = false;
        public ItemData ItemData;
        private void Update()
        {
            if (BuildModeOn)
            {
                List<Vector2Int> targetTiles = GetTargetTiles();
                TryHighlightTargetTiles(targetTiles);
                if (UserWantsToExitBuildMode())
                {
                    ExitBuildMode();
                    return;
                }

                if (Input.GetMouseButtonDown(0)) // left-click
                {
                    TryBuildAndRemoveResources(targetTiles);
                    ExitBuildMode();
                }
            }
        }
        public void TryEnterBuildMode(ItemData itemData)
        {
            ItemData = itemData;
            if (PlayerScript.Instance.HasInInventory(ItemData.Cost))
                BuildModeOn = true;
            else
                DialogueManagerScript.Instance.ShowDialogue("Not quite enough resources to build a " + ItemData.ItemName + " yet.");
        }
        private void ExitBuildMode()
        {
            BuildModeOn = false;
            ItemData = null;
            GridHighlighterScript.Instance.Hide();
        }
        private List<Vector2Int> GetTargetTiles()
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2Int mousePos = new Vector2Int((int)Mathf.Round(mouseWorld.x), (int)Mathf.Round(mouseWorld.y));
            List<Vector2Int> targetTiles = new();
            for (int x = 0; x < ItemData.Size.x; x++)
                for (int y = 0; y < ItemData.Size.y; y++)
                    targetTiles.Add(mousePos + new Vector2Int(x, y));
            return targetTiles;
        }
        private void TryHighlightTargetTiles(List<Vector2Int> targetTiles)
        {
            if (!GridManagerScript.Instance.IsOccupied(targetTiles))
                GridHighlighterScript.Instance.Highlight(targetTiles);
            else
                GridHighlighterScript.Instance.Hide();
        }
        private bool UserWantsToExitBuildMode()
        {
            if (Input.GetMouseButtonDown(1)) // right-click
                return true;

            if (Input.GetKeyDown(KeyCode.Escape)) // escape key
                return true;

            return false;
        }
        private void TryBuildAndRemoveResources(List<Vector2Int> targetTiles)
        {
            if (!PlayerScript.Instance.HasInInventory(ItemData.Cost))
            {
                DialogueManagerScript.Instance.ShowDialogue("Not quite enough resources to build a " + ItemData.ItemName + " yet.");
                return;
            }

            if (ItemBuilderScript.Instance.TryBuildItemWithinRange(targetTiles, ItemData, out ItemInstance builtObj))
            {
                PlayerScript.Instance.RemoveFromInventory(ItemData.Cost);
                GridHighlighterScript.Instance.Hide();
            }

        }
    }
}