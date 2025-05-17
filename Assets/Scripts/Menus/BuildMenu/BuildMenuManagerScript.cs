using System.Collections.Generic;
using System.Linq;
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
            ShowAdvancedBuildings();
        }

        private void PopulateBuildMenu()
        {
            foreach (ItemData building in AvailableBuildings)
            {
                GameObject buildButton = Instantiate(BuildButtonPrefab);
                buildButton.GetComponent<Image>().sprite = IconDictionary.GetIconSprite(building.ItemType);
                buildButton.transform.SetParent(BuildGridPanel.transform, false);
                buildButton.SetActive(false);
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

        public void ShowLabInventorySlot()
        {
            _buildButtons
                .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.Lab)
                .FirstOrDefault()
                .SetActive(true);
        }

        public void ShowAdvancedBuildings()
        {
            switch (EraManagerScript.Instance.CurrentEra)
            {
                case EraType.Survival:
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.Road)
                        .FirstOrDefault()
                        .SetActive(true);
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.WoodBurner)
                        .FirstOrDefault()
                        .SetActive(true);
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.Fabricator)
                        .FirstOrDefault()
                        .SetActive(true);
                    break;
                case EraType.Power:
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.CircuitMaker)
                        .FirstOrDefault()
                        .SetActive(true);
                    break;
                case EraType.Automation:
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.SolarPanel)
                        .FirstOrDefault()
                        .SetActive(true);
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.AutoHarvester)
                        .FirstOrDefault()
                        .SetActive(true);
                    break;
                case EraType.ScientificAdvancement:
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.SeedSplicer)
                        .FirstOrDefault()
                        .SetActive(true);
                    _buildButtons
                        .Where(bb => bb.GetComponent<BuildMenuIconScript>().BuildingData.ItemType == ItemType.ARM)
                        .FirstOrDefault()
                        .SetActive(true);
                    break;
            }
        }
    }
}