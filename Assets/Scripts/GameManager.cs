using System.Collections.Generic;
using UnityEngine;

namespace CookCo_opGame
{
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        List<List<int>> _orders = new List<List<int>>();
        public List<ItemData> ItemDataList { get; private set; }
        public List<List<int>> Orders { get { return _orders; } }
        public int Score { get; private set; }
        void Awake()
        {

            _itemDataManager = new ItemDataManager();
        }
        private void Start()
        {
            ItemDataList = _itemDataManager.GetAllItems();
            List<int> test = new List<int>();
            test.Add(1);
            _orders.Add(test);
        }
        public void ChangeScore(int mount)
        {
            Score += mount;
        }
    }
}
