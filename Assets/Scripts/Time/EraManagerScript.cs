using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FarmerDemo
{
    public class EraManagerScript : MonoBehaviourSingletonBase<EraManagerScript>
    {
        public GameObject EraPanel;
        public Image EraImage;
        public TMP_Text EraText;
        public TMP_Text StartNextEraButtonText;
        public EraType CurrentEra = EraType.Survival;
        public AudioSource AudioSource;
        public List<AudioClip> BackgroundSongs;
        public AudioClip IntraEraSoundClip;
        public event Action EraUpdate; // Holds listeners to era updates

        private void Start()
        {
            EraPanel.SetActive(false);
            AudioSource.PlayOneShot(BackgroundSongs[(int)CurrentEra]);
        }
        public void AdvanceEra()
        {
            UpdateEraPanelContents();
            ShowAdvanceEraPanel();
            CurrentEra++;
            EraUpdate.Invoke(); // Notify all listeners that era has updated
            AudioSource.Stop();
            AudioSource.PlayOneShot(IntraEraSoundClip);
            AudioSource.PlayOneShot(BackgroundSongs[(int)CurrentEra]);
            ResearchManagerScript.Instance.ResetResearchBar();
            BuildMenuManagerScript.Instance.ShowAdvancedBuildings();
        }
        private void UpdateEraPanelContents()
        {
            switch (CurrentEra)
            {
                case EraType.Survival:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/Berries");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin power era";
                    break;
                case EraType.Power:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/WoodFire");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin automation era";
                    break;
                case EraType.Automation:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/RobotField");
                    EraText.text = "Research complete!";
                    StartNextEraButtonText.text = "Begin disease research era";
                    break;
                case EraType.ScientificAdvancement:
                    EraImage.sprite = Resources.Load<Sprite>("Art/SplashImages/PlantBuilding");
                    EraText.text = "Cure research complete. You win the game!";
                    StartNextEraButtonText.text = "Close";
                    break;
            }
        }
        public void ShowAdvanceEraPanel()
        {
            EraPanel.SetActive(true);
        }

        public void HideAdvanceEraPanel()
        {
            EraPanel.SetActive(false);
        }
    }
}