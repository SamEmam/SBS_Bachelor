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
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        var deltatime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var lifespanC = components.lifespanC[i];

            // Functionality
            if (lifespanC)
            {
                if (lifespanC.lifespan <= 0)
                {
                    Object.Destroy(lifespanC.gameObject);
                }
                else
                {
                    lifespanC.lifespan -= deltatime;
                }
            }

        }
    }
}
