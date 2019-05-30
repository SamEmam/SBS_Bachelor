using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class CleanupSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentDataArray<DeathData> deathC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var em = World.Active.GetOrCreateManager<EntityManager>();

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var transform = components.transform[i];

            var deathC = em.GetComponentData<DeathData>(entity);

            // Functionality
            if (transform.gameObject && deathC.deathState == DeathEnum.Dead)                    // If deathState is dead, then destroy the GameObject
            {
                Object.Destroy(transform.gameObject, 1f);
            }
        }
    }
}
