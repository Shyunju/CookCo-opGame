using TMPro;
using UnityEngine;

namespace CookCo_opGame
{
    public class ScoreAndTimerUIController : MonoBehaviour
    {
        [SerializeField] TMP_Text _scoreTxt;
        [SerializeField] TMP_Text _timeText;

        //public float LevelTime { get;  private set; }
        private float LevelTime = 180f;

        public void UpdateScoreText()
        {
            _scoreTxt.text = GameManager.Instance.Score.ToString();
        }
        void Update()
        {
            if (LevelTime > 0)
        {
            LevelTime -= Time.deltaTime;
            if (LevelTime < 0) LevelTime = 0;

            int minutes = Mathf.FloorToInt(LevelTime / 60f);
            int seconds = Mathf.FloorToInt(LevelTime % 60);
            _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        }
    }
}
