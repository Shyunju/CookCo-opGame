using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class BGMVolumeSlider : MonoBehaviour
    {
        private Slider _bgmSlider;
        private void Awake()
        {
            _bgmSlider = GetComponent<Slider>();
            _bgmSlider.onValueChanged.AddListener(BGMChangeVolume);
        }
        void Start()
        {
            _bgmSlider.value = SoundManager.Instance.BGMVolume;
        }

        public void BGMChangeVolume(float volume)
        {
            SoundManager.Instance.BGMChangeVolume(volume);
        }
    }
}
