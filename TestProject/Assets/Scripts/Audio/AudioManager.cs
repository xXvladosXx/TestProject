using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        [SerializeField] private AudioSource _musicSource, _effectsSource;
    
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayEffectSound(AudioClip audioClip)
        {
            _effectsSource.PlayOneShot(audioClip);
        }
    
        public void PlayMusicSound(AudioClip audioClip)
        {
            _musicSource.PlayOneShot(audioClip);
            _musicSource.loop = true;
        }
    }
}