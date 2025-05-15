using System.Collections.Generic;

namespace FarmerDemo
{
    internal interface IManualHarvestable
    {
        List<ResourceAmount> ManualHarvest();
    }
}
