using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using System;

[UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
public class RotationSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> rotationC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var rotationC = components.rotationC[i];
            var transform = components.transform[i];

            // Functionality
            if (rotationC.target)
            {
                var pos = (rotationC.target.position - transform.position) + rotationC.target.forward;
                Quaternion targetRotation = Quaternion.LookRotation(pos);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationC.rotationSpeed * deltaTime);
            }
            
        }
    }
}
