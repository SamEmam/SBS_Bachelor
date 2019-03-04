using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DeathSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentDataArray<HealthData> healthC;
        public ComponentDataArray<DeathData> deathC;
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
            var healthC = components.healthC[i];
            var deathC = components.deathC[i];
            var transform = components.transform[i];

            // Functionality
            if (healthC.health <= 0 && deathC.isDead == 0)
            {
                deathC.isDead = 1;

                // temp death
                Object.Destroy(transform.gameObject);
                //var explosion = Object.Instantiate(deathC.deathEffect, transform.position, Quaternion.identity);

            }
        }


        
    }
}
