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

            var healthC = em.GetComponentData<HealthData>(entity);
            var deathC = em.GetComponentData<DeathData>(entity);

            // Functionality
            if (healthC.health <= 0)                                                                        // If health is <= 0
            {
                if (deathC.deathState == DeathEnum.Alive)                                                   // If deathState is alive
                {
                    em.SetComponentData(entity, new DeathData { deathState = DeathEnum.Dead });             // Set the deathState to dead
                }
            }
        }
    }
}
