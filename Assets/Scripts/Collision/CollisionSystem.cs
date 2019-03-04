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
            if (EntityManager.HasComponent<DamageData>(data.collisionDataC[i].OtherEntity))
            {
                EntityManager.SetComponentData<HealthData>(data.Entity[i], new HealthData
                {
                    health = data.healthC[i].health - EntityManager.GetComponentData<DamageData>(data.collisionDataC[i].OtherEntity).damage
                });
            }
        }
    }
}
