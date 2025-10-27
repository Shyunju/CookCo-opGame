using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

namespace CookCo_opGame
{
    public class TextLocalization : MonoBehaviour
    {
        [SerializeField] TMP_Text _languageText;
        private void RefreshText()
        {
            _languageText.text = Gley.Localization.API.GetCurrentLanguage().ToString();
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
            Gley.Localization.API.SetCurrentLanguage(Gley.Localization.API.GetCurrentLanguage());
        }
    }
}
