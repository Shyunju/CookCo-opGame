using UnityEngine;

namespace CookCo_opGame
{
    public class BuyTable : MonoBehaviour
    {
        [SerializeField] int _shopTableIndex;
        [SerializeField] int _price;

        public void BuyThisTable()
        {
            GameManager.Instance.ShopTables[_shopTableIndex].isBought = true;
            GameManager.Instance.ChangeWalletGold(_price * -1);
        }
    }
}
