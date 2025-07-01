using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CookCo_opGame
{
    public class ItemDataManager
    {
        private ItemDataList _itemList = new ItemDataList();
        private string _jsonPath;
        TextAsset jsonAsset;

        public ItemDataManager()
        {
            //Assets/
            //_jsonPath = Path.Combine(Application.dataPath, "Resources/Items.json");
            jsonAsset = Resources.Load<TextAsset>("Items");

            LoadItems();
        }


        //아이템 리스트 로드
        public void LoadItems()
        {

            if (jsonAsset != null)
            {
                string json = jsonAsset.text;
                _itemList = JsonUtility.FromJson<ItemDataList>(json);
                foreach (var item in _itemList.Items)
                {
                    //Resources/                    
                    item.itemSprite = Resources.Load<Sprite>(item._iconPath); //경로로 이미지 찾아 로드
                }
            }
            else
            {
                _itemList = new ItemDataList();
            }
        }

        public ItemData GetItemByID(int id)
        {
            
            ItemData it = _itemList.Items.Find((item) => item._itemID == id);
            return it;
        }

        public List<ItemData> GetAllItems()
        {
            return _itemList.Items;
        }
    }
}
