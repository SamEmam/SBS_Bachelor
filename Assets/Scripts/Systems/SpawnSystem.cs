using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SpawnSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<SpawnComponent> spawnC;
        public ComponentArray<AsteroidComponent> asteroidC;
        public ComponentArray<Transform> transfrom;
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
            var asteroidC = components.asteroidC[i];
            var transform = components.transfrom[i];

            // Functionality
            for (int j = 0; j < spawnC.numberOfSpawns; j++)
            {
                var asteroid = Object.Instantiate(asteroidC.asteroidPrefab, transform.position, transform.rotation);                            // Instantiate asteroid
                asteroid.transform.position = Random.insideUnitSphere * (spawnC.minDistance + (spawnC.maxDistance - spawnC.minDistance));       // Set position within sphere
                var size = Random.Range(asteroidC.minSize, asteroidC.maxSize);                                                                  // Randomize size
                asteroid.transform.localScale = new Vector3(size, size, size);                                                                  // Set size
                asteroid.transform.rotation = Random.rotation;                                                                                  // Randomize rotation
                spawnC.numberOfSpawns--;                                                                                                        // Count down spawned asteroid
            }
        }
    }
}
