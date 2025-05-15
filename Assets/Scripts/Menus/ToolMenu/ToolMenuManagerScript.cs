using TMPro;
using UnityEngine;

namespace FarmerDemo
{
    public class ToolMenuManagerScript : MonoBehaviourSingletonBase<ToolMenuManagerScript>
    {
        public GameObject BasketInventorySlot;
        public GameObject PickaxeInventorySlot;
        private void Update()
        {
            ShowToolsInPlayerInventory();
        }

        private void ShowToolsInPlayerInventory()
        {
            BasketInventorySlot.SetActive(PlayerScript.Instance.HasBasket);
            PickaxeInventorySlot.SetActive(PlayerScript.Instance.HasPickaxe);
        }
    }
}