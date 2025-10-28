using Gley.Localization;
using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class TextLocalization : MonoBehaviour
    {
        [SerializeField] TMP_Text _languageText;
        [SerializeField] TMP_Text _dashText;
        [SerializeField] TMP_Text _holdText;
        [SerializeField] TMP_Text _howToDescriptionText;
        [SerializeField] TMP_Text _howToText;
        [SerializeField] TMP_Text _interactionText;
        [SerializeField] TMP_Text _moveText;
        [SerializeField] TMP_Text _recipeText;
        [SerializeField] TMP_Text _tableText;
        [SerializeField] TMP_Text _saveCheckText;
        [SerializeField] TMP_Text _saveAlertText;

        void Start()
        {
            if (GameManager.Instance.CurLanguage != 0)
            {
                Gley.Localization.API.SetCurrentLanguage((SupportedLanguages)GameManager.Instance.CurLanguage);
            }
            RefreshText();
        }
        private void RefreshText()
        {
            _languageText.text = Gley.Localization.API.GetCurrentLanguage().ToString();
            _dashText.text = Gley.Localization.API.GetText(WordIDs.DashID);
            _holdText.text = Gley.Localization.API.GetText(WordIDs.HoldID);
            _howToDescriptionText.text = Gley.Localization.API.GetText(WordIDs.HowToDescriptionID);
            _howToText.text = Gley.Localization.API.GetText(WordIDs.HowToID);
            _interactionText.text = Gley.Localization.API.GetText(WordIDs.InteractionID);
            _moveText.text = Gley.Localization.API.GetText(WordIDs.MoveID);
            _recipeText.text = Gley.Localization.API.GetText(WordIDs.RecipeID);
            _tableText.text = Gley.Localization.API.GetText(WordIDs.TableID);
            _saveCheckText.text = Gley.Localization.API.GetText(WordIDs.SaveCheckID);
            _saveAlertText.text = Gley.Localization.API.GetText(WordIDs.SaveAlertID);
            SaveLanguage();
        }
        public void Nextlanguage()
        {
            Gley.Localization.API.NextLanguage();
            RefreshText();
        }
        public void Prevlanguage()
        {
            Gley.Localization.API.PreviousLanguage();
            RefreshText();
        }
        public void SaveLanguage()
        {
            GameManager.Instance.CurLanguage = (int)Gley.Localization.API.GetCurrentLanguage();
        }
    }
}
