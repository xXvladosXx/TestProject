using System;
using System.Collections.Generic;
using Data.Core;
using Interaction;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LevelGeneratorData : IData
    {
        [field: Header("Doors to generate")]
        [field: Range(1,7)]
        [field: SerializeField] public int PossibleDoorsToGenerate { get; private set; }
        
        [field: Range(2,7)]
        [field: SerializeField] public int MinAmountOfGeneratedDoors { get; private set; }
        [field: SerializeField] public List<GameObject> PossibleWallsToSpawn { get; private set; }

        
        [field: Header("Spawn form")]
        [field: SerializeField] public Vector3 PossibleSpawnPoint { get; private set; }
        [field: SerializeField] public Collider ColliderToSpawnChestIn { get; private set; }
        
        
        [field: Header("Interactable objects")]
        [field: SerializeField] public ChestInteractableObject ChestPrefab { get; private set; }
        [field: SerializeField] public RequirementInteractableObject DoorPrefab { get; private set; }
        [field: SerializeField] public ItemInteractableObject KeyPrefab { get; private set; }
    }
}