using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        public List<ItemData> ItemDataList { get;  private set; }
        void Awake()
        {
            
            _itemDataManager = new ItemDataManager();
        }
        private void Start()
        {

            //ItemData data = manager.GetItemByID(2);
            //Debug.Log(data.ItemName);
            ItemDataList = _itemDataManager.GetAllItems();
        }
    }
}
