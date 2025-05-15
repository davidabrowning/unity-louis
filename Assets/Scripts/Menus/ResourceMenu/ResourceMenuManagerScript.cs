using Codice.CM.Common;
using CodiceApp.Gravatar;
using FarmerDemo;
using System.Threading;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace FarmerDemo
{
    public class ResourceMenuManagerScript : MonoBehaviour
    {
        public TMP_Text TwigCountText;
        public TMP_Text BerryCountText;
        public TMP_Text StoneCountText;
        public TMP_Text IronCountText;
        public TMP_Text CircuitCountText;
        public TMP_Text FishCountText;
        public TMP_Text SeedCountText;
        private int _twigs, _berries, _stones, _irons, _circuits, _fishes, _seeds;

        private void Awake()
        {
            HideAdvancedResourceIcons();
        }

        void Update()
        {
            UpdateInventoryCounts();
            UpdateInventoryCountDisplays();
            UnhideCountsForDiscoveredResources();
        }

        private void HideAdvancedResourceIcons()
        {
            TwigCountText.transform.parent.gameObject.SetActive(false);
            BerryCountText.transform.parent.gameObject.SetActive(false);
            StoneCountText.transform.parent.gameObject.SetActive(false);
            IronCountText.transform.parent.gameObject.SetActive(false);
            CircuitCountText.transform.parent.gameObject.SetActive(false);
            FishCountText.transform.parent.gameObject.SetActive(false);
            SeedCountText.transform.parent.gameObject.SetActive(false);
        }

        private void UpdateInventoryCounts()
        {
            _twigs = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Twig);
            _berries = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Berry);
            _stones = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Stone);
            _irons = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Iron);
            _circuits = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Circuit);
            _fishes = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Fish);
            _seeds = PlayerScript.Instance.GetComponent<PlayerScript>().AmountInInventory(ItemType.Seed);
        }

        private void UpdateInventoryCountDisplays()
        {
            TwigCountText.text = _twigs.ToString();
            BerryCountText.text = _berries.ToString();
            StoneCountText.text = _stones.ToString();
            IronCountText.text = _irons.ToString();
            CircuitCountText.text = _circuits.ToString();
            FishCountText.text = _fishes.ToString();
            SeedCountText.text = _seeds.ToString();
        }

        private void UnhideCountsForDiscoveredResources()
        {
            if (_twigs > 0)
                TwigCountText.transform.parent.gameObject.SetActive(true);
            if (_berries > 0)
                BerryCountText.transform.parent.gameObject.SetActive(true);
            if (_stones > 0)
                StoneCountText.transform.parent.gameObject.SetActive(true);
            if (_irons > 0)
                IronCountText.transform.parent.gameObject.SetActive(true);
            if (_circuits > 0)
                CircuitCountText.transform.parent.gameObject.SetActive(true);
            if (_fishes > 0)
                FishCountText.transform.parent.gameObject.SetActive(true);
            if (_seeds > 0)
                SeedCountText.transform.parent.gameObject.SetActive(true);
        }
    }
}