using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public List<ItemData> ItemDataList { get; private set; }
        public List<RecipeData> RecipeDataList { get; private set; }
        public int Wallet { get; private set; }
        private void Start()
        {
            Wallet = 0;
            //LobbyUIController.Instance.LoadWallet();
            _itemDataManager = new ItemDataManager();
            _recipeDataManager = new RecipeDataManager();
            ItemDataList = new List<ItemData>();
            ItemDataList = _itemDataManager.GetAllItems();
            RecipeDataList = new List<RecipeData>();
            RecipeDataList = _recipeDataManager.GetAllRecipes();
        }
        public void StartCooking()
        {
            SceneManager.LoadScene("MainScene");
        }

        public void ChangeWalletGold(int amount)
        {
            Wallet += amount;
        }
        public void GoToLobby()
        {
            Wallet += CookingPlayManager.Instance.Score;
            SceneManager.LoadScene("LobbyScene");
        }

    }
}
