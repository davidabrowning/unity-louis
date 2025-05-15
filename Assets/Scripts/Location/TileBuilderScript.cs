using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace FarmerDemo
{
    public class TileBuilderScript : MonoBehaviourSingletonBase<TileBuilderScript>
    {
        public GameObject TileFolder;
        public GameObject DirtBackground;
        public GameObject GrassBackground;
        public GameObject WaterBackground;
        public Dictionary<Vector2Int, RegionTypeEnum> TileMap = new();

        public void PlaceTile(RegionTypeEnum regionType, Vector2 coords)
        {
            GameObject tile;
            switch (regionType)
            {
                case RegionTypeEnum.Tree:
                    tile = GrassBackground;
                    break;
                case RegionTypeEnum.Bush:
                    tile = GrassBackground;
                    break;
                case RegionTypeEnum.Dirt:
                    tile = DirtBackground;
                    break;
                case RegionTypeEnum.Water:
                    tile = WaterBackground;
                    break;
                default:
                    tile = WaterBackground;
                    break;
            }
            PlaceBackgroundTile(tile, regionType, new Vector3(coords.x, coords.y, 0));
        }

        void PlaceBackgroundTile(GameObject backgroundTile, RegionTypeEnum regionType, Vector3 position)
        {
            GameObject area = Instantiate(backgroundTile, position, Quaternion.identity);
            TileMap.Add(new Vector2Int((int)position.x, (int)position.y), regionType);

            SpriteRenderer renderer = area.GetComponent<SpriteRenderer>();
            Vector2 spriteSize = renderer.sprite.bounds.size;
            area.transform.localScale = new Vector3(1 / spriteSize.x, 1 / spriteSize.y, 1);
            area.transform.SetParent(TileFolder.transform);
            //if ((position.x + position.y) % 2 == 0)
            //    renderer.color = Color.gray;
        }

        public RegionTypeEnum GetRegionType(Vector2Int location)
        {
            return TileMap.GetValueOrDefault(location);
        }
    }
}