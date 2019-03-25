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
        public EntityArray entities;
    }

    [Inject] private Components components;

    int counter = 0;

    protected override void OnUpdate()
    {
        if (counter > components.Length)
        {
            return;
        }
        else
        {
            for (int i = 0; i < components.Length; i++)
            {
                // Setup
                var entity = components.entities[i];
                var asteroidC = components.asteroidC[i];
                var transform = components.transform[i];

                // Functionality
                if (!asteroidC.hasSetSize)                  
                {
                    asteroidC.hasSetSize = true;
                    var size = Random.Range(asteroidC.minSize, asteroidC.maxSize);
                    transform.localScale = new Vector3(size, size, size);
                }
                
            }
            counter++;
        }
    }
}
