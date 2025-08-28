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
        //public LobbyUIController LobbyUIController { get { return _lobbyUIController;} set { _lobbyUIController = value;}}

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
            _button = this.GetComponentInChildren<Button>();
            if (GameManager.Instance.ShopTables[_shopTableIndex].isBought)
            {
                SoldOut();
            }
        }

        public void BuyThisTable()
        {
            GameManager.Instance.ShopTables[_shopTableIndex].isBought = true;
            GameManager.Instance.ChangeWalletGold(_price * -1);
            _lobbyUIController.LoadWallet();
            _lobbyUIController.UpgradeRecipeSet();
            SoldOut();
        }
        void SoldOut()
        {
            _button.interactable = false;
            _buttonText.text = "SOLD OUT";
        }
    }
}
