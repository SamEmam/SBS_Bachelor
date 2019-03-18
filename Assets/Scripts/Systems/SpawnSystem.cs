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
        public ComponentArray<AsteroidComponent> asteroidC;
        public ComponentArray<Transform> transfrom;
        public EntityArray entities;
        [NativeDisableParallelForRestriction]public EntityCommandBuffer commandBuffer;
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
            var commandB = components.commandBuffer;

            // Functionality


            for (int j = 0; j < spawnC.numberOfSpawns; j++)
            {
                var pos = spawnC.transform.position = Random.insideUnitSphere * (spawnC.minDistance + (spawnC.maxDistance - spawnC.minDistance));       // Set position within sphere
                var size = Random.Range(asteroidC.minSize, asteroidC.maxSize);
                while (!Physics.CheckSphere(pos, size / 2f))
                {
                    pos = spawnC.transform.position = Random.insideUnitSphere * (spawnC.minDistance + (spawnC.maxDistance - spawnC.minDistance));       // Set position within sphere
                    size = Random.Range(asteroidC.minSize, asteroidC.maxSize);
                    Debug.Log(j + "collide on spawn");

                }
                // Instantiate asteroid
                var asteroid = Object.Instantiate(asteroidC.asteroidPrefab, transform.position, transform.rotation);
                asteroid.transform.localScale = new Vector3(size, size, size);                                                                  // Set size
                asteroid.transform.rotation = Random.rotation;
                spawnC.numberOfSpawns--;
                if (spawnC.numberOfSpawns <= 0)
                {
                    commandB.RemoveComponent<SpawnComponent>(entity);
                }                                                                                  // Randomize rotation
            }
        }
    }
}
