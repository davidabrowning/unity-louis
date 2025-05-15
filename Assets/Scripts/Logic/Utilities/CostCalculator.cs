using System.Collections.Generic;

namespace FarmerDemo
{
    public static class CostCalculator
    {
        public static List<ResourceAmount> GetItemCosts(ItemType itemType)
        {
            // Tools
            switch (itemType)
            {
                case ItemType.Basket:
                    return new() { new ResourceAmount(ItemType.Twig, 5) };
                case ItemType.Pickaxe:
                    return new() { new ResourceAmount(ItemType.Twig, 5), new ResourceAmount(ItemType.Stone, 2) };
                default:
                    break;
            }
            // Advanced resources
            switch (itemType)
            {
                case ItemType.CircuitBatch:
                    return new() { new ResourceAmount(ItemType.Berry, 2), new ResourceAmount(ItemType.Iron, 2) };
                case ItemType.SplicedSeeds:
                    return new() { new ResourceAmount(ItemType.Berry, 5), new ResourceAmount(ItemType.Fish, 5), new ResourceAmount(ItemType.Circuit, 1) };
                default:
                    break;
            }
            // Buildings
            switch (itemType)
            {
                case ItemType.Road:
                    return new() { new ResourceAmount(ItemType.Stone, 1) };
                case ItemType.Fabricator:
                    return new() { new ResourceAmount(ItemType.Twig, 1), new ResourceAmount(ItemType.Stone, 1) };
                case ItemType.Lab:
                    return new() { new ResourceAmount(ItemType.Stone, 3) };
                case ItemType.WoodBurner:
                    return new() { new ResourceAmount(ItemType.Stone, 50) };
                case ItemType.CircuitMaker:
                    return new() { new ResourceAmount(ItemType.Berry, 1), new ResourceAmount(ItemType.Iron, 8) };
                case ItemType.AutoHarvester:
                    return new() { new ResourceAmount(ItemType.Berry, 5), new ResourceAmount(ItemType.Circuit, 2) };
                case ItemType.SolarPanel:
                    return new() { new ResourceAmount(ItemType.Twig, 15), new ResourceAmount(ItemType.Circuit, 5) };
                case ItemType.SeedSplicer:
                    return new() { new ResourceAmount(ItemType.Circuit, 3), new ResourceAmount(ItemType.Stone, 5) };
                case ItemType.ARM:
                    return new() { new ResourceAmount(ItemType.Circuit, 50), new ResourceAmount(ItemType.Iron, 50) };
                default:
                    break;
            }
            return new();
        }

        // StandardResearch costs
        public static ResourceAmount StandardResearch()
        {
            switch (EraManagerScript.Instance.CurrentEra)
            {
                case EraType.Survival:
                    return new ResourceAmount(ItemType.Berry, 1);
                case EraType.Power:
                    return new ResourceAmount(ItemType.Circuit, 1);
                case EraType.Automation:
                    return new ResourceAmount(ItemType.Fish, 1);
            }
            return null;
        }

        // Other
        public static List<ResourceAmount> ARMCure() => new() { new ResourceAmount(ItemType.Seed, 3), new ResourceAmount(ItemType.Circuit, 3) };
    }
}
