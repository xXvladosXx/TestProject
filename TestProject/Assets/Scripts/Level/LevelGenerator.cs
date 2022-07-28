using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Interaction;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Level
{
    [Serializable]
    public class LevelGenerator
    {
        [field: SerializeField] public LevelGeneratorData LevelGeneratorData { get; private set; }

        private List<InteractableObject> _instantiatedObjects = new List<InteractableObject>();

        public void InstantiateObjects()
        {
            var xPoint = Random.Range(-LevelGeneratorData.PossibleSpawnPoint.x,
                LevelGeneratorData.PossibleSpawnPoint.x);

            var zPoint = Random.Range(-LevelGeneratorData.PossibleSpawnPoint.z,
                LevelGeneratorData.PossibleSpawnPoint.z);

            var vectorToSpawn = new Vector3(xPoint, 0, zPoint);

            var chest = Object.Instantiate(LevelGeneratorData.ChestPrefab,
                vectorToSpawn, Quaternion.identity);
            
            var key = Object.Instantiate(LevelGeneratorData.KeyPrefab, chest.transform);
            
            string uniqueIdentifier = System.Guid.NewGuid().ToString();
            key.Hash = uniqueIdentifier;

            _instantiatedObjects.Add(chest);
            _instantiatedObjects.Add(key);

            List<int> usedWalls = new List<int>();

            SpawnDoors(usedWalls, uniqueIdentifier);
            
            while (usedWalls.Count == 0)
            {
                SpawnDoors(usedWalls, uniqueIdentifier);
            }
        }

        private void SpawnDoors(List<int> usedWalls, string uniqueIdentifier)
        {
            var possibleDoorsToGenerate = Random.Range(4, LevelGeneratorData.PossibleDoorsToGenerate + 1);
            bool hashSpawned = false;
            
            for (int i = 0; i < possibleDoorsToGenerate; i++)
            {
                int hasToSpawn = Random.Range(0, 2);
                int withHash = Random.Range(0, 2);
                int randomWall = Random.Range(0, LevelGeneratorData.PossibleWallsToSpawn.Count);

                if (hasToSpawn == 1 && !usedWalls.Contains(randomWall))
                {
                    var door = Object.Instantiate(LevelGeneratorData.DoorPrefab,
                        LevelGeneratorData.PossibleWallsToSpawn[randomWall].transform);
                    
                    _instantiatedObjects.Add(door);

                    if (withHash == 1)
                    {
                        door.Hash = uniqueIdentifier;
                        hashSpawned = true;
                    }
                    
                    usedWalls.Add(randomWall);
                }
            }
            
            foreach (var instantiatedObject in _instantiatedObjects.Where(instantiatedObject => !hashSpawned))
            {
                if (!(instantiatedObject is RequirementInteractableObject requirementInteractableObject)) continue;
                
                requirementInteractableObject.Hash = uniqueIdentifier;
                hashSpawned = true;
            }
        }

        public void DestroyObjects()
        {
            foreach (var instantiatedObject in _instantiatedObjects)
            {
                Object.Destroy(instantiatedObject.gameObject);
            }

            _instantiatedObjects.Clear();
        }
    }
}