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
            var entity = components.entities[i];

            // Functionality
            if (lifespanC.lifespan <= 0)                                            // If lifespan is <= 0
            {
                EntityManager.AddSharedComponentData(entity, new DeathData { });    // Add death component to entity
            }
            else
            {
                lifespanC.lifespan -= deltatime;        // Count down lifespan with time since last time it was update
            }
        }
    }
}