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

        [field: Header("Movement clamp")]
        [field: SerializeField]
        public Vector3 MaxPosition { get; private set; } = new Vector3(10, 5, 10);
        [field: SerializeField] 
        public Vector3 MinPosition { get; private set; } = new Vector3(-10, 5, -10);
        
        [field: Header("Rotation")]
        [field: SerializeField] public float MaxRotationSpeed { get; private set; }

    }
}