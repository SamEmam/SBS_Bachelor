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
        public EntityArray entities;
    }

    [Inject] private Components components;


    protected override void OnUpdate()
    {
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var spawnC = components.spawnC[i];
            var transform = components.transform[i];

            // Functionality


            for (int j = 0; j < spawnC.numberOfSpawns; j++)
            {
                var pos = spawnC.transform.position = Random.insideUnitSphere * (spawnC.minDistance + (spawnC.maxDistance - spawnC.minDistance));       // Set position within sphere
                var gameobject = Object.Instantiate(spawnC.prefab, transform.position, transform.rotation);                                             // Instantiate prefab
                gameobject.transform.rotation = Random.rotation;                                                                                        // Randomize prefab
                spawnC.numberOfSpawns--;                                                                                                                // Count down number of spawns
                if (spawnC.numberOfSpawns <= 0)
                {
                    Object.Destroy(spawnC.gameObject);                                                                                                  // Remove component
                }
            }
        }
    }
}
