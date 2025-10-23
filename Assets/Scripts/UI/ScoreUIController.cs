using UnityEngine;
using TMPro;

namespace CookCo_opGame
{
    public class ScoreUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreTxt;
        [SerializeField] GameObject[] LifeUI;
        [SerializeField] TMP_Text timerText;         // Unity 에디터에서 할당
        float totalTime = 180f; // 3분 = 180초
        public bool IsCooking { get; set; }

        void Start()
        {
            IsCooking = false;
        }
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
            if (cnt == 0)
            {
                GameOver();
            }                
        }
        void Update()
        {
            if (IsCooking)
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
                    GameOver();
                }
            }
            
        }

        void GameOver()
        {
            //시간 오버
            IsCooking = false;
            timerText.text = "0 : 00";
            totalTime = 0;
            GameManager.Instance.TriggerInputStop();
            this.enabled = false;
        }
    }
}
