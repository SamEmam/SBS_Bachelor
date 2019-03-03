using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class DeathSystem : ComponentSystem
{
    struct Components
    {
        public HealthData healthC;
        public DeathComponent deathC;
        public Transform transform;
    }

    protected override void OnUpdate()
    {
        
        foreach (var entity in GetEntities<Components>())
        {
            if (entity.healthC.health <= 0 && !entity.deathC.isDead)
            {
                entity.deathC.isDead = true;

                // temp death
                Object.Destroy(entity.transform.gameObject);
                var explosion = Object.Instantiate(entity.deathC.deathEffect, entity.transform.position, Quaternion.identity);

            }
        }
    }
}
