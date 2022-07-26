using System;
using Data.Core;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class CameraControllerData : IData
    {
        [field: Header("Base Movement")]
        [field: SerializeField] public float MaxSpeed { get; private set; }
        [field: SerializeField] public float Acceleration { get; private set; }
        [field: SerializeField] public float Damping { get; private set; }
        
        [field: Header("Zooming")]
        [field: SerializeField] public float SizePerScroll { get; private set; }
        [field: SerializeField] public float ZoomDamping { get; private set; }
        [field: SerializeField] public float MinHeight { get; private set; }
        [field: SerializeField] public float MaxHeight { get; private set; }
        [field: SerializeField] public float ZoomSpeed { get; private set; }
        
        [field: Header("Rotation")]
        [field: SerializeField] public float MaxRotationSpeed { get; private set; }

        [field: Header("Edge Detection")]
        [field: Range(0,1f)]
        [field: SerializeField] public float EdgeTolerance { get; private set; }
        [field: SerializeField] public bool UseScreenEdge { get; private set; } = true;
    }
}