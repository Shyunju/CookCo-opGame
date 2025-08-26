using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class LobbyUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _walletTxt;
        void Start()
        {
            LoadWallet();
        }

        public void LoadWallet()
        {
            _walletTxt.text = GameManager.Instance.Wallet.ToString();
        }
    }
}
