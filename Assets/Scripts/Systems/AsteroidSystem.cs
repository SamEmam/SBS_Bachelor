using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateAfter(typeof(SpawnSystem))]
public class AsteroidSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<AsteroidComponent> asteroidC;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;

    int counter = 0;

    protected override void OnUpdate()
    {
        /*
         * If all asteroids has had their size updated
         * the update method will be interupted before
         * it iterates through all entities
         */
        if (counter > components.Length)
        {
            return;
        }
        else
        {
            for (int i = 0; i < components.Length; i++)
            {
                // Setup
                var asteroidC = components.asteroidC[i];
                var transform = components.transform[i];

                // Functionality
                // If size has not been set
                if (!asteroidC.hasSetSize)                  
                {
                    asteroidC.hasSetSize = true;                                            // Update hasSetSize boolean
                    RandomizeSize(asteroidC.minSize, asteroidC.maxSize, transform);
                }
                counter++;                                                                  // Count up for each asteroid
            }
        }
    }

    void RandomizeSize(float minSize, float maxSize, Transform transform)
    {
        float size = Random.Range(minSize, maxSize);                            // Set float size to a random value between min-max float
        transform.localScale = new Vector3(size, size, size);                   // Set the scale to a new vector 3 with size
    }
}
