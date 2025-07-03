using UnityEngine;
using System.Collections.Generic;
namespace CookCo_opGame
{
    [System.Serializable]
    public class ItemData
    {
        public int ItemID;
        public string ItemName;
        public string IconPath;

        [System.NonSerialized]
        public Sprite IconSprite;
    }

    [System.Serializable]
    public class ItemDataList
    {
        public List<ItemData> Items = new List<ItemData>();
    }
}
