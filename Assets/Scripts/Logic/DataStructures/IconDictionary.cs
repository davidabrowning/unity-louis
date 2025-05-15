using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FarmerDemo
{
    [CreateAssetMenu(fileName = "IconDictionary", menuName = "Scriptable Objects/IconDictionary")]
    public class IconDictionary : ScriptableObject
    {
        [Serializable]
        public class ItemIconEntry
        {
            public ItemType ItemType;
            public Sprite IconSprite;
        }

        public List<ItemIconEntry> ItemIconEntries = new();
        public Sprite GetIconSprite(ItemType itemType)
        {
            return ItemIconEntries
                .Where(e => e.ItemType == itemType)
                .Select(e => e.IconSprite)
                .FirstOrDefault();
        }
    }
}