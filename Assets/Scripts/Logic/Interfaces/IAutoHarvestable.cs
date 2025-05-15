using System.Collections.Generic;

namespace FarmerDemo
{
    public interface IAutoHarvestable
    {
        List<ResourceAmount> AutoHarvest();
    }
}