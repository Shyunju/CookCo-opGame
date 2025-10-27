using UnityEngine;
using UnityEngine.Audio;
namespace CookCo_opGame
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField] AudioClip _successSound;
        [SerializeField] AudioClip _failSound;
        [SerializeField] AudioClip _whistleSound;
        [SerializeField] AudioClip _paperSound;
        [SerializeField] AudioClip _pressedButtonSound;
        [SerializeField] AudioClip _cuttingSound;
        [SerializeField] AudioClip _cuteSound;
        [SerializeField] AudioClip _selectMusic;
        [SerializeField] AudioClip _lobbyMusic;
        [SerializeField] AudioClip _mainMusic;
        [SerializeField] AudioMixer _audioMixer;
        [SerializeField] AudioSource BGM;
        [SerializeField] AudioSource SFX;
        [SerializeField] AudioSource PFX1;
        [SerializeField] AudioSource PFX2;

        
        private bool[] _isMute = new bool[3];
        private float[] _audioVolumes = new float[3];
        public float BGMVolume { get;  private set; }
        public float SFXVolume { get; private set; }
        public float PFX1Volume { get; private set; }
        public float PFX2Volume { get; private set;}

        protected override void Awake()
        {
            base.Awake();
            BGMVolume = 1f;
            SFXVolume = 1f;
            PFX1Volume = 1f;
            PFX2Volume = 1f;
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
        public void PlayCuttingSound(int playerNumber)
        {
            if (playerNumber == 1)
            {
                PFX1.resource = _cuttingSound;
                PFX1.Play();
            }
            else
            {
                PFX2.resource = _cuttingSound;
                PFX2.Play();                
            }
        }
        public void StopCuttingSound(int playerNumber)
        {
            if (playerNumber == 1)
            {
                PFX1.Stop();
            }
            else
            {
                PFX2.Stop();
            }
        }
        public void PlayCuteSound(int playerNumber)
        {
            if (playerNumber == 1)
                PFX1.PlayOneShot(_cuteSound);
            else
                PFX2.PlayOneShot(_cuteSound);
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
            SetAudioVolume(EAudioMixerType.PFX1, volume);
            SetAudioVolume(EAudioMixerType.PFX2, volume);
            SFXVolume = volume;
            PFX1Volume = volume;
            PFX2Volume = volume;
        }
    }
}
