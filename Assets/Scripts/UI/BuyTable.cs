using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BuyTable : MonoBehaviour
    {
        [SerializeField] int _shopTableIndex;
        [SerializeField] int _price;
        [SerializeField] TMP_Text _buttonText;
        LobbyUIController _lobbyUIController;
        Button _button;

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
            _button = this.GetComponentInChildren<Button>();
            if(_shopTableIndex != 100)
            {
                if (GameManager.Instance.ShopTables[_shopTableIndex].isBought)
                {
                    SoldOut();
                }                
            }
        }

        public void BuyThisTable()
        {
            if (GameManager.Instance.ChangeWalletGold(_price * -1))
            {
                _lobbyUIController.LoadWallet();
                _lobbyUIController.UpgradeRecipeSet();
                SoldOut();
                if(_shopTableIndex != 100)
                    GameManager.Instance.ShopTables[_shopTableIndex].isBought = true;
                else
                    _lobbyUIController.ChangePannel(3);
            }
            else
            {
                _lobbyUIController.ShowWarningUI();
            }
        }
        void SoldOut()
        {
            _button.interactable = false;
            _buttonText.text = "SOLD OUT";
        }
    }
}
