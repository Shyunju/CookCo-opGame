using UnityEngine;
using UnityEngine.UI;

namespace CookCo_opGame
{
    public class SFXVolumeSlider : MonoBehaviour
    {
        private Slider _sfxSlider;
        private void Awake()
        {
            _sfxSlider = GetComponent<Slider>();
            _sfxSlider.onValueChanged.AddListener(SFXChangeVolume);
        }
        void Start()
        {
            _sfxSlider.value = SoundManager.Instance.SFXVolume;
        }

        public void SFXChangeVolume(float volume)
        {
            SoundManager.Instance.SFXChangeVolume(volume);
        }
    }
}
