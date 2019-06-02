using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateBefore(typeof(CollisionCleanupSystem))]
public class CollisionSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentDataArray<HealthData> healthC;
        public ComponentDataArray<CollisionDataComponent> collisionDataC;
        public EntityArray entities;
    }

    [Inject] Components components;

    protected override void OnUpdate()
    {
        for (var i = 0; i < components.Length; i++)
        {
            // Setup
            var collisionState = components.collisionDataC[i].collisionState;
            var otherEntity = components.collisionDataC[i].otherEntity;
            var entity = components.entities[i];

            // Check if collision has just happend
            if (collisionState == CollisionState.Enter)
            {
                // Check if other entity has a damage component
                if (EntityManager.HasComponent<DamageData>(otherEntity))
                {
                    // Check if other entity is not the same faction
                    if (EntityManager.GetComponentData<FactionData>(otherEntity).faction != EntityManager.GetComponentData<FactionData>(entity).faction)
                    {
                        // Set the health data component of current entity to be a new new health data component with the previous health minus the damage of the other entity
                        EntityManager.SetComponentData<HealthData>(entity, new HealthData
                        {
                            health = components.healthC[i].health - EntityManager.GetComponentData<DamageData>(otherEntity).damage
                        });
                    }
                }
            }
        }
    }
}
