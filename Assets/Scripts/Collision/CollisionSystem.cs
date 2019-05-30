using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateBefore(typeof(CollisionCleanupSystem))]
public class CollisionSystem : ComponentSystem
{
    struct Data
    {
        public readonly int Length;
        public ComponentDataArray<HealthData> healthC;
        public ComponentDataArray<CollisionDataComponent> collisionDataC;
        public EntityArray Entity;
    }

    [Inject] Data data;

    protected override void OnUpdate()
    {
        for (var i = 0; i < data.Length; i++)
        {
            // Setup
            var collisionDataC = data.collisionDataC[i];

            // Check if other entity has a damage component
            if (EntityManager.HasComponent<DamageData>(collisionDataC.otherEntity))
            {
                // Check if other entity is not the same faction
                if (EntityManager.GetComponentData<FactionData>(collisionDataC.otherEntity).faction != EntityManager.GetComponentData<FactionData>(data.Entity[i]).faction)
                {
                    // Set the health data component of current entity to be a new new health data component with the previous health minus the damage of the other entity
                    EntityManager.SetComponentData<HealthData>(data.Entity[i], new HealthData
                    {
                        health = data.healthC[i].health - EntityManager.GetComponentData<DamageData>(collisionDataC.otherEntity).damage
                    });
                }
            }
        }
    }
}
