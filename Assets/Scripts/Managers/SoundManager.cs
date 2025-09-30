using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace CookCo_opGame
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] AudioClip _successSound;
        [SerializeField] AudioClip _failSound;
        [SerializeField] AudioClip _whistleSound;
        [SerializeField] AudioClip _paperSound;
        [SerializeField] AudioClip _pressedButtonSound;
        [SerializeField] AudioClip _selectMusic;
        [SerializeField] AudioClip _lobbyMusic;
        [SerializeField] AudioClip _mainMusic;
        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] AudioSource BGM;
        [SerializeField] AudioSource SFX;
        
        private bool[] _isMute = new bool[3];
        private float[] _audioVolumes = new float[3];
        public float BGMVolume { get;  private set; }
        public float SFXVolume { get; private set;}

        void Awake()
        {
            base.Awake();
            BGMVolume = 1f;
            SFXVolume = 1f;
        }
        void Start()
        {
            PlaySelectBGM();
        }
        public void PlaySuccessSound()
        {
            SFX.PlayOneShot(_successSound);
        }
        public void PlayFailSound()
        {
            SFX.PlayOneShot(_failSound);
        }
        public void PlayWhistleSound()
        {
            SFX.PlayOneShot(_whistleSound);
        }
        public void PlayLobbyBGM()
        {
            BGM.resource = _lobbyMusic;
            BGM.Play();
        }
        public void PlayMainBGM()
        {
            BGM.resource = _mainMusic;
            BGM.Play();
        }
        public void PlaySelectBGM()
        {
            BGM.resource = _selectMusic;
            BGM.Play();
        }
        public void StopBGM()
        {
            BGM.Stop();
        }
        public void PlayPaperSound()
        {
            SFX.PlayOneShot(_paperSound);
        }
        public void PlayPressedButtonSound()
        {
            SFX.PlayOneShot(_pressedButtonSound);
        }


        public void SetAudioVolume(EAudioMixerType audioMixerType, float volume)
        {
            // 오디오 믹서의 값은 -80 ~ 0까지이기 때문에 0.0001 ~ 1의 Log10 * 20을 한다.
            _audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
        }

        public void SetAudioMute(EAudioMixerType audioMixerType)
        {
            int type = (int)audioMixerType;
            if (!_isMute[type]) // 뮤트 
            {
                _isMute[type] = true;
                _audioMixer.GetFloat(audioMixerType.ToString(), out float curVolume);
                _audioVolumes[type] = curVolume;
                SetAudioVolume(audioMixerType, 0.001f);
            }
            else
            {
                _isMute[type] = false;
                SetAudioVolume(audioMixerType, _audioVolumes[type]);
            }
        }
        public void Mute()
        {
            SetAudioMute(EAudioMixerType.BGM);
        }

        public void BGMChangeVolume(float volume)
        {
            SetAudioVolume(EAudioMixerType.BGM, volume);
            BGMVolume = volume;
        }
        public void SFXChangeVolume(float volume)
        {
            SetAudioVolume(EAudioMixerType.SFX, volume);
            SFXVolume = volume;
        }
    }
}
