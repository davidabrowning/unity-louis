using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FarmerDemo
{
    internal class SolarPanelBehaviour : BuildingBehaviour
    {
        protected override void Start()
        {
            base.Start();
            PlayerScript.Instance.AddActivePowerProducer(gameObject);
        }
        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "deconstruct":
                    PlayerScript.Instance.RemoveActivePowerProducer(gameObject);
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }
    }
}
