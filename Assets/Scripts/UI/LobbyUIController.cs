using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class LobbyUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _walletTxt;
        [SerializeField] BuyRecipe[] _recipeUIArray;
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
    }
}
