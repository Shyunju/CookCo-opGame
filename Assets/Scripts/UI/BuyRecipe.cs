using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BuyRecipe : MonoBehaviour
    {
        [SerializeField] int _recipeID;
        [SerializeField] int _price;
        [SerializeField] int[] _ingredientsTableIndex;
        [SerializeField] TMP_Text _buttonText;
        LobbyUIController _lobbyUIController;
        Button _button;

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
            _button = this.GetComponentInChildren<Button>();
            SetCanBuy();
        }
        public void BuyThisRecipe()
        {
            if (GameManager.Instance.ChangeWalletGold(_price * -1))
            {
                GameManager.Instance.HasRecipes.Add(_recipeID);
                _lobbyUIController.LoadWallet();
                SetCanBuy();
            }
            else
            {
                _lobbyUIController.ShowWarningUI();
            }
        }
        public void SetCanBuy()
        {
            if (GameManager.Instance.HasRecipes.Contains(_recipeID))
            {
                _buttonText.text = "SOLD OUT";
                _button.interactable = false;
                return;
            }

            bool canBuy = true;
            foreach (var i in _ingredientsTableIndex)
            {
                if (!GameManager.Instance.ShopTables[i].isBought)
                {
                    canBuy = false;
                    break;
                }
            }

            _buttonText.text = "-" + _price.ToString();
            _button.interactable = canBuy;

        }
    }
}
