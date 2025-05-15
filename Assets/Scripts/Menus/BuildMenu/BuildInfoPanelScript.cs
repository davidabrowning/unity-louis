using UnityEngine;
using UnityEngine.EventSystems;

namespace FarmerDemo
{
    public class BuildInfoPanelScript : MonoBehaviour, IPointerExitHandler
    {
        public GameObject BuildGridPanel;
        public void OnPointerExit(PointerEventData eventData)
        {
            gameObject.SetActive(false);
            BuildGridPanel.SetActive(true);
        }
    }
}
