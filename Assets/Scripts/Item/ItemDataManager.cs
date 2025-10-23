using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public class ItemDataManager
    {
        private ItemDataList _itemList = new ItemDataList();
        TextAsset _jsonAsset;

        public ItemDataManager()
        {
            _jsonAsset = Resources.Load<TextAsset>("Items");
            LoadItems();
        }        
        public void LoadItems() //아이템 리스트 로드
        {
            if (_jsonAsset != null)
            {
                string json = _jsonAsset.text;
                _itemList = JsonUtility.FromJson<ItemDataList>(json);
                foreach (var item in _itemList.Items)
                {
                    //Resources/                  
                    item.IconSprite = Resources.Load<Sprite>(item.IconPath); //경로로 이미지 찾아 로드
                }
            }
            else
            {
                _itemList = new ItemDataList();
            }
        }
        public ItemData GetItemByID(int id)
        {            
            ItemData it = _itemList.Items.Find((item) => item.ItemID == id);
            return it;
        }
        public List<ItemData> GetAllItems()
        {
            return _itemList.Items;
        }
    }
}
