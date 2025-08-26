using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CookCo_opGame
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreTxt;
        [SerializeField] GameObject[] LifeUI;
        [SerializeField] float totalTime = 30f; // 3분 = 180초
        [SerializeField] TMP_Text timerText;         // Unity 에디터에서 할당

        public void UpdateScoreText()
        {
            _scoreTxt.text = CookingPlayManager.Instance.Score.ToString();
        }

        public void ChangeLifeUI()
        {
            int cnt = CookingPlayManager.Instance.LifeCount;
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
        void Update()
        {
            if (totalTime > 0)
            {
                totalTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(totalTime / 60);
                int seconds = Mathf.FloorToInt(totalTime % 60);
                timerText.text = string.Format("{0:0} : {1:00}", minutes, seconds);
            }
            else
            {
                //시간 오버
                timerText.text = "0m 00s";
                totalTime = 0;
                GameManager.Instance.GoToLobby();
            }
        }
    }
}
