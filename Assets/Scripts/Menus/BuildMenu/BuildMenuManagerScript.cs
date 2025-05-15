using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class BuildMenuManagerScript : MonoBehaviourSingletonBase<BuildMenuManagerScript>
    {
        public IconDictionary IconDictionary;
        public List<ItemData> AvailableBuildings = new();
        public GameObject BuildGridPanel;
        public GameObject BuildButtonPrefab;
        public GameObject BuildingInfoPanel;
        public GameObject BuildCommandIcon;
        public GameObject BuildingDescription;
        public GameObject BuildingCostIconContainer;
        public GameObject BuildingCostIconPrefab;
        private List<GameObject> _buildButtons = new();
        protected override void Awake()
        {
            base.Awake();
            PopulateBuildMenu();
            HideAdvancedBuildingInventorySlots();
        }

        private void PopulateBuildMenu()
        {
            foreach (ItemData building in AvailableBuildings)
            {
                GameObject buildButton = Instantiate(BuildButtonPrefab);
                buildButton.GetComponent<Image>().sprite = IconDictionary.GetIconSprite(building.ItemType);
                buildButton.transform.SetParent(BuildGridPanel.transform, false);
                _buildButtons.Add(buildButton);

                BuildMenuIconScript buildButtonScript = buildButton.GetComponent<BuildMenuIconScript>();
                buildButtonScript.IconDictionary = IconDictionary;
                buildButtonScript.BuildingData = building;
                buildButtonScript.BuildGridPanel = BuildGridPanel;
                buildButtonScript.BuildingInfoPanel = BuildingInfoPanel;
                buildButtonScript.BuildCommandIcon = BuildCommandIcon;
                buildButtonScript.BuildingDescription = BuildingDescription;
                buildButtonScript.BuildingCostIconContainer = BuildingCostIconContainer;
                buildButtonScript.BuildingCostIconPrefab = BuildingCostIconPrefab;
            }
            BuildCommandIcon.AddComponent<Button>();
        }

        private void HideAdvancedBuildingInventorySlots()
        {
            for (int i = 1; i < _buildButtons.Count; i++)
            {
                _buildButtons[i].SetActive(false);
            }
        }

        public void ShowLabInventorySlot()
        {
            _buildButtons[1].SetActive(true);
        }

        public void ShowAdvancedBuildings()
        {
            switch (EraManagerScript.Instance.CurrentEra)
            {
                case EraType.Survival:
                    break;
                case EraType.Power:
                    _buildButtons[2].SetActive(true); // Wood burner
                    _buildButtons[3].SetActive(true); // Circuit maker
                    break;
                case EraType.Automation:
                    _buildButtons[4].SetActive(true); // Solar panel
                    _buildButtons[5].SetActive(true); // AutoHarvester
                    break;
                case EraType.ScientificAdvancement:
                    _buildButtons[6].SetActive(true); // SeedSplicer
                    _buildButtons[7].SetActive(true); // ARM
                    break;
            }
        }
    }
}