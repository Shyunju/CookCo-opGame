using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        List<List<int>> _orders = new List<List<int>>();
        public List<ItemData> ItemDataList { get; private set; }
        public List<List<int>> Orders { get{ return _orders; } }
        void Awake()
        {
            
            _itemDataManager = new ItemDataManager();
        }
        private void Start()
        {

            //ItemData data = manager.GetItemByID(2);
            //Debug.Log(data.ItemName);
            ItemDataList = _itemDataManager.GetAllItems();
            List<int> test = new List<int>();
            test.Add(1);
            _orders.Add(test);
        }
    }
}
