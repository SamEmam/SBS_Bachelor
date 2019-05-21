using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class LifespanSystem : ComponentSystem
{

    struct Components
    {
        public readonly int Length;
        public ComponentArray<LifespanComponent> lifespanC;
        public ComponentDataArray<DeathData> deathC;
        public ComponentArray<Rigidbody> rigidbody;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var lifespanC = components.lifespanC[i];
            var rigidbody = components.rigidbody[i];
            var transform = components.transform[i];
            var entity = components.entities[i];

            // Functionality
            if (lifespanC.lifespan <= 0 && !lifespanC.lifeHasEnded)                                                                 // If lifespan is <= 0 and lifespan hasn't been <= 0 before
            {
                lifespanC.lifeHasEnded = true;

                // If object contains explosionPrefab, initiate explosion and inherit velocity
                if (lifespanC.explosionPrefab)
                {
                    var explosion = Object.Instantiate(lifespanC.explosionPrefab, transform.position, transform.rotation);          // Instantiate exposion prefab
                    var explosionRB = explosion.GetComponent<Rigidbody>();                                                          // Get a reference to rigidbody of explosion
                    explosionRB.velocity = rigidbody.velocity;                                                                      // Inherit the velocity
                    explosionRB.angularVelocity = Vector3.zero;                                                                     // Set the angular velocity to 0
                    
                }
                EntityManager.SetComponentData(entity, new DeathData { deathState = DeathEnum.Dead });                              // Update the deathState

            }
            else
            {
                lifespanC.lifespan -= deltatime;                                                                                    // Count down lifespan
            }
        }
    }
}
