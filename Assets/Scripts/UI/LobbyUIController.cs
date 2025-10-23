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
        [SerializeField] GameObject _saveAlertUI;
        [SerializeField] GameObject _pressedButton;
        [SerializeField] GameObject _originButton;
        [SerializeField] GameObject _player2Objcet;
        void Start()
        {
            LoadWallet();
            SetPlayers();
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

        public void ChangePannel(int index)
        {
            SoundManager.Instance.PlayPaperSound();
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
        public void OnSaveButton()
        {
            GameManager.Instance.UpdateDataForSaving();
            DataManager.Instance.SaveData();
            _saveAlertUI.SetActive(true);
        }
        public void ExitGame()
        {
            DataManager.Instance.ExitGame();
        }
        public void PlayButtonSound()
        {
            SoundManager.Instance.PlayPressedButtonSound();
        }
        public void SetPlayer2(bool set)
        {
            GameManager.Instance.Player2 = set;
        }
        void SetPlayers()
        {
            if (GameManager.Instance.Player2)
            {
                //button
                _pressedButton.SetActive(true);
                _originButton.SetActive(false);
                //playerprefab
                _player2Objcet.SetActive(true);
            }
        }
    }
}
