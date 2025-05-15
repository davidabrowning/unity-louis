using UnityEngine;

namespace FarmerDemo
{
    [System.Serializable]
    public class ObjectAction
    {
        public ItemInteractableBehaviour Target;
        public string ActionId;
        public string ActionName;
        
        public ObjectAction(ItemInteractableBehaviour target, string actionId, string actionName)
        {
            Target = target;
            ActionId = actionId;
            ActionName = actionName;
        }
    }
}