using UnityEngine;

namespace CookCo_opGame
{
    public class SoundManager : Singleton<SoundManager>
    {
        [SerializeField]
        AudioClip _successSound;
        [SerializeField]
        AudioClip _failSound;
        [SerializeField]
        AudioClip _whistleSound;
        [SerializeField] AudioSource _backGroundMusicAudioSourcce;
        [SerializeField] AudioClip _lobbyMusic;
        [SerializeField] AudioClip _mainMusic;
        AudioSource _audioSource;

        //필요한거 (브금(로비, 쿠깅), 구매, 클릭(?), 도마(확인필요), 라이프 감소, 게임 오버)

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            PlayLobbyBGM();
        }
        public void PlaySuccessSound()
        {
            _audioSource.PlayOneShot(_successSound);
        }
        public void PlayFailSound()
        {
            _audioSource.PlayOneShot(_failSound);
        }
        public void PlayWhistleSound()
        {
            _audioSource.PlayOneShot(_whistleSound);
        }
        public void PlayLobbyBGM()
        {
            _backGroundMusicAudioSourcce.resource = _lobbyMusic;
            _backGroundMusicAudioSourcce.Play();
        }
        public void PlayMainBGM()
        {
            _backGroundMusicAudioSourcce.resource = _mainMusic;
            _backGroundMusicAudioSourcce.Play();
        }
        public void StopBGM()
        {
            _backGroundMusicAudioSourcce.Stop();
        }

    }
}
