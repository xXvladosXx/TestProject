using UnityEngine;

namespace Audio.Core
{
    public class PlaySoundMusicOnStart : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private void Start()
        {
            AudioManager.Instance.PlayMusicSound(_clip);
        }
    }
}