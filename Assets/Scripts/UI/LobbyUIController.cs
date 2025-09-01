using System;
using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class LobbyUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _walletTxt;
        [SerializeField] BuyRecipe[] _recipeUIArray;
        [SerializeField] GameObject[] _pannelUIArray;
        [SerializeField] GameObject _buyWarningUI;
        void Start()
        {
            LoadWallet();
        }

        public void LoadWallet()
        {
            _walletTxt.text = GameManager.Instance.Wallet.ToString();
        }

        public void UpgradeRecipeSet()
        {
            foreach (var item in _recipeUIArray)
            {
                item.SetCanBuy();
            }
        }

        // public void ShowTableShop()
        // {
        //     _tableShop.SetActive(true);
        //     _recipeShop.SetActive(false);
        // }
        // public void ShowRecipeShop()
        // {
        //     _recipeShop.SetActive(true);
        //     _tableShop.SetActive(false);
        // }
        public void ChangePannel(int index)
        {
            for (int i = 0; i < _pannelUIArray.Length; i++)
            {
                if (index == i)
                {
                    _pannelUIArray[i].SetActive(true);
                }
                else
                {
                    _pannelUIArray[i].SetActive(false);                                    
                }
            }
        }
        public void ShowWarningUI()
        {
            _buyWarningUI.SetActive(true);
        }
    }
}
