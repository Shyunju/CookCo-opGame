using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BuyRecipe : MonoBehaviour
    {
        [SerializeField] int _price;
        [SerializeField] int[] _ingredientsTableIndex;
        LobbyUIController _lobbyUIController;
        Button _button;
        bool _isBought = false;

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
            _button = this.GetComponentInChildren<Button>();
            SetCanBuy();
        }
        public void BuyThisRecipe()
        {
            GameManager.Instance.ChangeWalletGold(_price * -1);
            _lobbyUIController.LoadWallet();
            _button.interactable = false;
            _isBought = true;
        }
        public void SetCanBuy()
        {
            if (!_isBought)
            {
                foreach (var i in _ingredientsTableIndex)
                {
                    if (!GameManager.Instance.ShopTables[i].isBought)
                    {
                        _button.interactable = false;
                        break;
                    }
                    _button.interactable = true;
                }
            }
        }
    }
}
