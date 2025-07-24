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
        AudioClip _knifeSound;

        AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        public void PlaySuccessSound()
        {
            _audioSource.PlayOneShot(_successSound);
        }
        public void PlayFailSound()
        {
            _audioSource.PlayOneShot(_failSound);
        }

        public void PlayKnifeSound()
        {
            
        }

    }
}
