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

            // Functionality
            if (lifespanC.lifespan <= 0 && !lifespanC.lifeHasEnded)
            {
                lifespanC.lifeHasEnded = true;
                // If object contains explosionPrefab, initiale and give velocity
                if (lifespanC.explosionPrefab)
                {
                    var explosion = Object.Instantiate(lifespanC.explosionPrefab, transform.position, transform.rotation);
                    var explosionRB = explosion.GetComponent<Rigidbody>();
                    explosionRB.velocity = rigidbody.velocity;
                    explosionRB.angularVelocity = Vector3.zero;
                    
                }
                EntityManager.SetComponentData(components.entities[i], new DeathData { deathState = DeathEnum.Dead });

            }
            else
            {
                lifespanC.lifespan -= deltatime;
            }
        }


    }
}
