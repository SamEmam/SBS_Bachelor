using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class TargetSystem : ComponentSystem
{
    struct Components
    {
        public readonly int Length;
        public ComponentArray<RotationComponent> rotationC;
        public ComponentDataArray<TargetData> targetC;
        public ComponentArray<Transform> transform;
        public EntityArray entities;
    }

    [Inject] private Components components;
    private Transform lastEnemy;

    protected override void OnUpdate()
    {
        // This ship
        for (int i = 0; i < components.Length; i++)
        {
            // Setup
            var entity = components.entities[i];
            var rotationC = components.rotationC[i];
            var targetC = components.targetC[i];
            var transform = components.transform[i];

            // Functionality
            targetC.closestDistance = 1000f;

            if (rotationC.target)
            {
                lastEnemy = rotationC.target;
            }

            // Other ship
            for (int j = 0; j < components.Length; j++)
            {
                // Setup
                var otherEntity = components.entities[j];
                var otherRotationC = components.rotationC[j];
                var otherTargetC = components.targetC[j];
                var otherTransform = components.transform[j];

                // Functionality
                
                if (!lastEnemy)
                {
                    lastEnemy = otherTransform.transform;
                }

                // If this ship is not other ship, and if othership is not our current target
                if (transform != otherTransform && otherTransform.transform != lastEnemy)
                {
                    // Calculate distance between this ship and other ship
                    var dist = Vector3.Distance(transform.position, otherTransform.position);

                    // If distance between other ship is shorter than closest
                    // Sets new closest enemy
                    if (dist < targetC.closestDistance)
                    {
                        targetC.closestDistance = dist;
                        rotationC.target = otherTransform.transform;
                    }

                    // If othership is close, it is saved as lastEnemy, and cannot be targeted until new lastEnemy
                    // ClosestDist reset
                    if (targetC.closestDistance < 40)
                    {
                        lastEnemy = rotationC.target;
                        targetC.closestDistance = 1000f;
                    }
                }
            }
        }
    }
}
