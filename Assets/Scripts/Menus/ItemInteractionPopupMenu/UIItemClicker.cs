using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmerDemo
{
    public class UIItemClicker : MonoBehaviour
    {
        public GameObject MenuCanvas;
        public GameObject MenuPrefab;
        private GameObject _currentMenu;
        private ItemInteractableBehaviour _clickedInteractable;

        void Update()
        {
            if (MousePressed() && NoMenuYet() && !EventSystem.current.IsPointerOverGameObject() )
            {
                if (SetClickedInteractable())
                {
                    InstantiateMenu();
                }
            }
        }
        private bool MousePressed() => Input.GetMouseButtonDown(0);
        private bool NoMenuYet() => _currentMenu == null;
        private bool SetClickedInteractable()
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                ItemInteractableBehaviour interactable = hit.collider.GetComponentInParent<ItemInteractableBehaviour>();
                if (interactable != null)
                {
                    _clickedInteractable = interactable;
                    return true;
                }
            }
            return false;
        }
        private void InstantiateMenu()
        {
            _currentMenu = Instantiate(MenuPrefab, MenuCanvas.transform);
            UIItemPopupMenu popup = _currentMenu.GetComponent<UIItemPopupMenu>();
            popup.Setup(_clickedInteractable.Actions, Input.mousePosition);
        }
    }
}