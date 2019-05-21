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
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var rotationC = components.rotationC[i];
            var transform = components.transform[i];

            // Functionality
            if (rotationC.target)                                                                                                   // If target is not null
            {
                var pos = (rotationC.target.position - transform.position) + rotationC.target.forward;                              // Get position of target if position of this transform was (0, 0, 0)
                Quaternion targetRotation = Quaternion.LookRotation(pos);                                                           // Set the rotation from (0, 0, 0) to pos
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationC.rotationSpeed * deltaTime);     // Rotate towards target rotation over time
            }
        }
    }
}
