using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    public abstract class ItemInteractableBehaviour : ItemBehaviour
    {
        public List<ObjectAction> Actions = new();

        protected virtual void Start()
        {
            PopulateActions();
        }

        protected abstract void PopulateActions();
        public abstract void Interact(string actionId);
    }
}