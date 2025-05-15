using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace FarmerDemo
{
    public class UIItemPopupMenu : MonoBehaviour
    {
        public Transform ButtonContainer;
        public GameObject ActionButtonPrefab;
        public GameObject CancelButtonPrefab;

        public void Setup(List<ObjectAction> actions, Vector3 screenPos)
        {
            transform.position = screenPos;
            ClearOldButtons();
            AddActionButtons(actions);
            AddCancelButton();
        }

        private void ClearOldButtons()
        {
            foreach (Transform child in ButtonContainer)
                Destroy(child.gameObject);
        }

        private void AddActionButtons(List<ObjectAction> actions)
        {
            foreach (ObjectAction action in actions)
            {
                GameObject buttonObject = Instantiate(ActionButtonPrefab, ButtonContainer);
                Button button = buttonObject.GetComponent<Button>();
                TMP_Text buttonText = buttonObject.GetComponentInChildren<TMP_Text>();

                buttonText.text = action.ActionName;
                button.onClick.AddListener(() => action.Target.Interact(action.ActionId));
                button.onClick.AddListener(CloseMenu);
            }
        }

        private void AddCancelButton()
        {
            GameObject cancelButtonObject = Instantiate(CancelButtonPrefab, ButtonContainer);
            Button cancelButton = cancelButtonObject.GetComponent<Button>();
            TMP_Text cancelButtonText = cancelButtonObject.GetComponentInChildren<TMP_Text>();

            cancelButton.onClick.AddListener(CloseMenu);
        }

        private void CloseMenu()
        {
            Destroy(gameObject);
        }
    }
}