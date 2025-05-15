using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class BuildMenuIconScript : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public IconDictionary IconDictionary;
        public ItemData BuildingData;
        public GameObject BuildGridPanel;
        public GameObject BuildingInfoPanel;
        public GameObject BuildCommandIcon;
        public GameObject BuildingDescription;
        public GameObject BuildingCostIconContainer;
        public GameObject BuildingCostIconPrefab;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PlayerScript.Instance.HasInInventory(CostCalculator.GetItemCosts(BuildingData.ItemType)))
                ConstructionManagerScript.Instance.TryEnterBuildMode(BuildingData);

            BuildCommandIcon.GetComponent<Image>().sprite = IconDictionary.GetIconSprite(BuildingData.ItemType);
            BuildCommandIcon.GetComponent<Button>().onClick.RemoveAllListeners();
            BuildCommandIcon.GetComponent<Button>().onClick.AddListener(() =>
            {
                ConstructionManagerScript.Instance.TryEnterBuildMode(BuildingData);
            });

            BuildingCostIconContainer.transform.DetachChildren();
            foreach (ResourceAmount resourceAmount in CostCalculator.GetItemCosts(BuildingData.ItemType))
            {
                GameObject buildListResourceIcon = Instantiate(BuildingCostIconPrefab);
                buildListResourceIcon.transform.SetParent(BuildingCostIconContainer.transform);
                buildListResourceIcon.GetComponent<Image>().sprite = IconDictionary.GetIconSprite(resourceAmount.ItemType);
                buildListResourceIcon.GetComponentInChildren<TMP_Text>().text = resourceAmount.Amount.ToString();
            }

            BuildingDescription.GetComponent<TMP_Text>().text = $"{BuildingData.ItemName}: {BuildingData.ItemDescription}";

            BuildGridPanel.SetActive(false);
            BuildingInfoPanel.SetActive(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }
        public void OnPointerExit(PointerEventData eventData)
        {

        }
    }
}