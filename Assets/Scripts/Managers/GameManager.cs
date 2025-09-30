using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Action = System.Action;

namespace CookCo_opGame
{
    [Serializable]
    public struct SellingTable
    {
        public string tableName;
        public bool isBought;
    }
    public class GameManager : Singleton<GameManager>
    {
        ItemDataManager _itemDataManager;
        RecipeDataManager _recipeDataManager;
        [SerializeField] SellingTable[] _shopTables;
        
        public SellingTable[] ShopTables { get { return _shopTables; } set { _shopTables = value; } }
        public int Aggregate { get; set; }
        public List<ItemData> ItemDataList { get; private set; }
        public List<RecipeData> RecipeDataList { get; private set; }
        public List<int> HasRecipes { get; set; }
        public int Wallet { get; private set; }
        public static event Action OnInputStopRequest;
        override protected void Awake()
        {
            base.Awake();
            _itemDataManager = new ItemDataManager();
            _recipeDataManager = new RecipeDataManager();
            ItemDataList = new List<ItemData>();
            ItemDataList = _itemDataManager.GetAllItems();
            RecipeDataList = new List<RecipeData>();
            RecipeDataList = _recipeDataManager.GetAllRecipes();
            // Wallet = 10000;
            // Aggregate = 0;
            // HasRecipes = new List<int>
            // {
            //     1 //SunnySideUp
            // };
            SetPlayerData();
            SoundManager.Instance.PlayLobbyBGM();
        }
        public void StartCooking()
        {
            SceneManager.LoadScene("MainScene");
        }

        public bool ChangeWalletGold(int amount)
        {
            if (Wallet + amount < 0)
            {
                return false;
            }
            Wallet += amount;
            return true;
        }
        public void GoToLobby()
        {
            SceneManager.LoadScene("LobbyScene");
            SoundManager.Instance.PlayLobbyBGM();
        }

        public void TriggerInputStop()
        {
            OnInputStopRequest?.Invoke();
        }

        public void SetPlayerData()
        {
            if (DataManager.Instance.NowPlayer != null)
            {
                Wallet = DataManager.Instance.NowPlayer.Wallet;
                Aggregate = DataManager.Instance.NowPlayer.Aggregate;
                HasRecipes = DataManager.Instance.NowPlayer.HasRecipes;
                if (HasRecipes.Count == 0)
                {
                    HasRecipes.Add(1);
                }

                if (DataManager.Instance.NowPlayer.IsTablesBought != null)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (i < _shopTables.Length && i < DataManager.Instance.NowPlayer.IsTablesBought.Length)
                        {
                            _shopTables[i].isBought = DataManager.Instance.NowPlayer.IsTablesBought[i];
                        }
                    }
                }
            }
        }

        public void UpdateDataForSaving()
        {
            if (DataManager.Instance.NowPlayer != null)
            {
                DataManager.Instance.NowPlayer.Wallet = Wallet;
                DataManager.Instance.NowPlayer.Aggregate = Aggregate;
                DataManager.Instance.NowPlayer.HasRecipes = HasRecipes;

                for (int i = 0; i < 12; i++)
                {
                    if (i < _shopTables.Length && i < DataManager.Instance.NowPlayer.IsTablesBought.Length)
                    {
                        DataManager.Instance.NowPlayer.IsTablesBought[i] = _shopTables[i].isBought;
                    }
                }
                DataManager.Instance.NowPlayer.year = DateTime.Now.Year;
                DataManager.Instance.NowPlayer.month = DateTime.Now.Month;
                DataManager.Instance.NowPlayer.day = DateTime.Now.Day;
            }
        }
        

    }
}
