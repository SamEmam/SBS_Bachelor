using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

[UpdateAfter(typeof(UnityEngine.Experimental.PlayerLoop.FixedUpdate))]
public class MovementSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentDataArray<MovementData> movementC;
        public ComponentArray<Rigidbody> rigidbody;
        public ComponentArray<Transform> transform;
    }

    [Inject] private Components components;

    protected override void OnUpdate()
    {
        float deltaTime = Time.deltaTime;
        var movementSpeedMultiplier = 75f;

        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var movementC = components.movementC[i];
            var rigidbody = components.rigidbody[i];
            var transform = components.transform[i];

            // Functionality
            if (movementC.movementType == MovementType.vectorMovement)                                          // If vector movement
            {   
                transform.position += transform.forward * movementC.movementSpeed * deltaTime;                  // Move transform forward
            }
            else if (movementC.movementType == MovementType.rigidbodyMovement)                                  // If rigidbody movement
            {
                rigidbody.AddForce(transform.forward * movementC.movementSpeed * movementSpeedMultiplier);      // Add forward velocity to rigidbody
            }
        }
    }
}
