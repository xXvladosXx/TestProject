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
        
        [field: Header("Rotation")]
        [field: SerializeField] public float MaxRotationSpeed { get; private set; }

    }
}