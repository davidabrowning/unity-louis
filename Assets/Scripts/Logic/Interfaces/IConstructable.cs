using System.Collections.Generic;

namespace FarmerDemo
{
    public interface IConstructable
    {
        List<ResourceAmount> ConstructionCosts { get; }
    }
}
