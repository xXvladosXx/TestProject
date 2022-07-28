using System;
using DG.Tweening;
using UnityEngine;

namespace TransformChange
{
    public class Rotation : MonoBehaviour
    {
        private Tween _rotateTween;
        
        private void Start()
        {
            _rotateTween = transform.DORotate(new Vector3(360f, 360f, 360f), 5f, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetRelative()
                .SetEase(Ease.Linear);

        }

        private void OnDestroy()
        {
            _rotateTween.Kill(true);
        }
    }
}