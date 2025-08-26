using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BuyTable : MonoBehaviour
    {
        [SerializeField] int _shopTableIndex;
        [SerializeField] int _price;
        LobbyUIController _lobbyUIController;
        public LobbyUIController LobbyUIController { get { return _lobbyUIController;} set { _lobbyUIController = value;}}

        void Start()
        {
            _lobbyUIController = GetComponentInParent<LobbyUIController>();
        }

        public void BuyThisTable()
        {
            GameManager.Instance.ShopTables[_shopTableIndex].isBought = true;
            GameManager.Instance.ChangeWalletGold(_price * -1);
            _lobbyUIController.LoadWallet();
            Button tempButton = this.GetComponentInChildren<Button>();
            tempButton.interactable = false;
        }
    }
}
