using UnityEngine;

namespace Audio.Core
{
    public class PlaySoundEffectOnEnable : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        private void OnEnable()
        {
            AudioManager.Instance.PlayEffectSound(_clip);
        }
    }
}