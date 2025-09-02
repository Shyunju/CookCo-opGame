using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class CookingStartButton : MonoBehaviour
    {
        public Button targetButton;

        void Start()
        {
            targetButton.onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            GameManager.Instance.StartCooking(); // 싱글톤 메소드 호출
            SoundManager.Instance.StopBGM();
        }
    }
}
