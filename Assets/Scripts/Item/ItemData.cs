using UnityEngine;
using System.Collections.Generic;
namespace CookCo_opGame
{
    [System.Serializable]
    public class ItemData
    {
        public int _itemID;
        public string _itemName;
        public string _iconPath;

        [System.NonSerialized] 
        public Sprite itemSprite;
        public int ItemID { get { return _itemID; } set { _itemID = value; } }
        public string ItemName { get { return _itemName; } set { _itemName = value; } }
    }

    [System.Serializable]
    public class ItemDataList
    {
        public List<ItemData> Items = new List<ItemData>();
    }
}
