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
            var movementC = components.movementC[i];
            var rigidbody = components.rigidbody[i];
            var transform = components.transform[i];

            // Functionality
            if (movementC.movementType == MovementType.vectorMovement)
            {
                transform.position += transform.forward * movementC.movementSpeed * deltaTime;
            }
            else if (movementC.movementType == MovementType.rigidbodyMovement)
            {
                rigidbody.AddForce(transform.forward * movementC.movementSpeed * 75);
            }
        }
    }
}
