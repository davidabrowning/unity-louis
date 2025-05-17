using UnityEngine;

namespace FarmerDemo
{
    public class WorldBuilderScript : MonoBehaviourSingletonBase<WorldBuilderScript>
    {
        private void Start()
        {
            BuildInitialWorld();
        }
        public void BuildInitialWorld()
        {
            for (int x = -5; x < 5; x++)
            {
                for (int y = -5; y < 5; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        RegionBuilderScript.Instance.BuildRegion(new Vector2Int(x, y), RegionTypeEnum.Bush);
                    }
                    else
                    {
                        int choice = Random.Range(1, 10);
                        if (choice < 5)
                            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(x, y), RegionTypeEnum.Water);
                        else if (choice < 9)
                            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(x, y), RegionTypeEnum.Dirt);
                        else
                            RegionBuilderScript.Instance.BuildRegion(new Vector2Int(x, y), RegionTypeEnum.Bush);
                    }
                }
            }
        }
    }
}