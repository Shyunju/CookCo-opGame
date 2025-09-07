using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        public List<int> HasRecipes { get; set; }
        public int Wallet { get; private set; }
        public static event Action OnInputStopRequest;
        override protected void Awake()
        {
            base.Awake();
            Wallet = 200000;
            _itemDataManager = new ItemDataManager();
            _recipeDataManager = new RecipeDataManager();
            ItemDataList = new List<ItemData>();
            ItemDataList = _itemDataManager.GetAllItems();
            RecipeDataList = new List<RecipeData>();
            RecipeDataList = _recipeDataManager.GetAllRecipes();
            HasRecipes = new List<int>
            {
                1 //SunnySideUp
            };
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
            Wallet += CookingPlayManager.Instance.Score;
            SceneManager.LoadScene("LobbyScene");
            SoundManager.Instance.PlayLobbyBGM();
        }

        public void TriggerInputStop()
        {
            OnInputStopRequest?.Invoke();
        }

    }
}
