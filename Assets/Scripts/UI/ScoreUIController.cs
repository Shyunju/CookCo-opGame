using UnityEngine;
using TMPro;

namespace CookCo_opGame
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreTxt;
        [SerializeField] GameObject[] LifeUI;

        public void UpdateScoreText()
        {
            _scoreTxt.text = GameManager.Instance.Score.ToString();
        }

        public void ChangeLifeUI()
        {
            int cnt = GameManager.Instance.LifeCount;
            for (int i = 0; i < LifeUI.Length; i++)
            {
                if (i < cnt)
                {
                    LifeUI[i].SetActive(true);
                }
                else
                {
                    LifeUI[i].SetActive(false);
                }
            }
        }
    }
}
