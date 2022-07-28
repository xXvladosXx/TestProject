using System;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LevelGeneratorData
    {
        [field: Range(1,8)]
        [field: SerializeField] public int PossibleDoorsToGenerate { get; private set; }
        
        [field: SerializeField] public Vector3 PossibleSpawnPoint { get; private set; }
        [field: SerializeField] public ChestInteractableObject ChestPrefab { get; private set; }
        [field: SerializeField] public RequirementInteractableObject DoorPrefab { get; private set; }
        [field: SerializeField] public ItemInteractableObject KeyPrefab { get; private set; }
        
        [field: SerializeField] public List<GameObject> PossibleWallsToSpawn { get; private set; }
    }
}