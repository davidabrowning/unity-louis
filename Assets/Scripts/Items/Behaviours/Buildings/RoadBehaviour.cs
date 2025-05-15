using UnityEngine;

namespace FarmerDemo
{
    public class RoadBehaviour : BuildingBehaviour
    {
        public override void Interact(string actionId)
        {
            switch (actionId)
            {
                case "deconstruct":
                    Deconstruct();
                    break;
                default:
                    Debug.Log("Unknown action");
                    break;
            }
        }

        protected override void PopulateActions()
        {
            Actions.Add(new ObjectAction(this, "add_wood", "Add a twig to the burner"));
            Actions.Add(new ObjectAction(this, "deconstruct", "Deconstruct"));
        }
    }
}