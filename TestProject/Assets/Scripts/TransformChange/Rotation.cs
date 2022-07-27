using DG.Tweening;
using UnityEngine;

namespace TransformChange
{
    public class Rotation : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(new Vector3(360f, 360f, 360f), 5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear);

        }
    }
}