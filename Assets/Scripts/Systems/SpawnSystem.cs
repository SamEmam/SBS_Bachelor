using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class SpawnSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<SpawnComponent> spawnC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;
    
    protected override void OnUpdate()
    {
        int counter = 0;
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var spawnC = components.spawnC[i];
            var transform = components.transform[i];

            // Functionality
            Vector3 startPos = transform.position;

            for (int j = 0; j < spawnC.prefabs.Length; j++)
            {
                for (int k = 0; k < spawnC.numberOfSpawns[j]; k++)                                                                                  // For each prefab, for numberOfSpawns
                {
                    transform.position = Random.insideUnitSphere * (spawnC.minDistance + (spawnC.maxDistance - spawnC.minDistance)) + startPos;     // Set position random within sphere
                    var gameobject = Object.Instantiate(spawnC.prefabs[j], transform.position, transform.rotation);                                 // Instantiate prefab at position
                    gameobject.transform.rotation = Random.rotation;                                                                                // Randomize prefab rotation
                    counter++;                                                                                                                      // count instantiated GameObjects
                }
            }

            if (counter >= spawnC.numberOfSpawns[spawnC.prefabs.Length - 1])                                                                        // If all prefabs has been instantiated
            {
                Object.Destroy(spawnC.gameObject);                                                                                                  // Destroy spawn system GameObject
            }
        }
    }
}
